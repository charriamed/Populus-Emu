﻿using System.Collections.Generic;
using System.Linq;
using Stump.Core.Attributes;
using Stump.Core.Extensions;
using Stump.Core.Threading;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Arena;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Parties;
using Stump.Server.WorldServer.Handlers.Basic;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Game.Arena
{
    public class ArenaManager : DataManager<ArenaManager>
    {
        [Variable] public static int MaxPlayersPerFights = 3;

        [Variable] public static int ArenaMinLevel = 200;

        [Variable] public static int ArenaMaxLevelDifference = 40;
        /// <summary>
        /// in ms
        /// </summary>
        [Variable] public static int ArenaUpdateInterval = 100;

        /// <summary>
        /// is seconds
        /// </summary>
        [Variable] public static int ArenaMatchmakingInterval = 30;

        /// <summary>
        /// in minutes
        /// </summary>
        [Variable] public static int ArenaPenalityTime = 0;

        /// <summary>
        /// in minutes
        /// </summary>
        [Variable]
        public static int ArenaWaitTime = 10;

        /// <summary>
        /// Kolizeum MapId that show all Arena Fights
        /// </summary>
        [Variable]
        public static int KolizeumMapId = 81788928;

        public ItemTemplate TokenItemTemplate => m_tokenTemplate ??
                       (m_tokenTemplate = ItemManager.Instance.TryGetTemplate((int)12736)); // Caliston

        Dictionary<int, ArenaRecord> m_arenas_3vs3;
        Dictionary<int, ArenaRecord> m_arenas_1vs1;
        readonly SelfRunningTaskPool m_arenaTaskPool = new SelfRunningTaskPool(ArenaUpdateInterval, "Arena");
        readonly List<ArenaQueueMember> m_queue = new List<ArenaQueueMember>();
        ItemTemplate m_tokenTemplate;

        [Initialization(InitializationPass.Fifth)]
        public override void Initialize()
        {
            m_arenas_3vs3 = Database.Query<ArenaRecord>(ArenaRelator_3vs3.FetchQuery).ToDictionary(x => x.Id);
            m_arenas_1vs1 = Database.Query<ArenaRecord>(ArenaRelator_1vs1.FetchQuery).ToDictionary(x => x.Id);
            m_arenaTaskPool.CallPeriodically(ArenaMatchmakingInterval * 1000, ComputeMatchmaking);
            m_arenaTaskPool.Start();
        }

        public SelfRunningTaskPool ArenaTaskPool => m_arenaTaskPool;

        public Dictionary<int, ArenaRecord> Arenas_3vs3 => m_arenas_3vs3;
        public Dictionary<int, ArenaRecord> Arenas_1vs1 => m_arenas_1vs1;

        public bool CanJoinQueue(Character character)
        {
            if (m_arenas_3vs3.Count == 0 || m_arenas_1vs1.Count == 0)
                return false;

            //Already in queue
            if (IsInQueue(character))
                return false;

            return character.CanEnterArena();
        }

        public bool IsInQueue(Character character) => m_queue.Exists(x => x.Character == character);

        public bool IsInQueue(ArenaParty party) => m_queue.Exists(x => x.Party == party);

        public ArenaQueueMember GetQueueMember(Character character) => m_queue.FirstOrDefault(x => x.Character == character);

        public void AddToQueue(Character character, int mode)
        {
            if (!CanJoinQueue(character))
                return;

            lock (m_queue)
                m_queue.Add(new ArenaQueueMember(character, mode));

            if (mode == 1)
                ContextHandler.SendGameRolePlayArenaRegistrationStatusMessage(character.Client, true,
                    PvpArenaStepEnum.ARENA_STEP_REGISTRED, PvpArenaTypeEnum.ARENA_TYPE_1VS1);

            else
                ContextHandler.SendGameRolePlayArenaRegistrationStatusMessage(character.Client, true,
                        PvpArenaStepEnum.ARENA_STEP_REGISTRED, PvpArenaTypeEnum.ARENA_TYPE_3VS3_TEAM);
        }

        public void RemoveFromQueue(Character character)
        {
            lock (m_queue)
                m_queue.RemoveAll(x => x.Character == character);

            if (character.ArenaMode == 1)
                ContextHandler.SendGameRolePlayArenaRegistrationStatusMessage(character.Client, false,
                    PvpArenaStepEnum.ARENA_STEP_UNREGISTER, PvpArenaTypeEnum.ARENA_TYPE_1VS1);

            else
                ContextHandler.SendGameRolePlayArenaRegistrationStatusMessage(character.Client, false,
                    PvpArenaStepEnum.ARENA_STEP_UNREGISTER, PvpArenaTypeEnum.ARENA_TYPE_3VS3_TEAM);
        }

        public void AddToQueue(ArenaParty party)
        {
            if (!party.Members.All(CanJoinQueue))
                return;

            lock (m_queue)
                m_queue.Add(new ArenaQueueMember(party));

            ContextHandler.SendGameRolePlayArenaRegistrationStatusMessage(party.Clients, true,
                PvpArenaStepEnum.ARENA_STEP_REGISTRED, PvpArenaTypeEnum.ARENA_TYPE_3VS3_TEAM);


            foreach (var character in party.Members.Where(x => x != party.Leader))
                BasicHandler.SendTextInformationMessage(character.Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 272, party.Leader.Name);
            //%1 vous a inscrit à un combat en Kolizéum.
        }

        public void RemoveFromQueue(ArenaParty party)
        {
            lock (m_queue)
                m_queue.RemoveAll(x => x.Party == party);

            ContextHandler.SendGameRolePlayArenaRegistrationStatusMessage(party.Clients, false,
                PvpArenaStepEnum.ARENA_STEP_UNREGISTER, PvpArenaTypeEnum.ARENA_TYPE_3VS3_TEAM);
        }

        public void ComputeMatchmaking()
        {
            List<ArenaQueueMember> queue;
            lock (m_queue)
            {
                queue = m_queue.Where(x => !x.IsBusy()).ToList();
            }

            ArenaQueueMember current;
            while ((current = queue.FirstOrDefault()) != null)
            {
                queue.Remove(current);

                List<ArenaQueueMember> matchs = null;
                if (current.Character != null)
                {
                    matchs = queue.Where(x => x.IsCompatibleWith(current, (current.Character?.ArenaMode == 1) ? true : false)).ToList();
                }
                else
                {
                    matchs = queue.Where(x => x.IsCompatibleWith(current, false)).ToList();
                }
                var allies = new List<ArenaQueueMember> { current };
                var enemies = new List<ArenaQueueMember>();

                int missingAllies = 0;

                if (current.Character?.ArenaMode == 1)
                    missingAllies = 0;

                else
                    missingAllies = MaxPlayersPerFights - current.MembersCount;

                var i = 0;
                while (missingAllies > 0 && i < matchs.Count)
                {
                    if (matchs[i].MembersCount <= missingAllies)
                    {
                        allies.Add(matchs[i]);
                        missingAllies -= matchs[i].MembersCount;
                        matchs.Remove(matchs[i]);
                    }
                    else
                        i++;
                }

                if (missingAllies > 0)
                    continue;

                int missingEnemies = 0;

                if (current?.Character?.ArenaMode == 1)
                    missingEnemies = 1;

                else
                    missingEnemies = MaxPlayersPerFights;

                i = 0;
                while (missingEnemies > 0 && i < matchs.Count)
                {
                    if (matchs[i].MembersCount <= missingEnemies)
                    {
                        enemies.Add(matchs[i]);
                        missingEnemies -= matchs[i].MembersCount;
                        matchs.Remove(matchs[i]);
                    }
                    else
                        i++;
                }

                if (missingEnemies > 0)
                    continue;

                // start fight
                StartFight(allies, enemies);

                queue.RemoveAll(x => allies.Contains(x) || enemies.Contains(x));
                lock (m_queue)
                    m_queue.RemoveAll(x => allies.Contains(x) || enemies.Contains(x));
            }
        }

        void StartFight(IEnumerable<ArenaQueueMember> team1, IEnumerable<ArenaQueueMember> team2)
        {
            ArenaRecord arena = null;

            var m_characterscount1 = team1.SelectMany(x => x.EnumerateCharacters()).ToList().Count;
            var m_characterscount2 = team2.SelectMany(x => x.EnumerateCharacters()).ToList().Count;

            if (m_characterscount1 + m_characterscount2 <= 2)
            {
                Character first = team1.SelectMany(x => x.EnumerateCharacters()).ToArray()[0];
                Character second = team2.SelectMany(x => x.EnumerateCharacters()).ToArray()[0];

                arena = m_arenas_1vs1.RandomElementOrDefault().Value;
                first.CharacterToSeekName = second.Account.Nickname;
                second.CharacterToSeekName = first.Account.Nickname;
            }
            else
                arena = m_arenas_3vs3.RandomElementOrDefault().Value;


            var preFight = FightManager.Instance.CreateArenaPreFight(arena);

            foreach (var character in team1.SelectMany(x => x.EnumerateCharacters()))
            {
                character.DenyAllInvitations(PartyTypeEnum.PARTY_TYPE_ARENA);
                preFight.DefendersTeam.AddCharacter(character);
                character.LeaveDialog();
            }

            foreach (var character in team2.SelectMany(x => x.EnumerateCharacters()))
            {
                character.DenyAllInvitations(PartyTypeEnum.PARTY_TYPE_ARENA);
                preFight.ChallengersTeam.AddCharacter(character);
                character.LeaveDialog();
            }

            if (m_characterscount1 + m_characterscount2 <= 2)
            {
                preFight.ShowPopups();
                return;
            }

            var challengersParty = preFight.ChallengersTeam.Members.Select(x => x.Character.GetParty(PartyTypeEnum.PARTY_TYPE_ARENA)).FirstOrDefault() ??
                                 PartyManager.Instance.Create(PartyTypeEnum.PARTY_TYPE_ARENA);
            var defendersParty = preFight.DefendersTeam.Members.Select(x => x.Character.GetParty(PartyTypeEnum.PARTY_TYPE_ARENA)).FirstOrDefault() ??
                                 PartyManager.Instance.Create(PartyTypeEnum.PARTY_TYPE_ARENA);

            challengersParty.RemoveAllGuest();
            defendersParty.RemoveAllGuest();

            foreach (var character in preFight.ChallengersTeam.Members.Select(x => x.Character).Where(character => !challengersParty.IsInGroup(character)))
            {
                if (challengersParty.Leader != null)
                    challengersParty.Leader.Invite(character, PartyTypeEnum.PARTY_TYPE_ARENA, true);
                else
                    character.EnterParty(challengersParty);
            }

            foreach (var character in preFight.DefendersTeam.Members.Select(x => x.Character).Where(character => !defendersParty.IsInGroup(character)))
            {
                if (defendersParty.Leader != null)
                    defendersParty.Leader.Invite(character, PartyTypeEnum.PARTY_TYPE_ARENA, true);
                else
                    character.EnterParty(defendersParty);
            }

            preFight.ShowPopups();
        }
    }
}