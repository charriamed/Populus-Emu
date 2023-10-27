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
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Arena
{
    public enum LeaguesEnum
    {
        Bronze,
        Silver,
        Gold,
        Cristal,
        Diamond,
        Legend
    }

    public class VersusLeagueData
    {
        public VersusLeagueData(LeaguesEnum type, int pointstonextleague, int winpoints, int lostpoints)
        {
            Type = type;
            PointsToNextLeague = pointstonextleague;
            WinPoints = winpoints;
            LostPoints = lostpoints;
        }

        public LeaguesEnum Type
        {
            get;
            set;
        }

        public int PointsToNextLeague
        {
            get;
            set;
        }

        public int WinPoints
        {
            get;
            set;
        }

        public int LostPoints
        {
            get;
            set;
        }
    }

    public class VersusManager : DataManager<VersusManager>
    {
        [Variable]
        public static int MaxPlayersPerFights = 1;

        [Variable]
        public static int ArenaMinLevel = 50;

        [Variable]
        public static int ArenaMaxLevelDifference = 100;
        /// <summary>
        /// in ms
        /// </summary>
        [Variable]
        public static int ArenaUpdateInterval = 100;

        /// <summary>
        /// is seconds
        /// </summary>
        [Variable]
        public static int ArenaMatchmakingInterval = 10;

        /// <summary>
        /// in minutes
        /// </summary>
        [Variable]
        public static int ArenaPenalityTime = 30;

        /// <summary>
        /// in minutes
        /// </summary>
        [Variable]
        public static int ArenaWaitTime = 10;
        [Variable]
        public static int[] ArenaMapsId = {
            177475586,
            177474562,
            177473538,
            177603586,
            177471490
        };
        /// <summary>
        /// Kolizeum MapId that show all Arena Fights
        /// </summary>
        [Variable]
        public static int KolizeumMapId = 128452097;

        public ItemTemplate TokenItemTemplate => m_tokenTemplate ??
                       (m_tokenTemplate = ItemManager.Instance.TryGetTemplate((int)ItemIdEnum.KOLIZETON_12736));

        readonly List<ArenaQueueMember> m_queue = new List<ArenaQueueMember>();
        ItemTemplate m_tokenTemplate;

        private List<ArenaLeague> Leagues = new List<ArenaLeague>();
        private List<ArenaLeagueReward> LeaguesRewards = new List<ArenaLeagueReward>();
        private List<ArenaLeagueSeason> LeaguesSeasons = new List<ArenaLeagueSeason>();

        private Dictionary<LeaguesEnum, VersusLeagueData> LeaguesInformations = new Dictionary<LeaguesEnum, VersusLeagueData>();


        [Initialization(InitializationPass.Fifth)]
        public override void Initialize()
        {
            Arenas = new Dictionary<int, ArenaRecord>();
            int id = 1;
            foreach (var map in ArenaMapsId)
            {
                Arenas.Add(map, new ArenaRecord()
                {
                    Id = id,
                    MapId = map
                });
                id++;
            }

            Leagues = Database.Query<ArenaLeague>(ArenaLeagueRelator.FetchQuery).OrderBy(x => x.NextLeagueId).ToList();
            LeaguesRewards = Database.Query<ArenaLeagueReward>(ArenaLeagueRewardRelator.FetchQuery).ToList();
            LeaguesSeasons = Database.Query<ArenaLeagueSeason>(ArenaLeagueSeasonRelator.FetchQuery).ToList();

            InitializeLeaguesDatas();

            //ArenaTaskPool.CallPeriodically(ArenaMatchmakingInterval * 1000, ComputeMatchmaking);
            //ArenaTaskPool.Start();
        }

        private void InitializeLeaguesDatas()
        {
            var Bronze = new VersusLeagueData(LeaguesEnum.Bronze, 250, 300, 50);
            var Silver = new VersusLeagueData(LeaguesEnum.Silver, 100, 150, 80);
            var Gold = new VersusLeagueData(LeaguesEnum.Gold, 100, 100, 50);
            var Cristal = new VersusLeagueData(LeaguesEnum.Cristal, 100, 50, 20);
            var Diamond = new VersusLeagueData(LeaguesEnum.Diamond, 100, 50, 40);
            var Legend = new VersusLeagueData(LeaguesEnum.Legend, 0, 50, 50);

            LeaguesInformations.Clear();
            LeaguesInformations.Add(LeaguesEnum.Bronze, Bronze);
            LeaguesInformations.Add(LeaguesEnum.Silver, Silver);
            LeaguesInformations.Add(LeaguesEnum.Gold, Gold);
            LeaguesInformations.Add(LeaguesEnum.Cristal, Cristal);
            LeaguesInformations.Add(LeaguesEnum.Diamond, Diamond);
            LeaguesInformations.Add(LeaguesEnum.Legend, Legend);

            int currentrank = 1;
            Tuple<int, int> savedgoldfix = new Tuple<int, int>(0, 0);

            foreach (var league in Leagues)
            {
                if (league.Type == LeaguesEnum.Legend)
                {
                    league.MinRequiredRank = 5501;
                    league.MaxRequiredRank = int.MaxValue;
                    continue;
                }

                if (league.Id == 34)
                {
                    league.MinRequiredRank = savedgoldfix.Item1;
                    league.MaxRequiredRank = savedgoldfix.Item2;
                    continue;
                }

                var RankGap = GetLeagueData(league.Type).PointsToNextLeague;

                league.MinRequiredRank = currentrank;

                if (league.Id == 6) league.MinRequiredRank = int.MinValue;

                currentrank += RankGap;

                league.MaxRequiredRank = currentrank - 1;

                if (league.Id == 33)
                {
                    savedgoldfix = new Tuple<int, int>(currentrank, currentrank + RankGap - 1);
                    currentrank += RankGap;
                }
            }
        }

        public VersusLeagueData GetLeagueData(LeaguesEnum type)
        {
            return LeaguesInformations.GetOrDefault(type);
        }

        //public void ComputeArenaLeagueByRank(Character character)
        //{
        //    var currentLeague = character.ArenaLeague;
        //    var NewLeague = Leagues.FirstOrDefault(x => x.MinRequiredRank <= character.VersusRank && character.VersusRank <= x.MaxRequiredRank);

        //    if (NewLeague.Type < currentLeague.Type)
        //    {
        //        var firstleague = GetLeaguesByType(currentLeague.Type).OrderBy(x => x.MinRequiredRank).FirstOrDefault();
        //        character.PlayerData.LeagueId = firstleague.LeagueId;
        //    }
        //    else
        //    {
        //        character.PlayerData.LeagueId = NewLeague.LeagueId;
        //    }
        //}

        public SelfRunningTaskPool ArenaTaskPool { get; } = new SelfRunningTaskPool(ArenaUpdateInterval, "Versus");

        public Dictionary<int, ArenaRecord> Arenas { get; private set; }

        public bool CanJoinQueue(Character character)
        {
            if (Arenas.Count == 0)
                return false;

            //Already in queue
            if (IsInQueue(character))
                return false;

            return character.CanEnterArena();
        }

        public bool IsInQueue(Character character) => m_queue.Exists(x => x.Character == character);

        public bool IsInQueue(ArenaParty party) => m_queue.Exists(x => x.Party == party);

        public ArenaQueueMember GetQueueMember(Character character) => m_queue.FirstOrDefault(x => x.Character == character);

        //public void AddToQueue(Character character)
        //{
        //    if (!CanJoinQueue(character))
        //        return;

        //    lock (m_queue)
        //        m_queue.Add(new ArenaQueueMember(character));

        //    ContextHandler.SendGameRolePlayArenaRegistrationStatusMessage(character.Client, true,
        //        PvpArenaStepEnum.ARENA_STEP_REGISTRED, PvpArenaTypeEnum.ARENA_TYPE_1VS1);
        //}

        public void RemoveFromQueue(Character character)
        {
            lock (m_queue)
                m_queue.RemoveAll(x => x.Character == character);

            ContextHandler.SendGameRolePlayArenaRegistrationStatusMessage(character.Client, false,
                PvpArenaStepEnum.ARENA_STEP_UNREGISTER, PvpArenaTypeEnum.ARENA_TYPE_1VS1);
        }

        //public void ComputeMatchmaking()
        //{
        //    List<ArenaQueueMember> queue;
        //    lock (m_queue)
        //    {
        //        queue = m_queue.Where(x => !x.IsBusy()).ToList();
        //    }

        //    ArenaQueueMember current;
        //    while ((current = queue.FirstOrDefault()) != null)
        //    {
        //        queue.Remove(current);

        //        var matchs = queue.Where(x => x.IsCompatibleWithVersus(current)).ToList();
        //        var allies = new List<ArenaQueueMember> { current };
        //        var enemies = new List<ArenaQueueMember>();

        //        var missingAllies = MaxPlayersPerFights - current.MembersCount;
        //        var i = 0;
        //        while (missingAllies > 0 && i < matchs.Count)
        //        {
        //            if (matchs[i].MembersCount <= missingAllies)
        //            {
        //                allies.Add(matchs[i]);
        //                missingAllies -= matchs[i].MembersCount;
        //                matchs.Remove(matchs[i]);
        //            }
        //            else
        //                i++;
        //        }

        //        if (missingAllies > 0)
        //            continue;

        //        var missingEnemies = MaxPlayersPerFights;
        //        i = 0;
        //        while (missingEnemies > 0 && i < matchs.Count)
        //        {
        //            if (matchs[i].MembersCount <= missingEnemies)
        //            {
        //                enemies.Add(matchs[i]);
        //                missingEnemies -= matchs[i].MembersCount;
        //                matchs.Remove(matchs[i]);
        //            }
        //            else
        //                i++;
        //        }

        //        if (missingEnemies > 0)
        //            continue;

        //        // start fight
        //        StartFight(allies, enemies);

        //        queue.RemoveAll(x => allies.Contains(x) || enemies.Contains(x));
        //        lock (m_queue)
        //            m_queue.RemoveAll(x => allies.Contains(x) || enemies.Contains(x));
        //    }
        //}

        //void StartFight(IEnumerable<ArenaQueueMember> team1, IEnumerable<ArenaQueueMember> team2)
        //{
        //    var arena = Arenas.RandomElementOrDefault().Value;
        //    var preFight = FightManager.Instance.CreateArenaPreFight(arena, PvpArenaTypeEnum.ARENA_TYPE_1VS1);

        //    foreach (var character in team1.SelectMany(x => x.EnumerateCharacters()))
        //    {
        //        character.DenyAllInvitations(PartyTypeEnum.PARTY_TYPE_ARENA);
        //        preFight.DefendersTeam.AddCharacter(character);
        //    }

        //    foreach (var character in team2.SelectMany(x => x.EnumerateCharacters()))
        //    {
        //        character.DenyAllInvitations(PartyTypeEnum.PARTY_TYPE_ARENA);
        //        preFight.ChallengersTeam.AddCharacter(character);
        //    }
        //    preFight.arenatype = PvpArenaTypeEnum.ARENA_TYPE_1VS1;
        //    preFight.ShowPopups();
        //}

        public ArenaLeague GetNextLeague(ArenaLeague league) => Leagues.FirstOrDefault(x => x.LeagueId == league.NextLeagueId);

        public ArenaLeague GetLeague(int leagueId) => Leagues.FirstOrDefault(x => x.LeagueId == leagueId);

        public List<ArenaLeague> GetLeaguesByType(LeaguesEnum type) => Leagues.Where(x => x.Type == type).ToList();
    }
}