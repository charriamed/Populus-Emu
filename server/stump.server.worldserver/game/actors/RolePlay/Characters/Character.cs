﻿using MongoDB.Bson;
using Stump.Core.Attributes;
using Stump.Core.Collections;
using Stump.Core.Extensions;
using Stump.Core.Threading;
using Stump.Server.WorldServer.Game.Dopple;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.IPC.Messages;
using Stump.Server.BaseServer.IPC.Objects;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Accounts;
using Stump.Server.WorldServer.Database.Breeds;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Accounts;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Merchants;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Mounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.TaxCollectors;
using Stump.Server.WorldServer.Game.Actors.Stats;
using Stump.Server.WorldServer.Game.Arena;
using Stump.Server.WorldServer.Game.Breeds;
using Stump.Server.WorldServer.Game.Dialogs;
using Stump.Server.WorldServer.Game.Dialogs.Interactives;
using Stump.Server.WorldServer.Game.Dialogs.Merchants;
using Stump.Server.WorldServer.Game.Dialogs.Npcs;
using Stump.Server.WorldServer.Game.Exchanges;
using Stump.Server.WorldServer.Game.Exchanges.Trades;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Players;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Items.BidHouse;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Items.Player.Custom;
using Stump.Server.WorldServer.Game.Jobs;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Pathfinding;
using Stump.Server.WorldServer.Game.Notifications;
using Stump.Server.WorldServer.Game.Parties;
using Stump.Server.WorldServer.Game.Shortcuts;
using Stump.Server.WorldServer.Game.Social;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Basic;
using Stump.Server.WorldServer.Handlers.Characters;
using Stump.Server.WorldServer.Handlers.Chat;
using Stump.Server.WorldServer.Handlers.Compass;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.WorldServer.Handlers.Context.RolePlay;
using Stump.Server.WorldServer.Handlers.Context.RolePlay.Party;
using Stump.Server.WorldServer.Handlers.Guilds;
using Stump.Server.WorldServer.Handlers.Initialization;
using Stump.Server.WorldServer.Handlers.Interactives;
using Stump.Server.WorldServer.Handlers.Inventory;
using Stump.Server.WorldServer.Handlers.Moderation;
using Stump.Server.WorldServer.Handlers.Titles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Stump.Server.WorldServer.Database.Npcs.Actions;
using Stump.Server.WorldServer.Database.Quests;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Quests;
using Stump.Server.WorldServer.Database.Mounts;
using Stump.Server.WorldServer.Database.Social;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Interactives.Skills;
using Stump.Server.WorldServer.Handlers.Mounts;
using GuildMember = Stump.Server.WorldServer.Game.Guilds.GuildMember;
using Stump.Server.WorldServer.Game.Idols;
using Stump.Server.WorldServer.Handlers.PvP;
using Stump.Server.BaseServer;
using Stump.Server.WorldServer.Database.Companion;
using Stump.Server.WorldServer.Game.Companions;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Conditions.Criterions;
using Stump.Server.WorldServer.Game.HavenBags;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Database.Arena;
using Stump.Server.WorldServer.Game.Exchanges.Paddock;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Achievements;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    public sealed class Character : Humanoid, IStatsOwner, IInventoryOwner, ICommandsUser
    {
        [Variable]
        public static ushort HonorLimit = 20000;
        public bool battleFieldOn = false;
        public Map followMap = null;
        public Map nextMap = null;
        public List<Map> followHistory = null;

        public List<CharacterSpellModification> SpellsModifications = new List<CharacterSpellModification>();

        readonly CharacterRecord m_record;

        bool m_recordLoaded;

        public Character(CharacterRecord record, WorldClient client)
        {
            m_record = record;
            Client = client;
            SaveSync = new object();
            LoggoutSync = new object();
            Status = new PlayerStatus((sbyte)PlayerStatusEnum.PLAYER_STATUS_AVAILABLE);
        }

        #region Events

        public void onFollowMap(Map follow)
        {
            this.followMap = follow;
            this.followHistory = new List<Map>();
            this.findFollowMap();
        }

        public void UndoFollow()
        {
            this.followMap = null;
            this.NextMap = null;
            this.followHistory = null;
            this.StopMove();
        }

        internal static void LosExperience(object experienceFightDelta)
        {
            throw new NotImplementedException();
        }

        public void checkFollowMap()
        {
            if (this.NextMap != null)
                ContextRoleplayHandler.HandleChangeMapMessage(this.Client, new ChangeMapMessage(this.NextMap.Id, false));
            if (this.Map == this.followMap)
                this.UndoFollow();
            else
                this.findFollowMap();
        }


        public void tryMove(MapNeighbour state)
        {
            MapNeighbour moveState = MapNeighbour.None;
            if (state == MapNeighbour.Top)
                moveState = MapNeighbour.Bottom;
            else if (state == MapNeighbour.Bottom)
                moveState = MapNeighbour.Top;
            else if (state == MapNeighbour.Right)
                moveState = MapNeighbour.Left;
            else if (state == MapNeighbour.Left)
                moveState = MapNeighbour.Right;

            var cells = MapPoint.GetBorderCells(moveState).Where(x => this.Map.GetCell(x.CellId).Walkable);
            if (cells.Count() > 0)
            {
                MapPoint selected = cells.RandomElementOrDefault();
                var pathfinder = new Pathfinder(this.Map.CellsInfoProvider);
                this.StartMove(pathfinder.FindPath(this.Cell.Id, selected.CellId, true));

                this.StartMove(pathfinder.FindPath(this.Cell.Id, selected.CellId, true));
                //idk why its not working on one time ?
            }
        }


        public Map followOnX(bool negative)
        {
            if (negative)
            {
                var maps = World.Instance.GetMaps(this.Map, this.Map.Position.X - 1, this.Map.Position.Y).ToList();

                if (maps.Count() > 0)
                {
                    this.NextMap = maps[0];
                    return maps[0];
                }
            }
            else
            {
                var maps = World.Instance.GetMaps(this.Map, this.Map.Position.X + 1, this.Map.Position.Y).ToList();
                if (maps.Count() > 0)
                {
                    this.NextMap = maps[0];
                    return maps[0];
                }
            }
            return null;
        }

        public Map followOnY(bool negative)
        {
            if (negative)
            {
                var maps = World.Instance.GetMaps(this.Map, this.Map.Position.X, this.Map.Position.Y - 1).ToList();
                if (maps.Count() > 0)
                {
                    this.NextMap = maps[0];
                    return maps[0];
                }
            }
            else
            {
                var maps = World.Instance.GetMaps(this.Map, this.Map.Position.X, this.Map.Position.Y + 1).ToList();
                if (maps.Count() > 0)
                {
                    var cells = MapPoint.GetBorderCells(MapNeighbour.Left).Where(x => this.Map.GetCell(x.CellId).Walkable).ToList();
                    this.NextMap = maps[0];
                    return maps[0];
                }
            }
            return null;
        }

        public void findFollowMap()
        {
            Map nextMap = null;
            if (this.followMap != null)
            {
                if (this.Map.Position.X != this.followMap.Position.X)
                {
                    if (this.followMap.Position.X < this.Map.Position.X)
                        nextMap = followOnX(true);
                    else
                        nextMap = followOnX(false);
                }
                else
                {
                    if (this.followMap.Position.Y < this.Map.Position.Y)
                        nextMap = followOnY(true);
                    else
                        nextMap = followOnY(false);
                }

                if (nextMap != null && !this.followHistory.Contains(nextMap))
                {
                    var neighbourState = this.Map.GetClientMapRelativePosition(nextMap.Id);
                    //this.SendServerMessage("We are now going to " + nextMap.Position.X + ", " + nextMap.Position.Y);
                    this.followHistory.Add(nextMap);

                    this.tryMove(neighbourState);
                }
            }
        }

        public event Action<Character> LoggedIn;

        public bool IsFirstConnection
        {
            get { return Record.FirstConnection; }
            set
            {
                Record.FirstConnection = value;
            }
        }

        public int ChallengesCount
        {
            get { return Record.ChallengesCount; }
            set { Record.ChallengesCount = value; }
        }

        public int ChallengesInDungeonCount
        {
            get { return Record.ChallengesInDungeonCount; }
            set { Record.ChallengesInDungeonCount = value; }
        }

        public int OwnedRuneAmount
        {
            get { return Record.OwnedRuneAmount; }
            set { Record.OwnedRuneAmount = value; }
        }

        public int VipRank
        {
            get { return m_record.VipRank; }
            private set { m_record.VipRank = value; }
        }
        public int VipRank2
        {
            get { return m_record.VipRank2; }
            private set { m_record.VipRank2 = value; }
        }
        public int VipRank3
        {
            get { return m_record.VipRank3; }
            private set { m_record.VipRank3 = value; }
        }
        public int VipRank4
        {
            get { return m_record.VipRank4; }
            private set { m_record.VipRank4 = value; }
        }

        void OnLoggedIn()
        {

            if (GuildMember != null)
            {
                GuildMember.OnCharacterConnected(this);

                if (Guild.MotdContent != null)
                    GuildHandler.SendGuildMotdMessage(Client, Guild);

                if (Guild.BulletinContent != null)
                    GuildHandler.SendGuildBulletinMessage(Client, Guild);
            }
            else
                RemoveEmote(EmotesEnum.EMOTE_GUILD);

            //Arena
            CheckArenaDailyProperties_1vs1();
            CheckArenaDailyProperties_3vs3();
            var character = (Client as WorldClient).Character;

            if (VipRank > 0 && VipRank < 2)
            {
                character.OpenPopup("Félicitation, vous êtes maintenant VIP Silver");
                VipRank++;
                var VipItem3 = ItemManager.Instance.TryGetTemplate(13032); ; // ID ITEMS A DONNER
                Client.Character.Inventory.AddItem(VipItem3, 1);
                character.SendServerMessage("Tu as gagné bowlton de Cuivre  Clique sur Lui S'il vous plait.");
                character.OpenPopup("Félicitation d'être devenu VIP , Tu obtiens un bonus d'xp 25% et d'autres cadeaux.");
            }

            if (VipRank2 > 0 && VipRank2 < 2 && VipRank < 1)
            {

                character.OpenPopup("Félicitation, vous êtes maintenant VIP Fer");
                VipRank2++;
                var VipItem4 = ItemManager.Instance.TryGetTemplate(13023); // ID ITEMS A DONNER
                character.Inventory.AddItem(VipItem4, 1);
                character.SendServerMessage("Tu as gagné bowlton de Fer Clique sur Lui S'il vous plait .");
                character.SendServerMessage("Félicitation d'être devenu VIP , Tu obtiens un bonus d'xp de 50% et d'autres cadeaux.");
            }
            if (VipRank3 > 0 && VipRank3 < 2 && VipRank2 < 1 && VipRank < 1)
            {
                character.OpenPopup("Félicitation, vous êtes maintenant VIP Gold");
                VipRank3++;
                var VipItem6 = ItemManager.Instance.TryGetTemplate(13026); // ID ITEMS A DONNER
                Inventory.AddItem(VipItem6, 1);
                character.SendServerMessage("Tu as gagné bowlton d'or Clique sur Lui S'il vous plait .");
                character.SendServerMessage("Félicitation d'être devenu VIP , Tu obtiens un bonus d'xp de 75% et d'autres cadeaux.");


            }
            if (VipRank4 > 0 && VipRank4 < 2 && VipRank3 < 1 && VipRank2 < 1 && VipRank < 1)
            {
                character.OpenPopup("Félicitation, vous êtes maintenant VIP Diamond");
                VipRank4++;
                var VipItem6 = ItemManager.Instance.TryGetTemplate(10275); // ID ITEMS A DONNER
                Inventory.AddItem(VipItem6, 1);
                character.SendServerMessage("Tu as gagné bowlton d'or Clique sur Lui S'il vous plait .");
                character.SendServerMessage("Félicitation d'être devenu VIP , Tu obtiens un bonus d'xp de 100% et d'autres cadeaux.");


            }


            #region ZAAP a la creation joueur
            if (this.KnownZaaps.Count == 0)
            {
                var maps = World.Instance.GetMaps();
                foreach (var map in maps)
                {
                    if (map.Zaap != null)
                        this.DiscoverZaap(map, false);
                }
            }
            #endregion

            #region 
            #endregion

            //TROUSSEAU
            if (DateTime.Now > Record.CreationDate.AddDays(3d))
            {
                Record.DungeonDone.Clear();
                Record.CreationDate = DateTime.Now;
            }

            OnPlayerLifeStatusChanged(PlayerLifeStatus);

            if (!IsGhost())
            {
                var energyGain = (short)(DateTime.Now - Record.LastUsage.Value).TotalMinutes;

                energyGain = (short)((Energy + energyGain) > EnergyMax ? (EnergyMax - Energy) : energyGain);

                if (energyGain > 0)
                {
                    Energy += energyGain;

                    RefreshStats();

                    //Vous avez récupéré <b>%1</b> points d'énergie.
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 7, energyGain);
                }
            }

            Record.LastUsage = DateTime.Now;

            var document = new BsonDocument
            {
                { "AcctId", Account.Id },
                { "AcctName", Account.Login },
                { "CharacterId", Id },
                { "CharacterName", Name },

                { "IPAddress", Client.IP },
                { "Action", "Login" },
                { "Date", DateTime.Now.ToString(CultureInfo.InvariantCulture) }
            };

            //MongoLogger.Instance.Insert("characters_connections", document);

            LoggedIn?.Invoke(this);
        }

        public event Action<Character> LoggedOut;

        void OnLoggedOut()
        {
            EnterMap -= OnFollowedMemberEnterMap;

            if (FriendsBook != null)
                FriendsBook.CheckDC(); // attempt to resolve leaks

            if (Fight != null && (Fight.State == FightState.Placement || Fight.State == FightState.Fighting))
                Record.LeftFightId = Fight.Id;
            else
                Record.LeftFightId = null;

            if (GuildMember != null)
                GuildMember.OnCharacterDisconnected(this);

            if (TaxCollectorDefendFight != null)
                TaxCollectorDefendFight.RemoveDefender(this);

            if (ArenaManager.Instance.IsInQueue(this))
                ArenaManager.Instance.RemoveFromQueue(this);

            if (ArenaPopup != null)
                ArenaPopup.Deny();

            if (Jobs != null)
                foreach (var job in Jobs.Where(x => x.IsIndexed))
                    job.Template.RemoveAvaiableCrafter(this);

            var document = new BsonDocument
            {
                { "AcctId", Client.Account.Id },
                { "AcctName", Client.Account.Login },
                { "CharacterId", Id },
                { "CharacterName", Name },
                { "IPAddress", Client.IP },
                { "Action", "Loggout" },
                { "Date", DateTime.Now.ToString(CultureInfo.InvariantCulture) }
            };

            // MongoLogger.Instance.Insert("characters_connections", document);

            Record.LastUsage = DateTime.Now;

            LoggedOut?.Invoke(this);
        }

        public event Action<Character> Saved;

        public void OnSaved()
        {
            IsAuthSynced = true;
            UnBlockAccount();

            Saved?.Invoke(this);
        }

        public event Action<Character, int> LifeRegened;

        private void OnLifeRegened(int regenedLife)
        {
            LifeRegened?.Invoke(this, regenedLife);
        }

        public event Action<Character> AccountUnblocked;

        private void OnAccountUnblocked()
        {
            AccountUnblocked?.Invoke(this);
        }

        public event Action<Character> LookRefreshed;

        private void OnLookRefreshed()
        {
            LookRefreshed?.Invoke(this);
        }

        public event Action<Character> StatsResfreshed;

        private void OnStatsResfreshed()
        {
            StatsResfreshed?.Invoke(this);
        }

        public event Action<Character, Npc, NpcActionTypeEnum, NpcAction> InteractingWith;

        public void OnInteractingWith(Npc npc, NpcActionTypeEnum actionType, NpcAction action)
        {
            InteractingWith?.Invoke(this, npc, actionType, action);
        }
        #endregion Events

        #region Properties

        public WorldClient Client
        {
            get;
        }

        public string _timeTarget
        {
            get;
            set;
        }

        public int _divisor
        {
            get;
            set;
        }

        public DateTime _date
        {

            get;
            set;
        }

        public string CharacterToSeekName
        {
            get { return Record.CharacterToSeekName; }
            set { Record.CharacterToSeekName = value; }
        }

        public AccountData Account
        {
            get { return Client.Account; }
        }

        public WorldAccount WorldAccount
        {
            get { return Client.WorldAccount; }
        }


        public UserGroup UserGroup
        {
            get
            {
                return Client.UserGroup;
            }
        }

        public object SaveSync
        {
            get;
            private set;
        }

        public object LoggoutSync
        {
            get;
            private set;
        }

        private bool m_inWorld;

        public override bool IsInWorld
        {
            get
            {
                return m_inWorld;
            }
        }

        public CharacterMerchantBag MerchantBag
        {
            get;
            private set;
        }

        public Map MapBattleField
        {
            get;
            set;
        }

        public Cell CellBattleField
        {
            get;
            set;
        }

        #region Commands
        public bool ForcePassTurn
        {
            get;
            set;
        }

        public bool isMultiLeadder
        {
            get;
            set;
        }

        public bool IsIpDrop
        {
            get;
            set;
        }
        #endregion

        #region Incarnation

        public bool IsInIncarnation
        {
            get
            {
                return Record.IsInIncarnation;
            }
            set
            {
                Record.IsInIncarnation = value;
            }
        }

        public int IncarnationId
        {
            get
            {
                return Record.IncarnationId;
            }
            set
            {
                Record.IncarnationId = value;
            }
        }

        #endregion

        #region Battlefield
        public void updateBattleFieldPosition()
        {
            this.MapBattleField = this.Map;
            this.CellBattleField = this.Cell;
        }
        #endregion

        #region Achievement
        public PlayerAchievement Achievement { get; private set; }

        #endregion

        #region Identifier

        public override string Name
        {
            get
            {
                return (RoleEnum)Account.UserGroupId >= RoleEnum.Moderator ? $"[{m_record.Name}]" : m_record.Name;

            }
            protected set
            {

                m_record.Name = value;
                base.Name = value;
            }
        }



        public override int Id
        {
            get { return m_record.Id; }
            protected set
            {
                m_record.Id = value;
                base.Id = value;
            }
        }

        #endregion Identifier

        #region Inventory

        public Inventory Inventory
        {
            get;
            private set;
        }

        public ulong Kamas
        {
            get { return Record.Kamas; }
            set { Record.Kamas = value; }
        }

        #endregion Inventory

        #region Jobs

        public JobsCollection Jobs
        {
            get;
            private set;
        }

        public event Action<ItemTemplate, int> HarvestItem;

        public void OnHarvestItem(ItemTemplate item, int quantity)
        {
            HarvestItem?.Invoke(item, quantity);
        }

        public event Action<BasePlayerItem, int> CraftItem;

        public void OnCraftItem(BasePlayerItem item, int quantity)
        {
            CraftItem?.Invoke(item, quantity);
        }

        public event Action<ItemTemplate, int> DecraftItem;

        public void OnDecraftItem(ItemTemplate item, int runeQuantity)
        {
            OwnedRuneAmount += runeQuantity;
            DecraftItem?.Invoke(item, runeQuantity);
        }

        #endregion Jobs

        #region Interactives

        public InteractiveObject CurrentUsedInteractive => CurrentUsedSkill?.InteractiveObject;

        public Skill CurrentUsedSkill
        {
            get;
            private set;
        }

        public void SetCurrentSkill(Skill skill)
        {
            CurrentUsedSkill = skill;
        }

        public void ResetCurrentSkill()
        {
            CurrentUsedSkill = null;
        }

        #endregion

        #region Position

        public override ICharacterContainer CharacterContainer
        {
            get
            {
                if (IsFighting())
                    return Fight;

                return Map;
            }
        }


        #endregion Position

        #region Dialog

        private IDialoger m_dialoger;

        public IDialoger Dialoger
        {
            get { return m_dialoger; }
            private set
            {
                m_dialoger = value;
                m_dialog = value != null ? m_dialoger.Dialog : null;
            }
        }

        private IDialog m_dialog;

        public IDialog Dialog
        {
            get { return m_dialog; }
            private set
            {
                m_dialog = value;
                if (m_dialog == null)
                    m_dialoger = null;
            }
        }

        public NpcShopDialogLogger NpcShopDialog => Dialog as NpcShopDialogLogger;

        public ZaapDialog ZaapDialog => Dialog as ZaapDialog;

        public ZaapiDialog ZaapiDialog => Dialog as ZaapiDialog;

        public DungsDialog DungsDialog => Dialog as DungsDialog;

        public MerchantShopDialog MerchantShopDialog => Dialog as MerchantShopDialog;

        public RequestBox RequestBox
        {
            get;
            private set;
        }

        public void SetDialoger(IDialoger dialoger)
        {
            if (Dialog != null)
                Dialog.Close();

            Dialoger = dialoger;
        }

        public void SetDialog(IDialog dialog)
        {
            if (Dialog != null)
                Dialog.Close();

            Dialog = dialog;
        }

        public void CloseDialog(IDialog dialog)
        {
            if (Dialog == dialog)
                Dialoger = null;
        }

        public void ResetDialog()
        {
            Dialoger = null;
        }

        public void OpenRequestBox(RequestBox request)
        {
            RequestBox = request;
        }

        public void ResetRequestBox()
        {
            RequestBox = null;
        }

        public bool IsBusy() => IsInRequest() || IsDialoging();

        public bool IsDialoging() => Dialog != null;

        public bool IsInRequest() => RequestBox != null;

        public bool IsRequestSource() => IsInRequest() && RequestBox.Source == this;

        public bool IsRequestTarget() => IsInRequest() && RequestBox.Target == this;

        public bool IsTalkingWithNpc() => Dialog is NpcDialog;

        public bool IsInZaapDialog() => Dialog is ZaapDialog;

        public bool IsInZaapiDialog() => Dialog is ZaapiDialog;

        public bool IsInDungsDialog() => Dialog is DungsDialog;

        #endregion Dialog

        #region Party

        private readonly Dictionary<int, PartyInvitation> m_partyInvitations
            = new Dictionary<int, PartyInvitation>();

        private readonly List<PartyTypeEnum> m_partiesLoyalTo = new List<PartyTypeEnum>();

        private Character m_followedCharacter;

        private Party m_party;
        private ArenaParty m_arenaParty;

        public Party Party
        {
            get { return m_party; }
            private set
            {
                if (m_party != null && value != m_party) SetLoyalToParty(PartyTypeEnum.PARTY_TYPE_CLASSICAL, false);
                m_party = value;
            }
        }

        public ArenaParty ArenaParty
        {
            get { return m_arenaParty; }
            private set
            {
                if (m_arenaParty != null && value != m_arenaParty) SetLoyalToParty(PartyTypeEnum.PARTY_TYPE_ARENA, false);
                m_arenaParty = value;
            }
        }

        public Party[] Parties => new[] { Party, ArenaParty }.Where(x => x != null).ToArray();

        public bool IsInParty()
        {
            return Party != null || ArenaParty != null;
        }

        public bool IsInParty(int id)
        {
            return (Party != null && Party.Id == id) || (ArenaParty != null && ArenaParty.Id == id);
        }

        public bool IsInParty(PartyTypeEnum type)
        {
            return (type == PartyTypeEnum.PARTY_TYPE_CLASSICAL && Party != null) || (type == PartyTypeEnum.PARTY_TYPE_ARENA && ArenaParty != null);
        }

        public bool IsPartyLeader()
        {
            return Party?.Leader == this;
        }

        public bool IsPartyLeader(int id)
        {
            return GetParty(id)?.Leader == this;
        }

        public Party GetParty(int id)
        {
            if (Party != null && Party.Id == id)
                return Party;

            if (ArenaParty != null && ArenaParty.Id == id)
                return ArenaParty;

            return null;
        }

        public bool IsLoyalToParty(PartyTypeEnum type) => m_partiesLoyalTo.Contains(type);
        public void SetLoyalToParty(PartyTypeEnum type, bool loyal)
        {
            if (loyal) m_partiesLoyalTo.Add(type); else m_partiesLoyalTo.Remove(type);

            PartyHandler.SendPartyLoyaltyStatusMessage(Client, GetParty(type), loyal);
        }

        public Party GetParty(PartyTypeEnum type)
        {
            switch (type)
            {
                case PartyTypeEnum.PARTY_TYPE_CLASSICAL:
                    return Party;

                case PartyTypeEnum.PARTY_TYPE_ARENA:
                    return ArenaParty;

                default:
                    throw new NotImplementedException(string.Format("Cannot manage party of type {0}", type));
            }
        }

        public void SetParty(Party party)
        {
            switch (party.Type)
            {
                case PartyTypeEnum.PARTY_TYPE_CLASSICAL:
                    Party = party;
                    break;

                case PartyTypeEnum.PARTY_TYPE_ARENA:
                    ArenaParty = (ArenaParty)party;
                    break;

                default:
                    logger.Error("Cannot manage party of type {0} ({1})", party.GetType(), party.Type);
                    break;
            }
        }

        public void ResetParty(PartyTypeEnum type)
        {
            switch (type)
            {
                case PartyTypeEnum.PARTY_TYPE_CLASSICAL:
                    Party = null;
                    break;

                case PartyTypeEnum.PARTY_TYPE_ARENA:
                    ArenaParty = null;
                    break;

                default:
                    logger.Error("Cannot manage party of type {0}", type);
                    break;
            }

            CompassHandler.SendCompassResetMessage(Client, CompassTypeEnum.COMPASS_TYPE_PARTY);
        }

        #endregion Party

        #region Trade

        public IExchange Exchange
        {
            get { return Dialog as IExchange; }
        }

        public Exchanger Exchanger => Dialoger as Exchanger;

        public ITrade Trade
        {
            get { return Dialog as ITrade; }
        }

        public PlayerTrade PlayerTrade
        {
            get { return Trade as PlayerTrade; }
        }

        public Trader Trader
        {
            get { return Dialoger as Trader; }
        }

        public bool IsInExchange()
        {
            return Exchanger != null;
        }

        public bool IsTrading()
        {
            return Trade != null;
        }

        public bool IsTradingWithPlayer()
        {
            return PlayerTrade != null;
        }

        #endregion Trade

        #region Idols

        public IdolInventory IdolInventory
        {
            get;
            set;
        }

        #endregion

        #region BreedsItemsSpellsModifications
        public void SpellRangeableEnable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.RangeableEnable = true;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.RANGEABLE, (ushort)spellId, new CharacterBaseCharacteristic(0, 1, 0, 0, 1));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void SpellRangeableDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.RangeableEnable = false;
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.RANGEABLE);
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s.FirstOrDefault());
                }
                catch { }
                RefreshStats();
            }
        }

        public void SpellObstaclesDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.SpellObstaclesDisable = true;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.LOS, (ushort)spellId, new CharacterBaseCharacteristic(0, 1, 0, 0, 1));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void SpellObstaclesDisable(Spell spell)
        {
            spell.SpellObstaclesDisable = true;
            CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.LOS, (ushort)spell.Template.Id, new CharacterBaseCharacteristic(0, 1, 0, 0, 1));
            SpellsModifications.Add(s);
        }

        public void SpellObstaclesEnable(Spell spell)
        {
            if (spell != null)
            {
                spell.SpellObstaclesDisable = false;
                var s = SpellsModifications.FirstOrDefault(x => x.SpellId == spell.Template.Id && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.LOS);
                if (s != null)
                    SpellsModifications.Remove(s);
            }
        }

        public void SpellObstaclesEnable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.SpellObstaclesDisable = false;
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.LOS);
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s.FirstOrDefault());
                }
                catch { }
                RefreshStats();
            }
        }

        public void LineCastDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.LineCastDisable = true;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.CAST_LINE, (ushort)spellId, new CharacterBaseCharacteristic(0, 1, 0, 0, 1));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void LineCastEnable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.LineCastDisable = false;
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.CAST_LINE);
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s.FirstOrDefault());
                }
                catch { }
                RefreshStats();
            }
        }

        public void ReduceSpellCost(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var v = SpellsModifications.FirstOrDefault(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.AP_COST);
                if (v != null)
                {
                    v.Value.Additionnal += (short)amount;
                    spell.ApCostReduction += amount;
                    RefreshStats();
                    return;
                }
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.AP_COST, (ushort)spellId, new CharacterBaseCharacteristic(0, (short)amount, 0, 0, 0));
                SpellsModifications.Add(s);
                spell.ApCostReduction += amount;
                RefreshStats();
            }
        }

        public void SpellCostDisable(short spellId, short boost)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.AP_COST).FirstOrDefault();

                try
                {
                    if (s != null)
                    {
                        s.Value.Additionnal -= boost;
                        spell.ApCostReduction -= (uint)boost;
                        if (s.Value.Additionnal <= 0)
                        {
                            SpellsModifications.Remove(s);
                        }
                    }
                }
                catch { }
                RefreshStats();
            }
        }

        public void IncreaseRange(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.AdditionalRange += (int)amount;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.RANGE, (ushort)spellId, new CharacterBaseCharacteristic(0, 0, 0, 0, (short)amount));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void IncreaseRangeDisable(short spellId, short boost)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.RANGE).FirstOrDefault();

                try
                {
                    if (s != null)
                    {
                        s.Value.ContextModif -= boost;
                        spell.AdditionalRange -= boost;
                        if (s.Value.ContextModif <= 0)
                        {
                            SpellsModifications.Remove(s);
                        }
                    }
                }
                catch { }
                RefreshStats();
            }
        }

        public void SpellRangeHandler(short spellId, short amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var v = SpellsModifications.FirstOrDefault(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.RANGE);
                if (v != null)
                {
                    v.Value.ContextModif += amount;
                    spell.AdditionalRange += amount;
                    RefreshStats();
                    return;
                }
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.RANGE, (ushort)spellId, new CharacterBaseCharacteristic(0, 0, 0, 0, amount));
                SpellsModifications.Add(s);
                spell.AdditionalRange += amount;
                RefreshStats();
            }
        }

        public void ReduceDelay(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.DelayReduction += amount;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.CAST_INTERVAL, (ushort)spellId, new CharacterBaseCharacteristic(0, (short)amount, 0, 0, 0));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void ReduceDelayDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.CAST_INTERVAL).FirstOrDefault();
                spell.DelayReduction -= (uint)s.Value.Additionnal;
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s);
                }
                catch { }
                RefreshStats();
            }
        }

        public void AddMaxCastPerTurn(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.AdditionalCastPerTurn += amount;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.MAX_CAST_PER_TURN, (ushort)spellId, new CharacterBaseCharacteristic(0, (short)amount, 0, 0, (short)amount));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void AddMaxCastPerTurnDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.MAX_CAST_PER_TURN).FirstOrDefault();
                spell.AdditionalCastPerTurn -= (uint)s.Value.Additionnal;
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s);
                }
                catch { }
                RefreshStats();
            }
        }

        public void AddMaxCastPerTarget(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.AdditionalCastPerTarget += amount;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.MAX_CAST_PER_TARGET, (ushort)spellId, new CharacterBaseCharacteristic(0, (short)amount, 0, 0, (short)amount));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void AddMaxCastPerTargetDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.MAX_CAST_PER_TARGET).FirstOrDefault();
                spell.AdditionalCastPerTarget -= (uint)s.Value.Additionnal;
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s);
                }
                catch { }
                RefreshStats();
            }
        }

        public void AddDamage(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.AdditionalDamage += amount;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.DAMAGE, (ushort)spellId, new CharacterBaseCharacteristic(0, (short)amount, 0, 0, (short)amount));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void AddDamageDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.DAMAGE).FirstOrDefault();
                spell.AdditionalDamage -= (uint)s.Value.Additionnal;
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s);
                }
                catch { }
                RefreshStats();
            }
        }
        public void SpellAddDamage(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.DAMAGE, (ushort)spellId, new CharacterBaseCharacteristic(0, (short)amount, 0, 0, (short)amount));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void SpellAddDamageDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.DAMAGE);
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s.FirstOrDefault());
                }
                catch { }
                RefreshStats();
            }
        }


        public void AddHeal(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.AdditionalHeal += amount;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.HEAL_BONUS, (ushort)spellId, new CharacterBaseCharacteristic(0, (short)amount, 0, 0, (short)amount));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void AddHealDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.HEAL_BONUS).FirstOrDefault();
                spell.AdditionalHeal -= (uint)s.Value.Additionnal;
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s);
                }
                catch { }
                RefreshStats();
            }
        }

        public void AddCritical(short spellId, uint amount)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                spell.AdditionalCriticalPercent += amount;
                CharacterSpellModification s = new CharacterSpellModification((sbyte)CharacterSpellModificationTypeEnum.CRITICAL_HIT_BONUS, (ushort)spellId, new CharacterBaseCharacteristic(0, (short)amount, 0, 0, 0));
                SpellsModifications.Add(s);
                RefreshStats();
            }
        }

        public void AddCriticalDisable(short spellId)
        {
            if (Spells == null) return; if (Spells.Where(x => x.Id == spellId) == null || Spells.Where(x => x.Id == spellId).Count() < 1) return; var spell = Spells.Where(x => x.Id == spellId).First();
            if (spell != null)
            {
                var s = SpellsModifications.Where(x => x.SpellId == spellId && x.ModificationType == (byte)CharacterSpellModificationTypeEnum.CRITICAL_HIT_BONUS).FirstOrDefault();
                spell.AdditionalCriticalPercent -= (uint)s.Value.Additionnal;
                try
                {
                    if (s != null)
                        SpellsModifications.Remove(s);
                }
                catch { }
                RefreshStats();
            }
        }

        #endregion

        #region Titles & Ornaments

        public ReadOnlyCollection<short> Titles => Record.Titles.AsReadOnly();

        public ReadOnlyCollection<short> Ornaments => Record.Ornaments.AsReadOnly();

        public short? SelectedTitle
        {
            get { return Record.TitleId; }
            private set { Record.TitleId = value; }
        }

        public bool HasTitle(short title) => Record.Titles.Contains(title);

        public void AddTitle(short title)
        {
            if (HasTitle(title))
                return;

            Record.Titles.Add(title);
            TitleHandler.SendTitleGainedMessage(Client, title);
        }

        public bool RemoveTitle(short title)
        {
            var result = Record.Titles.Remove(title);

            if (result)
                TitleHandler.SendTitleLostMessage(Client, title);

            if (title == SelectedTitle)
                ResetTitle();

            return result;
        }

        public bool SelectTitle(short title)
        {
            if (!HasTitle(title))
                return false;

            SelectedTitle = title;
            TitleHandler.SendTitleSelectedMessage(Client, title);
            RefreshActor();
            return true;
        }

        public void ResetTitle()
        {
            SelectedTitle = null;
            TitleHandler.SendTitleSelectedMessage(Client, 0);
            RefreshActor();
        }

        public short? SelectedOrnament
        {
            get
            {
                return Record.Ornament;
            }
            private set
            {
                Record.Ornament = value;
            }
        }

        public bool HasOrnament(short ornament)
        {
            return Record.Ornaments.Contains(ornament);
        }

        public void AddOrnament(short ornament)
        {
            if (!HasOrnament(ornament))
                Record.Ornaments.Add(ornament);

            TitleHandler.SendOrnamentGainedMessage(Client, ornament);
        }

        public bool RemoveOrnament(short ornament)
        {
            var result = Record.Ornaments.Remove(ornament);

            if (result)
                TitleHandler.SendTitlesAndOrnamentsListMessage(Client, this);

            if (ornament == SelectedOrnament)
                ResetOrnament();

            return result;
        }

        public void RemoveAllOrnament()
        {
            Record.Ornaments.Clear();
            TitleHandler.SendTitlesAndOrnamentsListMessage(Client, this);
        }

        public bool SelectOrnament(short ornament)
        {
            if (!HasOrnament(ornament))
                return false;

            SelectedOrnament = ornament;
            TitleHandler.SendOrnamentSelectedMessage(Client, ornament);
            RefreshActor();
            return true;
        }

        public void ResetOrnament()
        {
            SelectedOrnament = null;
            TitleHandler.SendOrnamentSelectedMessage(Client, 0);
            RefreshActor();
        }

        #endregion Titles & Ornaments

        #region Apparence

        public bool CustomLookActivated
        {
            get { return m_record.CustomLookActivated; }
            set { m_record.CustomLookActivated = value; }
        }

        public ActorLook CustomLook
        {
            get { return m_record.CustomEntityLook; }
            set { m_record.CustomEntityLook = value; }
        }

        public ActorLook DefaultLook
        {
            get { return m_record.DefaultLook; }
            set
            {
                m_record.DefaultLook = value;

                UpdateLook();
            }
        }

        public override ActorLook Look
        {
            get { return CustomLookActivated ? CustomLook : m_look; }
            set
            {
                m_look = value;
                m_record.LastLook = value;
            }
        }

        public override SexTypeEnum Sex
        {
            get { return m_record.Sex; }
            protected set { m_record.Sex = value; }
        }

        public PlayableBreedEnum BreedId
        {
            get { return m_record.Breed; }
            private set
            {
                m_record.Breed = value;
                Breed = BreedManager.Instance.GetBreed(value);
            }
        }

        public Breed Breed
        {
            get;
            private set;
        }

        public Head Head
        {
            get;
            set;
        }

        public bool Invisible
        {
            get;
            private set;
        }

        public PlayerStatus Status
        {
            get;
            private set;
        }

        public void SetStatus(PlayerStatusEnum status)
        {
            if (Status.StatusId == (sbyte)status)
                return;

            Status = new PlayerStatus((sbyte)status);
            CharacterStatusHandler.SendPlayerStatusUpdateMessage(Client, Status);
        }

        public bool IsAvailable(Character character, bool msg)
        {
            if (Status.StatusId == (sbyte)PlayerStatusEnum.PLAYER_STATUS_SOLO)
                return false;

            if (Status.StatusId == (sbyte)PlayerStatusEnum.PLAYER_STATUS_PRIVATE && !FriendsBook.IsFriend(character.Account.Id))
                return false;

            if (Status.StatusId == (sbyte)PlayerStatusEnum.PLAYER_STATUS_AFK && !msg)
                return false;

            return true;
        }

        public bool ToggleInvisibility(bool toggle)
        {
            Invisible = toggle;

            if (!IsInFight())
                Map.Refresh(this);

            SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, toggle ? (short)236 : (short)237);

            return Invisible;
        }

        public bool ToggleInvisibility() => ToggleInvisibility(!Invisible);

        public void ResetDefaultLook()
        {
            var look = Breed.GetLook(Sex, true);
            look.SetColors(DefaultLook.Colors);

            foreach (var skin in Head.Skins)
                look.AddSkin(skin);

            DefaultLook = look;
        }

        public void UpdateLook(bool send = true)
        {
            var look = DefaultLook.Clone();

            look = Inventory.Where(x => x.IsEquiped()).Aggregate(look, (current, item) => item.UpdateItemSkin(current));

            switch (PlayerLifeStatus)
            {
                case PlayerLifeStatusEnum.STATUS_PHANTOM:
                    look.BonesID = 3;
                    look.AddSkin(Sex == SexTypeEnum.SEX_FEMALE ? (short)323 : (short)322);
                    look.AddSkin(Sex == SexTypeEnum.SEX_FEMALE ? Breed.FemaleGhostBonesId : Breed.MaleGhostBonesId);
                    break;
                case PlayerLifeStatusEnum.STATUS_TOMBSTONE:
                    look.BonesID = Breed.TombBonesId;
                    break;
            }

            if (IsRiding)
            {
                var mountLook = EquippedMount.Look.Clone();
                look.BonesID = 2;
                mountLook.SetRiderLook(look);

                look = mountLook;
            }
            var currentEmote = GetCurrentEmote();

            if (currentEmote != null)
            {
                look = currentEmote.UpdateEmoteLook(this, look, true);
            }

            Look = look;

            if (send)
                SendLookUpdated();
        }

        public void UpdateLook(Emote emote, bool apply, bool send = true)
        {
            Look = emote.UpdateEmoteLook(this, Look, apply);

            if (send)
                SendLookUpdated();
        }

        public void UpdateLook(BasePlayerItem item, bool send = true)
        {
            Look = item.UpdateItemSkin(Look);

            if (send)
                SendLookUpdated();
        }


        private void SendLookUpdated()
        {
            if (Fight != null)
            {
                Fighter.Look = Look.Clone();
                Fighter.Look.RemoveAuras();

                if (Fighter.IsDead() || Fighter.HasLeft())
                    return;

                ContextHandler.SendGameContextRefreshEntityLookMessage(CharacterContainer.Clients, Fighter);
            }
            else
            {
                ContextHandler.SendGameContextRefreshEntityLookMessage(CharacterContainer.Clients, this);
            }
        }

        public void RefreshActor()
        {
            if (Fight != null)
            {
                Fighter.Look = Look.Clone();
                Fighter.Look.RemoveAuras();

                Fight.Map.Area.ExecuteInContext(() =>
                    Fight.RefreshActor(Fighter));
            }
            else if (Map != null)
            {
                Map.Area.ExecuteInContext(() =>
                    Map.Refresh(this));
            }

            OnLookRefreshed();
        }

        #endregion Apparence

        #region Stats

        #region Delegates

        public delegate void LevelChangedHandler(Character character, ushort currentLevel, int difference);

        public delegate void GradeChangedHandler(Character character, sbyte currentGrade, int difference);

        #endregion Delegates

        #region Levels

        public ushort Level
        {
            get;
            private set;
        }

        public long Experience
        {
            get { return RealExperience - PrestigeRank * ExperienceManager.Instance.HighestCharacterExperience; }
            private set
            {
                RealExperience = PrestigeRank * ExperienceManager.Instance.HighestCharacterExperience + value;
                if ((value < UpperBoundExperience || Level >= ExperienceManager.Instance.HighestCharacterLevel) &&
                    value >= LowerBoundExperience) return;
                var lastLevel = Level;

                Level = ExperienceManager.Instance.GetCharacterLevel(value);

                LowerBoundExperience = ExperienceManager.Instance.GetCharacterLevelExperience(Level);
                UpperBoundExperience = ExperienceManager.Instance.GetCharacterNextLevelExperience(Level);

                var difference = Level - lastLevel;

                OnLevelChanged(Level, difference);
            }
        }

        public void LevelUp(ushort levelAdded)
        {
            ushort level;

            if (levelAdded + Level > ExperienceManager.Instance.HighestCharacterLevel)
                level = ExperienceManager.Instance.HighestCharacterLevel;
            else
                level = (ushort)(levelAdded + Level);

            var experience = ExperienceManager.Instance.GetCharacterLevelExperience(level);

            Experience = experience;
        }

        public void LevelDown(ushort levelRemoved)
        {
            ushort level;

            if (Level - levelRemoved < 1)
                level = 1;
            else
                level = (ushort)(Level - levelRemoved);

            var experience = ExperienceManager.Instance.GetCharacterLevelExperience(level);

            Experience = experience;
        }

        public void AddExperience(int amount)
        {
            Experience += amount;
        }
        #region perte xp
        public void LosExperience(int amount)
        {
            Experience -= amount;
        }

        public void LosExperience(long amount)
        {
            Experience -= amount;
        }

        public void LosExperience(double amount)
        {
            Experience -= (long)amount;
        }
        #endregion
        public void AddExperience(long amount)
        {
            Experience += amount;
        }

        public void AddExperience(double amount)
        {
            Experience += (long)amount;
        }

        #endregion Levels

        public long LowerBoundExperience
        {
            get;
            private set;
        }

        public long UpperBoundExperience
        {
            get;
            private set;
        }

        public ushort StatsPoints
        {
            get { return m_record.StatsPoints; }
            set { m_record.StatsPoints = value; }
        }

        public ushort SpellsPoints
        {
            get { return m_record.SpellsPoints; }
            set { m_record.SpellsPoints = value; }
        }

        public short EnergyMax
        {
            get { return m_record.EnergyMax; }
            set { m_record.EnergyMax = value; }
        }

        public short Energy
        {
            get { return m_record.Energy; }
            set
            {
                var energy = (short)(value < 0 ? 0 : value);
                var diff = (short)(energy - m_record.Energy);

                m_record.Energy = energy;
                OnEnergyChanged(energy, diff);
            }
        }

        public PlayerLifeStatusEnum PlayerLifeStatus
        {
            get { return m_record.PlayerLifeStatus; }
            set
            {
                m_record.PlayerLifeStatus = value;
                OnPlayerLifeStatusChanged(value);
            }
        }

        public int LifePoints
        {
            get { return Stats.Health.Total; }
        }

        public int MaxLifePoints
        {
            get { return Stats.Health.TotalMax; }
        }

        public SpellInventory Spells
        {
            get;
            private set;
        }

        public StatsFields PrivateStats
        {
            get;
            private set;
        }

        public StatsFields Stats
        {
            get
            {
                if (CustomStatsActivated) return CustomStats;
                return PrivateStats;
            }
            set
            {
                if (CustomStatsActivated) CustomStats = value;
                else
                {
                    PrivateStats = value;
                }
            }
        }
        public StatsFields CustomStats
        {
            get;
            private set;
        }
        public bool CustomStatsActivated = false;

        public bool GodMode
        {
            get;
            private set;
        }

        public bool CriticalMode
        {
            get;
            private set;
        }

        public static Dictionary<short, byte> OrnamentsEarnables = new Dictionary<short, byte>()
        {
            {100, 13},
            {160, 14},
            {200, 15},
            {225, 111},
            {250, 112},
            {275, 113},
            {325, 114},
            {350, 115},
            {375, 116},
            {425, 117},
            {450, 118},
            {475, 119},
            {525, 120},
            {550, 121},
            {575, 122},
            {625, 123},
            {650, 124},
            {675, 125}
        };

        public static Dictionary<short, EmotesEnum> EmotesEarnables = new Dictionary<short, EmotesEnum>()
        {
            {100, (Stump.DofusProtocol.Enums.EmotesEnum)22},
            {300, (Stump.DofusProtocol.Enums.EmotesEnum)171},
            {400, (Stump.DofusProtocol.Enums.EmotesEnum)172},
            {500, (Stump.DofusProtocol.Enums.EmotesEnum)173},
            {600, (Stump.DofusProtocol.Enums.EmotesEnum)174},
            {700, (Stump.DofusProtocol.Enums.EmotesEnum)175}
        };

        private void OnEnergyChanged(short energy, short diff)
        {
            if (diff < 0)
                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 34, Math.Abs(diff)); //Vous avez perdu <b>%1</b> points d'énergie.

            if (energy > 0 && energy <= (Level * 10) && diff < 0)
                SendSystemMessage(11, false, energy);

            PlayerLifeStatus = energy > 0 ? PlayerLifeStatusEnum.STATUS_ALIVE_AND_KICKING : PlayerLifeStatusEnum.STATUS_TOMBSTONE;
        }

        private void OnPlayerLifeStatusChanged(PlayerLifeStatusEnum status)
        {
            if (status != PlayerLifeStatusEnum.STATUS_ALIVE_AND_KICKING)
                ForceDismount();

            var phoenixMapId = 0;

            if (status == PlayerLifeStatusEnum.STATUS_PHANTOM)
            {
                phoenixMapId = World.Instance.GetNearestGraveyard(Map).PhoenixMapId;
                StartRegen();
            }

            CharacterHandler.SendGameRolePlayPlayerLifeStatusMessage(Client, status, phoenixMapId);
            InitializationHandler.SendSetCharacterRestrictionsMessage(Client, this);

            UpdateLook();
        }

        public void FreeSoul()
        {
            if (PlayerLifeStatus != PlayerLifeStatusEnum.STATUS_TOMBSTONE)
                return;

            var graveyard = World.Instance.GetNearestGraveyard(Map);
            Teleport(graveyard.Map, graveyard.Map.GetCell(graveyard.CellId));

            PlayerLifeStatus = PlayerLifeStatusEnum.STATUS_PHANTOM;
        }

        public event LevelChangedHandler LevelChanged;

        private void OnLevelChanged(ushort currentLevel, int difference)
        {
            if (difference > 0)
            {
                if (currentLevel > 200)
                {
                    if (currentLevel - difference < 200)
                    {
                        var statslevels = difference - (currentLevel - 200);
                        StatsPoints += (ushort)(statslevels * 5);
                        Stats.Health.Base += (short)(statslevels * 5);
                    }
                }
                else
                {
                    StatsPoints += (ushort)(difference * 5);
                    Stats.Health.Base += (short)(difference * 5);
                }
            }

            PrivateStats.Health.DamageTaken = 0;


            if (currentLevel >= 100 && currentLevel - difference < 100)
            {
                PrivateStats.AP.Base = Level >= 100 ? 7 : 6;
            }
            else if (currentLevel < 100 && currentLevel - difference >= 100)
            {
                PrivateStats.AP.Base = Level >= 100 ? 7 : 6;
            }

            foreach (var earnable in OrnamentsEarnables)
            {
                if (Client.Character.Level >= earnable.Key)
                {
                    if (!Client.Character.Ornaments.Contains(earnable.Value))
                    {
                        AddOrnament(earnable.Value);
                    }
                }
                else
                {
                    if (Client.Character.Level < earnable.Key)
                    {
                        RemoveOrnament(earnable.Key);
                    }
                }
            }

            foreach (var earnable in EmotesEarnables)
            {
                if (Client.Character.Level >= earnable.Key)
                {
                    if (!Client.Character.Emotes.Contains(earnable.Value))
                    {
                        AddEmote(earnable.Value);
                    }
                }
                else
                {
                    if (Client.Character.Level < earnable.Key)
                    {
                        RemoveEmote((EmotesEnum)earnable.Key);
                    }
                }
            }

            var shortcuts = Shortcuts.SpellsShortcuts;

            foreach (var spell in Breed.Spells)
            {
                if (spell.ObtainLevel > currentLevel)
                {
                    foreach (var shortcut in shortcuts.Where(x => x.Value.SpellId == spell.Spell).ToArray())
                        Shortcuts.RemoveShortcut(ShortcutBarEnum.SPELL_SHORTCUT_BAR, shortcut.Key);

                    if (Spells.HasSpell(spell.Spell, true))
                    {
                        Spells.UnLearnSpell(spell.Spell);
                    }
                }
                else if (spell.ObtainLevel <= currentLevel && !Spells.HasSpell(spell.Spell, true))
                {
                    Spells.LearnSpell(spell.Spell);

                    Shortcuts.AddSpellShortcut(Shortcuts.GetNextFreeSlot(ShortcutBarEnum.SPELL_SHORTCUT_BAR),
                        (short)spell.Spell);
                }

                if (spell.VariantLevel > currentLevel)
                {
                    foreach (var shortcut in shortcuts.Where(x => x.Value.SpellId == spell.VariantId).ToArray())
                        Shortcuts.RemoveShortcut(ShortcutBarEnum.SPELL_SHORTCUT_BAR, shortcut.Key);

                    if (Spells.HasSpell(spell.VariantId, true))
                    {
                        Spells.UnLearnSpell(spell.VariantId);
                    }
                }

                if (spell.VariantLevel <= currentLevel && !Spells.HasSpell(spell.VariantId, true))
                {
                    Spells.LearnSpell(spell.VariantId, 0);
                }

                //BOOST SPELLS WHEN LEVELUP
                if (spell.Spell != 0)
                {
                    var count = SpellManager.Instance.GetSpellLevels(spell.Spell).Count();
                    foreach (var spelllevel in SpellManager.Instance.GetSpellLevels(spell.Spell).OrderByDescending(x => x.MinPlayerLevel))
                    {
                        if (spelllevel.MinPlayerLevel <= currentLevel)
                        {
                            Spells.BoostSpell(spell.Spell, (ushort)count);
                            break;
                        }
                        count--;
                    }
                }
            }

            InventoryHandler.SendSpellListMessage(Client, true);

            RefreshStats();

            if (currentLevel > 1)
            {
                if (difference > 0)
                    CharacterHandler.SendCharacterLevelUpMessage(Client, (ushort)currentLevel);
                CharacterHandler.SendCharacterLevelUpInformationMessage(Map.Clients, this, (ushort)currentLevel);
            }

            LevelChanged?.Invoke(this, currentLevel, difference);
            InventoryHandler.SendSpellListMessage(Client, true);
            SaveLater();
        }

        public void ResetStats(bool additional = false)
        {
            Stats.Agility.Base = 0;
            Stats.Strength.Base = 0;
            Stats.Vitality.Base = 0;
            Stats.Wisdom.Base = 0;
            Stats.Intelligence.Base = 0;
            Stats.Chance.Base = 0;

            if (additional)
            {
                Stats.Agility.Additional = 0;
                Stats.Strength.Additional = 0;
                Stats.Vitality.Additional = 0;
                Stats.Wisdom.Additional = 0;
                Stats.Intelligence.Additional = 0;
                Stats.Chance.Additional = 0;
            }

            var newPoints = (Level - 1) * 5;

            if (Level > 200)
            {
                newPoints = 199 * 5;
            }


            StatsPoints = (ushort)newPoints;

            RefreshStats();
            Inventory.CheckItemsCriterias();

            //Caractéristiques (de base et additionnelles) réinitialisées.(469)
            //Caractéristiques de base réinitialisées.(470)
            SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, (short)(additional ? 469 : 470));
        }

        public void RefreshStats()
        {
            if (IsRegenActive())
                UpdateRegenedLife();

            CharacterHandler.SendCharacterStatsListMessage(Client);

            OnStatsResfreshed();
        }

        public void ToggleGodMode(bool state)
        {
            GodMode = state;
        }

        public void ToggleCriticalMode(bool state)
        {
            CriticalMode = state;
        }

        public bool IsGameMaster()
        {
            return UserGroup.IsGameMaster;
        }

        public void SetBreed(PlayableBreedEnum breed)
        {
            BreedId = breed;
            ResetDefaultLook();
        }

        #endregion Stats

        #region Mount

        private List<Mount> m_stabledMounts = new List<Mount>();
        private List<Mount> m_publicPaddockedMounts = new List<Mount>();
        private Queue<Mount> m_releaseMounts = new Queue<Mount>();

        public Mount EquippedMount
        {
            get { return m_equippedMount; }
            private set
            {
                m_equippedMount = value;
                Record.EquippedMount = value?.Id;

                if (value == null)
                    IsRiding = false;
            }
        }

        public bool IsRiding
        {
            get { return EquippedMount != null && Record.IsRiding; }
            private set { Record.IsRiding = value; }
        }

        public ReadOnlyCollection<Mount> PublicPaddockedMounts => m_publicPaddockedMounts.AsReadOnly();
        public ReadOnlyCollection<Mount> StabledMounts => m_stabledMounts.AsReadOnly();

        public Mount GetStabledMount(int mountId)
        {
            return m_stabledMounts.FirstOrDefault(x => x.Id == mountId);
        }

        public Mount GetPublicPaddockedMount(int mountId)
        {
            return m_publicPaddockedMounts.FirstOrDefault(x => x.Id == mountId);
        }

        private void LoadMounts()
        {
            var database = MountManager.Instance.Database;

            m_stabledMounts = database.Query<MountRecord>(string.Format(MountRecordRelator.FindByOwnerStabled, Id)).Select(x => new Mount(this, x)).ToList();
            m_publicPaddockedMounts = database.Query<MountRecord>(string.Format(MountRecordRelator.FindByOwnerPublicPaddocked, Id)).Select(x => new Mount(this, x)).ToList();

            if (Record.EquippedMount.HasValue)
            {
                EquippedMount = new Mount(this, database.Single<MountRecord>(string.Format(MountRecordRelator.FindById, Record.EquippedMount.Value)));
                EquippedMount.Inventory = new MountInventory(this);
                EquippedMount.Inventory.LoadRecord();

                if (IsRiding)
                    EquippedMount.ApplyMountEffects(false);
            }
        }

        private void SaveMounts()
        {
            var database = MountManager.Instance.Database;
            if (EquippedMount != null && (EquippedMount.IsDirty || EquippedMount.Record.IsNew))
                EquippedMount.Save(database);

            if (EquippedMount != null)
            {
                EquippedMount.Inventory.Save(database);
            }

            foreach (var mount in m_publicPaddockedMounts.Where(x => x.IsDirty || x.Record.IsNew))
                mount.Save(database);

            foreach (var mount in m_stabledMounts.Where(x => x.IsDirty || x.Record.IsNew))
                mount.Save(database);

            while (m_releaseMounts.Count > 0)
            {
                var deletedMount = m_releaseMounts.Dequeue();
                MountManager.Instance.DeleteMount(deletedMount.Record);
            }
        }

        private void SaveQuests()
        {
            var database = QuestManager.Instance.Database;
            foreach (var quest in m_quests)
                quest.Save(database);
        }

        public void AddStabledMount(Mount mount)
        {
            mount.Owner = this;
            m_stabledMounts.Add(mount);
        }

        public void RemoveStabledMount(Mount mount)
        {
            m_stabledMounts.Remove(mount);
        }

        public void AddPublicPaddockedMount(Mount mount)
        {
            m_publicPaddockedMounts.Add(mount);
        }

        public void RemovePublicPaddockedMount(Mount mount)
        {
            m_publicPaddockedMounts.Remove(mount);
        }

        public void SetOwnedMount(Mount mount)
        {
            mount.Owner = this;
        }

        public bool HasEquippedMount()
        {
            return EquippedMount != null;
        }

        public bool EquipMount(Mount mount)
        {
            if (mount.Owner != this)
                return false;

            EquippedMount = mount;
            EquippedMount.Inventory = new MountInventory(this);

            MountHandler.SendMountSetMessage(Client, mount.GetMountClientData());
            MountHandler.SendMountXpRatioMessage(Client, mount.GivenExperience);
            return true;
        }

        public void UnEquipMount()
        {
            if (EquippedMount == null)
                return;

            ForceDismount();

            if (EquippedMount.Harness != null)
            {
                Inventory.MoveItem(EquippedMount.Harness, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, true);

                // Votre harnachement est déposé dans votre inventaire.
                BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 661);
            }

            EquippedMount.Save(MountManager.Instance.Database);
            EquippedMount = null;

            MountHandler.SendMountUnSetMessage(Client);
        }

        public bool IsMountInventoryEmpty()
        {
            if (HasEquippedMount() && EquippedMount.Inventory.Count != 0)
            {
                SendServerMessage("Vous devez d'abord vider l'inventaire de votre monture, avant de la déséquiper.", Color.OrangeRed);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ReleaseMount()
        {
            if (EquippedMount == null || !IsMountInventoryEmpty())
                return false;

            var mount = EquippedMount;
            UnEquipMount();

            MountHandler.SendMountReleaseMessage(Client, mount.Id);
            m_releaseMounts.Enqueue(mount);
            return true;
        }

        public bool RideMount()
        {
            return !IsRiding && ToggleRiding();
        }

        public bool Dismount()
        {
            return IsRiding && ToggleRiding();
        }

        public void ForceDismount()
        {
            IsRiding = false;

            if (EquippedMount == null)
                return;

            EquippedMount.UnApplyMountEffects();
            UpdateLook();

            //Vous descendez de votre monture.
            BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 273);
            MountHandler.SendMountRidingMessage(Client, IsRiding);
        }

        public bool ToggleRiding()
        {
            if (EquippedMount == null)
                return false;

            if (!IsRiding && Level < Mount.RequiredLevel)
            {
                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 227, Mount.RequiredLevel);
                return false;
            }

            if (IsBusy() || (IsInFight() && Fight.State != FightState.Placement))
            {
                //Une action est déjà en cours. Impossible de monter ou de descendre de votre monture.
                BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 355);
                return false;
            }

            IsRiding = !IsRiding;

            if (IsRiding)
            {
                var pet = Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_PETS);
                if (pet != null)
                    Inventory.MoveItem(pet, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);

                EquippedMount.ApplyMountEffects();
            }
            else
            {
                //Vous descendez de votre monture.
                BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 273);

                EquippedMount.UnApplyMountEffects();
            }

            UpdateLook();

            MountHandler.SendMountRidingMessage(Client, IsRiding);

            return true;
        }

        #endregion Mount

        #region Guild

        public GuildMember GuildMember
        {
            get;
            set;
        }

        public Guild Guild
        {
            get { return GuildMember != null ? GuildMember.Guild : null; }
        }

        public bool WarnOnGuildConnection
        {
            get { return Record.WarnOnGuildConnection; }
            set
            {
                Record.WarnOnGuildConnection = value;
                GuildHandler.SendGuildMemberWarnOnConnectionStateMessage(Client, value);
            }
        }

        #endregion Guild

        #region Alignment

        public AlignmentSideEnum AlignmentSide
        {
            get { return m_record.AlignmentSide; }
            private set
            {
                m_record.AlignmentSide = value;
            }
        }

        public sbyte AlignmentGrade
        {
            get;
            set;
        }

        public sbyte AlignmentValue
        {
            get { return m_record.AlignmentValue; }
            private set { m_record.AlignmentValue = value; }
        }

        public ushort Honor
        {
            get { return m_record.Honor; }
            set
            {
                m_record.Honor = value;
                if ((value > LowerBoundHonor && value < UpperBoundHonor))
                    return;

                var lastGrade = AlignmentGrade;

                AlignmentGrade = (sbyte)ExperienceManager.Instance.GetAlignementGrade(m_record.Honor);

                LowerBoundHonor = ExperienceManager.Instance.GetAlignementGradeHonor((byte)AlignmentGrade);
                UpperBoundHonor = ExperienceManager.Instance.GetAlignementNextGradeHonor((byte)AlignmentGrade);

                var difference = AlignmentGrade - lastGrade;

                if (difference != 0)
                    OnGradeChanged(AlignmentGrade, difference);
            }
        }

        public ushort LowerBoundHonor
        {
            get;
            private set;
        }

        public ushort UpperBoundHonor
        {
            get;
            private set;
        }

        public ushort Dishonor
        {
            get { return m_record.Dishonor; }
            private set { m_record.Dishonor = value; }
        }

        public int CharacterPower
        {
            get { return Id + Level; }
        }

        public int CharacterRankId
        {
            get { return m_record.RankId; }
            set
            {
                m_record.RankId = value;
            }
        }

        public int CharacterRankExp
        {
            get { return m_record.RankExp; }
            set
            {
                int before = m_record.RankExp;
                m_record.RankExp = value;
                if (m_record.RankExp < 0)
                {
                    m_record.RankExp = 0;
                    m_record.RankId = 1;
                }
                this.checkRank(before, m_record.RankExp);
            }
        }

        public int CharacterExpEpique
        {
            get { return m_record.Xpepique; }
            set
            {
                int before = m_record.Xpepique;
                m_record.Xpepique = value;


            }
        }

        public double CharacterWinPvm
        {
            get { return m_record.WinPvm; }
            set
            {

                // m_record.WinPvm = value;


            }
        }

        public double CharacterLosPvm
        {
            get { return m_record.LosPvm; }
            set
            {

                //  m_record.LosPvm = value;


            }
        }

        public double CharacterWinPvp
        {
            get { return m_record.WinPvp; }
            set
            {

                // m_record.WinPvm = value;


            }
        }

        public double CharacterLosPvp
        {
            get { return m_record.LosPvp; }
            set
            {

                //  m_record.LosPvm = value;


            }
        }


        public DoppleCollection DoppleCollection { get; private set; }
        public int CharacterRankWin
        {
            get { return m_record.RankWin; }
            set
            {
                m_record.RankWin = value;
            }
        }

        public int CharacterRankLose
        {
            get { return m_record.RankLose; }
            set
            {
                m_record.RankLose = value;
            }
        }

        public DateTime CharacterRankReward
        {
            get { return m_record.RankReward; }
            set { m_record.RankReward = value; }
        }


        public void checkRank(int expBefore, int expAfter)
        {
            if (this.CharacterRankId == 1)
                return;
            var ranks = RankManager.Instance.getRanks();
            foreach (var rank in ranks)
            {
                if (expBefore < rank.Value.RankExp && expAfter >= rank.Value.RankExp)
                {
                    this.CharacterRankId = rank.Value.RankId;
                    SendServerMessage("Enhorabuena, has ganado un nuevo rango Deathmatch, '<b>" + GetCharacterRankName() + "</b>', te queda perfectamente!", Color.Chartreuse);
                    break;
                }
                if (expBefore >= rank.Value.RankExp && expAfter < rank.Value.RankExp)
                {
                    this.CharacterRankId = rank.Value.RankId - 1;
                    SendServerMessage("Has perdido un rango en DeathMatch, ahora estás en el ranking '<b>" + GetCharacterRankName() + "</b>'..", Color.Chartreuse);
                    break;
                }
            }
        }

        public bool PvPEnabled
        {
            get { return m_record.PvPEnabled; }
            set
            {
                m_record.PvPEnabled = value;
                OnPvPToggled();
            }
        }

        public string GetCharacterRankName()
        {
            return RankManager.Instance.getRecordById(this.CharacterRankId).RankName;
        }

        public int GetCharacterRankBonus()
        {
            return RankManager.Instance.getRecordById(this.CharacterRankId).RankBonus;
        }

        public void ChangeAlignementSide(AlignmentSideEnum side)
        {
            AlignmentSide = side;
            OnAligmenentSideChanged();

            if (side == AlignmentSideEnum.ALIGNMENT_ANGEL)
            {
                SendServerMessage("Felicidades, ahora eres <b>Bontariano</b> !");
            }
            else if (side == AlignmentSideEnum.ALIGNMENT_EVIL)
            {
                SendServerMessage("Felicidades, ahora eres <b>Branmariano</b> !");
            }
        }

        public void AddHonor(ushort amount)
        {
            Honor += (Honor + amount) >= HonorLimit ? (ushort)(HonorLimit - Honor) : amount;
        }

        public void SubHonor(ushort amount)
        {
            if (Honor - amount < 0)
                Honor = 0;
            else
                Honor -= amount;
        }

        public void AddDishonor(ushort amount)
        {
            Dishonor += amount;
        }

        public void SubDishonor(ushort amount)
        {
            if (Dishonor - amount < 0)
                Dishonor = 0;
            else
                Dishonor -= amount;
        }

        public void TogglePvPMode(bool state)
        {
            if (IsInFight())
                return;

            PvPEnabled = state;
        }

        public event GradeChangedHandler GradeChanged;

        private void OnGradeChanged(sbyte currentLevel, int difference)
        {
            Map.Refresh(this);
            RefreshStats();

            GradeChanged?.Invoke(this, currentLevel, difference);
        }

        public event Action<Character, bool> PvPToggled;

        private void OnPvPToggled()
        {
            foreach (var item in Inventory.GetItems(x => x.Position == CharacterInventoryPositionEnum.ACCESSORY_POSITION_SHIELD && x.AreConditionFilled(this)))
                Inventory.MoveItem(item, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
            /*
            if (!PvPEnabled)
            {
                var amount = (ushort)Math.Round(Honor * 0.05);
                SubHonor(amount);
                SendServerMessage($"Vous avez perdu <b>{amount}</b> points d' honneur.");
            }
            */

            Map.Refresh(this);
            RefreshStats();

            PvPToggled?.Invoke(this, PvPEnabled);
        }

        public event Action<Character, AlignmentSideEnum> AlignmnentSideChanged;

        private void OnAligmenentSideChanged()
        {
            TogglePvPMode(false);
            Map.Refresh(this);

            Honor = 0;
            Dishonor = 0;

            PvPHandler.SendAlignmentRankUpdateMessage(Client, this);

            AlignmnentSideChanged?.Invoke(this, AlignmentSide);
        }

        #endregion Alignment

        #region Fight

        public CharacterFighter Fighter
        {
            get;
            private set;
        }

        public CompanionActor Companion
        {
            get; set;
        }

        public FightSpectator Spectator
        {
            get;
            private set;
        }

        public FightPvT TaxCollectorDefendFight
        {
            get;
            private set;
        }

        public IFight Fight
        {
            get { return Fighter == null ? (Spectator != null ? Spectator.Fight : null) : Fighter.Fight; }
        }

        public FightTeam Team
        {
            get { return Fighter != null ? Fighter.Team : null; }
        }

        public bool IsGhost()
        {
            return PlayerLifeStatus != PlayerLifeStatusEnum.STATUS_ALIVE_AND_KICKING;
        }

        public bool IsSpectator()
        {
            return Spectator != null;
        }

        public bool IsInFight()
        {
            return IsSpectator() || IsFighting();
        }

        public bool IsFighting()
        {
            return Fighter != null;
        }

        public void SetDefender(FightPvT fight)
        {
            TaxCollectorDefendFight = fight;
        }

        public void ResetDefender()
        {
            TaxCollectorDefendFight = null;
        }

        #endregion Fight

        #region Shortcuts

        public ShortcutBar Shortcuts
        {
            get;
            private set;
        }

        #endregion Shortcuts

        #region Regen

        public byte RegenSpeed
        {
            get;
            private set;
        }

        public DateTime? RegenStartTime
        {
            get;
            private set;
        }

        #endregion Regen

        #region Chat

        public ChatHistory ChatHistory
        {
            get;
            private set;
        }

        public DateTime? MuteUntil
        {
            get { return m_record.MuteUntil; }
            private set { m_record.MuteUntil = value; }
        }

        public void Mute(TimeSpan time, Character from)
        {
            MuteUntil = DateTime.Now + time;

            // %1 vous a rendu muet pour %2 minute(s).
            SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 17, from.Name,
                (int)time.TotalMinutes);
        }

        public void Mute(TimeSpan time)
        {
            MuteUntil = DateTime.Now + time;
            // Le principe de précaution vous a rendu muet pour %1 seconde(s).
            SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 123, (int)time.TotalSeconds);
        }

        public void UnMute()
        {
            MuteUntil = null;
            SendServerMessage("Vous avez été démuté.", Color.Red);
        }

        public bool IsMuted()
        {
            return MuteUntil.HasValue && MuteUntil > DateTime.Now;
        }

        public TimeSpan GetMuteRemainingTime()
        {
            if (!MuteUntil.HasValue)
                return TimeSpan.MaxValue;

            return MuteUntil.Value - DateTime.Now;
        }

        #endregion Chat

        #region Smiley

        public event Action<Character, int> MoodChanged;

        private void OnMoodChanged()
        {
            Guild?.UpdateMember(Guild.TryGetMember(Id));
            MoodChanged?.Invoke(this, SmileyMoodId);
        }

        public ReadOnlyCollection<SmileyPacksEnum> SmileyPacks => Record.SmileyPacks.AsReadOnly();

        public int SmileyMoodId
        {
            get { return Record.SmileyMoodId; }
            set { Record.SmileyMoodId = value; }
        }

        public DateTime LastMoodChange
        {
            get;
            private set;
        }

        public bool HasSmileyPack(SmileyPacksEnum pack) => SmileyPacks.Contains(pack);

        public void AddSmileyPack(SmileyPacksEnum pack)
        {
            if (HasSmileyPack(pack))
                return;

            Record.SmileyPacks.Add(pack);
            ChatHandler.SendChatSmileyExtraPackListMessage(Client, SmileyPacks.ToArray());
        }

        public bool RemoveSmileyPack(SmileyPacksEnum pack)
        {
            var result = Record.SmileyPacks.Remove(pack);

            if (result)
                ChatHandler.SendChatSmileyExtraPackListMessage(Client, SmileyPacks.ToArray());

            return result;
        }

        public override void DisplaySmiley(short smileyId)
        {
            ChatHandler.SendChatSmileyMessage(CharacterContainer.Clients, this, smileyId);
        }

        public void SetMood(short smileyId)
        {
            if (DateTime.Now - LastMoodChange < TimeSpan.FromSeconds(5))
                ChatHandler.SendMoodSmileyResultMessage(Client, 2, smileyId);
            else
            {
                SmileyMoodId = smileyId;
                LastMoodChange = DateTime.Now;

                ChatHandler.SendMoodSmileyResultMessage(Client, 0, smileyId);
                OnMoodChanged();
            }
        }

        #endregion Smiley

        #region Prestige & epique heroique safe

        public double WinPvm
        {
            get { return m_record.WinPvm; }
            //  private set { m_record.WinPvm = value; }
        }

        public double LosPvm
        {
            get { return m_record.LosPvm; }
            //  private set { m_record.LosPvm = value; }
        }

        public int Safeitem
        {
            get { return m_record.safeitem; }
            private set { m_record.safeitem = value; }
        }

        public int Safe200
        {
            get { return m_record.safe200; }
            private set { m_record.safe200 = value; }
        }

        public int PrestigeRank
        {
            get { return m_record.PrestigeRank; }
            private set { m_record.PrestigeRank = value; }
        }

        public int PrestigeHonor
        {
            get { return m_record.PrestigeHonor; }
            private set { m_record.PrestigeHonor = value; }
        }

        public int Xpepique
        {
            get { return m_record.PrestigeHonor; }
            private set { m_record.PrestigeHonor = value; }
        }

        public long RealExperience
        {
            get { return m_record.Experience; }
            private set { m_record.Experience = value; }
        }

        public ArenaLeague ArenaLeague => VersusManager.Instance.GetLeague(Record.LeagueId);

        #region Prestige

        public bool IsPrestigeMax() => PrestigeRank == PrestigeManager.PrestigeTitles.Length;

        public PrestigeItem GetPrestigeItem()
        {
            if (!PrestigeManager.Instance.PrestigeEnabled)
                return null;

            return Inventory.TryGetItem(PrestigeManager.BonusItem) as PrestigeItem;
        }

        public PrestigeItem CreatePrestigeItem() => (PrestigeItem)Inventory.AddItem(PrestigeManager.BonusItem);

        public bool IncrementPrestige()
        {
            if (Level < 200 || IsPrestigeMax() && PrestigeManager.Instance.PrestigeEnabled)
                return false;

            PrestigeRank++;
            AddTitle(PrestigeManager.Instance.GetPrestigeTitle(PrestigeRank));

            switch (PrestigeRank)
            {
                case 3:
                    AddOrnament(111);
                    break;
                case 7:
                    AddOrnament(112);
                    break;
                case 10:
                    AddOrnament(113);
                    break;
            }

            var item = GetPrestigeItem();

            if (item == null)
                item = CreatePrestigeItem();
            else
            {
                item.UpdateEffects();
                Inventory.RefreshItem(item);
            }

            OpenPopup($"Tu es passé prestige {PrestigeRank} ! \r\nTu repasses donc niveau 1 ! \r\n Si tu veux actualiser ton level et tes sorts n'hésite pas à deco reco !");
            foreach (var equippedItem in Inventory.ToArray())
                if (equippedItem.Position != CharacterInventoryPositionEnum.INVENTORY_POSITION_BOOST_FOOD)
                {
                    Inventory.MoveItem(equippedItem, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
                }

            var points = (Spells.CountSpentBoostPoint() + SpellsPoints) - (Level - 1);

            Dismount();

            Experience = 0;
            Spells.ForgetAllSpells();
            SpellsPoints = (ushort)(points >= 0 ? points : 0);
            ResetStats();
            RefreshActor();

            return true;
        }

        public bool DecrementPrestige()
        {
            RemoveTitle(PrestigeManager.Instance.GetPrestigeTitle(PrestigeRank));
            PrestigeRank--;

            var item = GetPrestigeItem();

            if (item != null)
            {
                if (PrestigeRank > 0)
                {
                    item.UpdateEffects();
                    Inventory.RefreshItem(item);
                }
                else Inventory.RemoveItem(item);
            }

            OpenPopup(
                string.Format(
                    "Vous venez de passer au rang prestige {0}. Vous repassez niveau 1 et vous avez acquis des bonus permanents visible sur l'objet '{1}' de votre inventaire, ",
                    PrestigeRank + 1, item.Template.Name) +
                "les bonus s'appliquent sans équipper l'objet. Vous devez vous reconnecter pour actualiser votre niveau.");

            return true;
        }

        public void ResetPrestige()
        {
            foreach (var title in PrestigeManager.PrestigeTitles)
            {
                RemoveTitle(title);
            }
            PrestigeRank = 0;

            var item = GetPrestigeItem();

            if (item != null)
            {
                Inventory.RemoveItem(item);
            }
        }

        #endregion Prestige

        #region Arena

        public bool CanEnterArena(bool send = true)
        {
            if (Level < ArenaManager.ArenaMinLevel)
            {
                if (send)
                    // Vous devez être au moins niveau 50 pour faire des combats en Kolizéum.
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 326);
                return false;
            }

            if (ArenaPenality >= DateTime.Now)
            {
                if (send)
                    // Vous êtes interdit de Kolizéum pour un certain temps car vous avez abandonné un match de Kolizéum.
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 323);

                return false;
            }

            if (IsInJail())
            {
                if (send)
                    // Vous ne pouvez pas participer au Kolizéum depuis une prison.
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 339);

                return false;
            }

            if (IsGhost())
            {
                if (send)
                    // Aucun combat de kolizéum ne vous sera proposé tant que vous serez en tombe ou en fantôme.
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 373);

                return false;
            }

            if (Fight is ArenaFight)
            {
                if (send)
                    //Vous êtes déjà en combat de Kolizéum.
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 334);

                return false;
            }

            if (Fight is FightAgression || Fight is FightPvT || Fight is FightDuel)
                return false;

            return true;
        }

        public void CheckArenaDailyProperties_1vs1()
        {
            if (m_record.ArenaDailyDate_1vs1.Day == DateTime.Now.Day || ArenaDailyMaxRank_1vs1 <= 0)
                return;

            var amountToken = (int)Math.Floor(ArenaDailyMaxRank_1vs1 / 10d);
            var amountKamas = (ulong)(ArenaDailyMaxRank_1vs1 * 1000);

            if (amountToken > 6)
                amountToken = 6;

            m_record.ArenaDailyDate_1vs1 = DateTime.Now;
            ArenaDailyMaxRank_1vs1 = 0;
            ArenaDailyMatchsCount_1vs1 = 0;
            ArenaDailyMatchsWon_1vs1 = 0;

            Inventory.AddItem(ArenaManager.Instance.TokenItemTemplate, amountToken);
            Inventory.AddKamas(amountKamas);

            DisplayNotification(NotificationEnum.KOLIZÉUM, amountKamas, amountToken);
        }


        public void CheckArenaDailyProperties_3vs3()
        {
            if (m_record.ArenaDailyDate_3vs3.Day == DateTime.Now.Day || ArenaDailyMaxRank_3vs3 <= 0)
                return;

            var amountToken = (int)Math.Floor(ArenaDailyMaxRank_3vs3 / 10d);
            var amountKamas = (ulong)(ArenaDailyMaxRank_3vs3 * 1000);

            if (amountToken > 6)
                amountToken = 6;

            m_record.ArenaDailyDate_3vs3 = DateTime.Now;
            ArenaDailyMaxRank_3vs3 = 0;
            ArenaDailyMatchsCount_3vs3 = 0;
            ArenaDailyMatchsWon_3vs3 = 0;

            Inventory.AddItem(ArenaManager.Instance.TokenItemTemplate, amountToken);
            Inventory.AddKamas(amountKamas);

            DisplayNotification(NotificationEnum.KOLIZÉUM, amountKamas, amountToken);
        }

        public int ComputeWonArenaCaliston()
        {
            return (this.Level > 1) ? this.Level / 2 : 1;
        }

        public int ComputeWonArenaTokens(int rank)
        {
            return 1;
        }

        public ulong ComputeWonArenaKamas()
        {
            //return (10000);
            return (ulong)(Level > 199 ? (Level * 1000) : (Level * 1000));
        }

        public int ComputeWonArenaExp()
        {
            int[,] earnedEXPMatrix = new int[,] {
                { 250000, 450000, 750000 },
                { 750000, 1000000, 1250000 },
                { 1250000, 1500000, 2000000 },
            };

            if (Level >= 51 && Level <= 100)
                return earnedEXPMatrix[0, 2];
            else if (Level >= 101 && Level <= 150)
                return earnedEXPMatrix[1, 2];
            else if (Level >= 151 && Level <= 200)
                return earnedEXPMatrix[2, 2];
            return 250000;
        }

        public bool _IsVip1 { get { return VipB == true; } }
        public bool VipB { get { return m_record.VipB; } set { m_record.VipB = value; } }

        public bool _IsVip2 { get { return VipS == true; } }
        public bool VipS { get { return m_record.VipS; } set { m_record.VipS = value; } }

        public bool _IsVip3 { get { return VipG == true; } }
        public bool VipG { get { return m_record.VipG; } set { m_record.VipG = value; } }
        public bool VipD { get { return m_record.VipD; } set { m_record.VipD = value; } }

        public void UpdateArenaProperties(int rank, bool win, int mode)
        {
            if (mode == 1)
                CheckArenaDailyProperties_1vs1();

            else
                CheckArenaDailyProperties_3vs3();

            #region 1vs1
            if (mode == 1)
            {
                ArenaRank_1vs1 = rank;

                if (rank > ArenaMaxRank_1vs1)
                    ArenaMaxRank_1vs1 = rank;

                if (rank > ArenaDailyMaxRank_1vs1)
                    ArenaDailyMaxRank_1vs1 = rank;

                ArenaDailyMatchsCount_1vs1++;

                if (win)
                    ArenaDailyMatchsWon_1vs1++;

                m_record.ArenaDailyDate_1vs1 = DateTime.Now;

            }
            #endregion

            #region 3vs3
            else
            {
                ArenaRank_3vs3 = rank;

                if (rank > ArenaMaxRank_3vs3)
                    ArenaMaxRank_3vs3 = rank;

                if (rank > ArenaDailyMaxRank_3vs3)
                    ArenaDailyMaxRank_3vs3 = rank;

                ArenaDailyMatchsCount_3vs3++;

                if (win)
                    ArenaDailyMatchsWon_3vs3++;

                m_record.ArenaDailyDate_3vs3 = DateTime.Now;

            }


            ContextRoleplayHandler.SendGameRolePlayArenaUpdatePlayerInfosMessage(Client, this);
            #endregion

            ContextRoleplayHandler.SendGameRolePlayArenaUpdatePlayerInfosMessage(Client, this);

            if (!win)
                return;

            if (this == null)
                return;

            if (this.Fighter != null)
            {
                Random rand = new Random();

                var m_breeds = (from fighter in this.Fighter.OpposedTeam.GetAllFightersWithLeavers().OfType<CharacterFighter>()
                                where (this.Fight.Losers.Fighters.Contains(fighter) || this.Fight.Leavers.Contains(fighter))
                                select fighter.Character.Breed.Id).ToList();

                Inventory.AddItem(ArenaManager.Instance.TokenItemTemplate, ComputeWonArenaCaliston());
                Inventory.AddKamas(ComputeWonArenaKamas());
                AddExperience(ComputeWonArenaExp());
            }

        }

        public void SetArenaPenality(TimeSpan time)
        {
            ArenaPenality = DateTime.Now + time;

            // Vous êtes interdit de Kolizéum pour un certain temps car vous avez abandonné un match de Kolizéum.
            SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 323);
        }

        public void ToggleArenaPenality()
        {
            SetArenaPenality(TimeSpan.FromMinutes(ArenaManager.ArenaPenalityTime));
        }

        public void SetAgressionPenality(TimeSpan time)
        {
            AgressionPenality = DateTime.Now + time;
            SendServerMessage("Vous avez été banni pendant une période de " + ArenaManager.ArenaPenalityTime + " minutes pour avoir abandonner un match..", Color.DarkOrange);
            this.battleFieldOn = false;
        }

        public void ToggleAgressionPenality()
        {
            SetAgressionPenality(TimeSpan.FromMinutes(ArenaManager.ArenaPenalityTime));
        }

        public void ToggleArenaWaitTime()
        {
            SetArenaPenality(TimeSpan.FromMinutes(ArenaManager.ArenaWaitTime));
        }

        #region Arena(3vs3)
        public int ArenaRank_3vs3
        {
            get { return m_record.ArenaRank_3vs3; }
            set { m_record.ArenaRank_3vs3 = value; }
        }

        public int ArenaMaxRank_3vs3
        {
            get { return m_record.ArenaMaxRank_3vs3; }
            set { m_record.ArenaMaxRank_3vs3 = value; }
        }

        public int ArenaDailyMaxRank_3vs3
        {
            get { return m_record.ArenaDailyMaxRank_3vs3; }
            set { m_record.ArenaDailyMaxRank_3vs3 = value; }
        }

        public int ArenaDailyMatchsWon_3vs3
        {
            get { return m_record.ArenaDailyMatchsWon_3vs3; }
            set { m_record.ArenaDailyMatchsWon_3vs3 = value; }
        }

        public int ArenaDailyMatchsCount_3vs3
        {
            get { return m_record.ArenaDailyMatchsCount_3vs3; }
            set { m_record.ArenaDailyMatchsCount_3vs3 = value; }
        }
        #endregion

        #region Arena(1vs1)
        public int ArenaRank_1vs1
        {
            get { return m_record.ArenaRank_1vs1; }
            set { m_record.ArenaRank_1vs1 = value; }
        }

        public int ArenaMaxRank_1vs1
        {
            get { return m_record.ArenaMaxRank_1vs1; }
            set { m_record.ArenaMaxRank_1vs1 = value; }
        }

        public int ArenaDailyMaxRank_1vs1
        {
            get { return m_record.ArenaDailyMaxRank_1vs1; }
            set { m_record.ArenaDailyMaxRank_1vs1 = value; }
        }

        public int ArenaDailyMatchsWon_1vs1
        {
            get { return m_record.ArenaDailyMatchsWon_1vs1; }
            set { m_record.ArenaDailyMatchsWon_1vs1 = value; }
        }

        public int ArenaDailyMatchsCount_1vs1
        {
            get { return m_record.ArenaDailyMatchsCount_1vs1; }
            set { m_record.ArenaDailyMatchsCount_1vs1 = value; }
        }
        #endregion

        public DateTime ArenaPenality
        {
            get { return m_record.ArenaPenalityDate; }
            set { m_record.ArenaPenalityDate = value; }
        }

        public DateTime AgressionPenality
        {
            get { return m_record.AgressionPenalityDate; }
            set { m_record.AgressionPenalityDate = value; }
        }

        public ArenaPopup ArenaPopup
        {
            get;
            set;
        }

        public int ArenaMode
        {
            get;
            set;
        }

        #endregion Arena

        #endregion Properties

        #region Actions

        #region Chat

        public bool AdminMessagesEnabled
        {
            get;
            set;
        }

        public void SendConnectionMessages()
        {
            SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 89);
            if (Account.LastConnection != null)
            {
                var date = Account.LastConnection.Value;

                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 152,
                    date.Year,
                    date.Month,
                    date.Day,
                    date.Hour,
                    date.Minute.ToString("00"),
                    Account.LastConnectionIp);

                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 153, Client.IP);
            }

            ulong kamasMerchant = 0;
            var merchantSoldItems = new List<ObjectItemGenericQuantityPrice>();

            foreach (var item in MerchantBag.ToArray())
            {
                if (item.StackSold <= 0)
                    continue;

                var price = item.Price * item.StackSold;
                kamasMerchant += (ulong)price;

                merchantSoldItems.Add(new ObjectItemGenericQuantityPrice((ushort)item.Template.Id, (uint)item.StackSold, (ulong)price));

                item.StackSold = 0;

                if (item.Stack == 0)
                    MerchantBag.RemoveItem(item, true);
            }

            Inventory.AddKamas(kamasMerchant);

            var soldItems = BidHouseManager.Instance.GetSoldBidHouseItems(Account.Id);
            var bidhouseSoldItems = new List<ObjectItemGenericQuantityPrice>();
            ulong kamasBidHouse = 0;

            foreach (var item in soldItems)
            {
                kamasBidHouse += (ulong)item.Price;
                BidHouseManager.Instance.RemoveBidHouseItem(item, true);

                bidhouseSoldItems.Add(new ObjectItemGenericQuantityPrice((ushort)item.Template.Id, (uint)item.Stack, (ulong)item.Price));
            }

            Bank.AddKamas(kamasBidHouse);

            if (merchantSoldItems.Any() || bidhouseSoldItems.Any())
                InventoryHandler.SendExchangeOfflineSoldItemsMessage(Client, merchantSoldItems.ToArray(), bidhouseSoldItems.ToArray());
        }

        public void SendServerMessage(string message)
        {
            BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 0, message);
        }

        public void SendServerMessage(string message, Color color)
        {
            SendServerMessage(string.Format("<font color=\"#{0}\">{1}</font>", color.ToArgb().ToString("X"), message));
        }

        public void SendInformationMessage(TextInformationTypeEnum msgType, short msgId, params object[] parameters)
        {
            BasicHandler.SendTextInformationMessage(Client, msgType, msgId, parameters);
        }

        public void SendSystemMessage(short msgId, bool hangUp, params object[] parameters)
        {
            BasicHandler.SendSystemMessageDisplayMessage(Client, hangUp, msgId, parameters);
        }

        public void OpenPopup(string message)
        {
            OpenPopup(message, "Aflorys", 0);
        }

        public void OpenPopup(string message, string sender, sbyte lockDuration)
        {
            ModerationHandler.SendPopupWarningMessage(Client, message, sender, lockDuration);
        }

        #endregion Chat

        #region Move

        public override void OnEnterMap(Map map)
        {
            ContextRoleplayHandler.SendCurrentMapMessage(Client, map.Id);

            // send actor actions
            foreach (var actor in map.Actors)
            {
                if (!actor.IsMoving())
                    continue;

                var moveKeys = actor.MovementPath.GetServerPathKeys();
                var actorMoving = actor;

                if (actor.MovementPath.Walk)
                    ContextHandler.SendGameCautiousMapMovementMessage(Client, moveKeys, actorMoving);
                else
                    ContextHandler.SendGameMapMovementMessage(Client, moveKeys, actorMoving);

                BasicHandler.SendBasicNoOperationMessage(Client);
            }

            if (IsInIncarnation)
            {
                IncarnationManager.Instance.CheckArea(this, map);
            }

            if (map.Zaap != null && !KnownZaaps.Contains(map))
                DiscoverZaap(map);

            if (MustBeJailed() && !IsInJail())
                TeleportToJail();
            else if (!MustBeJailed() && IsInJail() && !IsGameMaster())
                Teleport(Breed.GetStartPosition());

            /*if (IsRiding && !map.Outdoor && ArenaManager.Instance.Arenas.All(x => x.Value.MapId != map.Id))
                Dismount();*/

            ResetCurrentSkill();

            foreach (var job in Jobs.Where(x => x.IsIndexed))
            {
                job.Template.RefreshCrafter(this);
            }

            //TUTO Animation
            if (Map.Id == 153092354 && Quests.FirstOrDefault(x => x.Id == 1629 && !x.Finished) != null && !Quests.FirstOrDefault(x => x.Id == 1629).CurrentStep.Objectives.FirstOrDefault(x => x.ObjectiveRecord.ObjectiveId == 10015).Finished)
            {
                Map.ActivateInteractiveObjectForASpecificPlayer(this, 489593);
            }
            else if (Map.Id == 153092354 && ((Quests.FirstOrDefault(x => x.Id == 1629 && !x.Finished) != null && Quests.FirstOrDefault(x => x.Id == 1629).CurrentStep.Objectives.FirstOrDefault(x => x.ObjectiveRecord.ObjectiveId == 10015).Finished) || Quests.FirstOrDefault(x => x.Id == 1629 && x.Finished) != null))
            {
                Map.DisableActivateStateInteractiveObjectForASpecificPlayer(this, 489593);
            }

            if (Map.Id == 153092354 && Quests.FirstOrDefault(x => x.Id == 1630 && !x.Finished) != null && !Quests.FirstOrDefault(x => x.Id == 1630).CurrentStep.Objectives.FirstOrDefault(x => x.ObjectiveRecord.ObjectiveId == 10016).Finished)
            {
                Map.ActivateInteractiveObjectForASpecificPlayer(this, 489541);
            }
            else if (Map.Id == 153092354 && ((Quests.FirstOrDefault(x => x.Id == 1630 && !x.Finished) != null && Quests.FirstOrDefault(x => x.Id == 1630).CurrentStep.Objectives.FirstOrDefault(x => x.ObjectiveRecord.ObjectiveId == 10016).Finished) || Quests.FirstOrDefault(x => x.Id == 1630 && x.Finished) != null))
            {
                Map.DisableActivateStateInteractiveObjectForASpecificPlayer(this, 489541);
            }

            base.OnEnterMap(map);
        }

        public override bool CanMove()
        {
            if (PlayerLifeStatus == PlayerLifeStatusEnum.STATUS_TOMBSTONE)
                return false;

            if (Fight?.State == FightState.Placement || Fight?.State == FightState.NotStarted)
                return false;

            return base.CanMove() && !IsDialoging();
        }

        public override bool IsGonnaChangeZone() => base.IsGonnaChangeZone() || !IsLoggedIn;

        public override bool StartMove(Path movementPath)
        {
            LeaveDialog(); //Close Dialog && RequestBox when moving

            if (Inventory.IsFull())
                movementPath.SetWalk();

            if (!IsFighting() && !MustBeJailed() && IsInJail())
            {
                Teleport(Breed.GetStartPosition());
                return false;
            }

            if (IsFighting())
                if (Fighter.IsSlaveTurn())
                    return Fighter.GetSlave().StartMove(movementPath);
                else return Fighter.StartMove(movementPath);

            CancelEmote(false);

            return base.StartMove(movementPath);
        }

        public bool IsOppositeAlignement(int alignmentId)
        {
            bool flag = false;
            switch (this.AlignmentSide)
            {
                case AlignmentSideEnum.ALIGNMENT_ANGEL:
                    if (alignmentId == (int)AlignmentSideEnum.ALIGNMENT_EVIL)
                    {
                        flag = true;
                    }
                    break;
                case AlignmentSideEnum.ALIGNMENT_EVIL:
                    if (alignmentId == (int)AlignmentSideEnum.ALIGNMENT_ANGEL)
                    {
                        flag = true;
                    }
                    break;
            }
            return flag;
        }


        public override bool StopMove() => IsFighting() ? Fighter.StopMove() : base.StopMove();

        public override bool MoveInstant(ObjectPosition destination) => IsFighting() ? Fighter.MoveInstant(destination) : base.MoveInstant(destination);

        public override bool StopMove(ObjectPosition currentObjectPosition) => IsFighting() ? Fighter.StopMove(currentObjectPosition) : base.StopMove(currentObjectPosition);

        public override bool Teleport(MapNeighbour mapNeighbour)
        {
            var success = base.Teleport(mapNeighbour);

            if (!success)
            {
                if (this.Account.UserGroupId >= (int)RoleEnum.Moderator)
                    SendServerMessage("Unknown map transition");
            }
            return success;
        }

        public override bool Teleport(Map mapScroll, MapNeighbour mapNeighbour)
        {
            var success = base.Teleport(mapScroll, mapNeighbour);

            if (!success)
            {
                if (this.Account.UserGroupId >= (int)RoleEnum.Moderator)
                    SendServerMessage("Unknown map transition");
            }
            return success;
        }

        #region Jail

        private readonly int[] JAILS_MAPS = { 105121026, 105119744, 105120002 };
        private readonly int[][] JAILS_CELLS = { new[] { 179, 445, 184, 435 }, new[] { 314 }, new[] { 300 } };

        public bool TeleportToJail()
        {
            var random = new AsyncRandom();

            var mapIndex = random.Next(0, JAILS_MAPS.Length);
            var cellIndex = random.Next(0, JAILS_CELLS[mapIndex].Length);

            var map = World.Instance.GetMap(JAILS_MAPS[mapIndex]);

            if (map == null)
            {
                logger.Error("Cannot find jail map {0}", JAILS_MAPS[mapIndex]);
                return false;
            }

            var cell = map.Cells[JAILS_CELLS[mapIndex][cellIndex]];

            Teleport(new ObjectPosition(map, cell), false);

            return true;
        }

        public bool MustBeJailed()
        {
            return Client.Account.IsJailed && (Client.Account.BanEndDate == null || Client.Account.BanEndDate > DateTime.Now);
        }

        public bool IsInJail()
        {
            return JAILS_MAPS.Contains(Map.Id);
        }

        #endregion Jail

        protected override void OnTeleported(ObjectPosition position)
        {
            base.OnTeleported(position);

            UpdateRegenedLife();

            if (Dialog != null)
                Dialog.Close();
        }

        public override bool CanChangeMap() => base.CanChangeMap() && !IsFighting() && !Account.IsJailed;

        #endregion Move



        public void AddAscensionStair(int amount)
        {
            AscensionStair += amount;
        }

        public int AscensionStair
        {
            get { return m_record.AscensionStair; }
            private set { m_record.AscensionStair = value; }
        }

        public void SetAscensionStair(int amount)
        {
            AscensionStair = amount;
        }

        public void SubAscensionStair(int amount)
        {
            if (AscensionStair - amount < 0)
                AscensionStair = 0;
            else
                AscensionStair -= amount;
        }

        public int GetAscensionStair()
        {
            return AscensionStair;
        }

        public bool IncrementAscensionStair()
        {
            AddAscensionStair(1);
            RefreshActor();
            return true;
        }

        public bool DecrementAscensionStair()
        {
            SubAscensionStair(1);
            RefreshActor();
            return true;
        }


        #region Dialog

        public void DisplayNotification(string text, NotificationEnum notification = NotificationEnum.INFORMATION)
        {
            Client.Send(new NotificationByServerMessage((ushort)notification, new[] { text }, true));
        }

        public void DisplayNotification(NotificationEnum notification, params object[] parameters)
        {
            Client.Send(new NotificationByServerMessage((ushort)notification, parameters.Select(entry => entry.ToString()).ToArray(), true));
        }

        public void DisplayNotification(Notification notification)
        {
            notification.Display();
        }

        public void LeaveDialog()
        {
            if (IsInRequest())
                CancelRequest();

            if (IsDialoging())
                Dialog.Close();
        }

        public void ReplyToNpc(int replyId)
        {
            if (!IsTalkingWithNpc())
                return;

            ((NpcDialog)Dialog).Reply(replyId);
        }

        public void AcceptRequest()
        {
            if (!IsInRequest())
                return;

            if (RequestBox.Target == this)
                RequestBox.Accept();
        }

        public void DenyRequest()
        {
            if (!IsInRequest())
                return;

            if (RequestBox.Target == this)
                RequestBox.Deny();
        }

        public void CancelRequest()
        {
            if (!IsInRequest())
                return;

            if (IsRequestSource())
                RequestBox.Cancel();
            else if (IsRequestTarget())
                DenyRequest();
        }

        #endregion Dialog

        #region Party

        public void Invite(Character target, PartyTypeEnum type, bool force = false)
        {
            var created = false;
            Party party;
            if (!IsInParty(type))
            {
                party = PartyManager.Instance.Create(type);

                if (!EnterParty(party))
                    return;

                created = true;
            }
            else party = GetParty(type);
            if (!party.CanInvite(target, out var error, this))
            {
                PartyHandler.SendPartyCannotJoinErrorMessage(target.Client, party, error);
                if (created)
                    LeaveParty(party);

                return;
            }

            if (target.m_partyInvitations.ContainsKey(party.Id))
            {
                if (created)
                    LeaveParty(party);

                return; // already invited
            }

            var invitation = new PartyInvitation(party, this, target);
            target.m_partyInvitations.Add(party.Id, invitation);

            party.AddGuest(target);

            if (force)
                invitation.Accept();
            else
                invitation.Display();
        }

        public PartyInvitation GetInvitation(int id)
        {
            return m_partyInvitations.ContainsKey(id) ? m_partyInvitations[id] : null;
        }

        public bool RemoveInvitation(PartyInvitation invitation)
        {
            return m_partyInvitations.Remove(invitation.Party.Id);
        }

        public void DenyAllInvitations()
        {
            foreach (var partyInvitation in m_partyInvitations.ToArray())
            {
                partyInvitation.Value.Deny();
            }
        }

        public void DenyAllInvitations(PartyTypeEnum type)
        {
            foreach (var partyInvitation in m_partyInvitations.Where(x => x.Value.Party.Type == type).ToArray())
            {
                partyInvitation.Value.Deny();
            }
        }

        public void DenyAllInvitations(Party party)
        {
            foreach (var partyInvitation in m_partyInvitations.Where(x => x.Value.Party == party).ToArray())
            {
                partyInvitation.Value.Deny();
            }
        }

        public bool EnterParty(Party party)
        {
            if (IsInParty(party.Type))
                LeaveParty(GetParty(party.Type));

            if (m_partyInvitations.ContainsKey(party.Id))
                m_partyInvitations.Remove(party.Id);

            DenyAllInvitations(party.Type);
            UpdateRegenedLife();

            if (party.Disbanded)
                return false;

            SetParty(party);
            party.MemberRemoved += OnPartyMemberRemoved;
            party.PartyDeleted += OnPartyDeleted;

            if (party.IsMember(this))
                return false;

            if (party.PromoteGuestToMember(this))
                return true;

            // if fails to enter
            party.MemberRemoved -= OnPartyMemberRemoved;
            party.PartyDeleted -= OnPartyDeleted;
            ResetParty(party.Type);

            return false;
        }

        public void LeaveParty(Party party)
        {
            if (!IsInParty(party.Id) || !party.CanLeaveParty(this))
                return;

            party.MemberRemoved -= OnPartyMemberRemoved;
            party.PartyDeleted -= OnPartyDeleted;
            party.RemoveMember(this);
            ResetParty(party.Type);
        }

        private void OnPartyMemberRemoved(Party party, Character member, bool kicked)
        {
            if (m_followedCharacter == member)
                UnfollowMember();

            if (member != this)
                return;

            party.MemberRemoved -= OnPartyMemberRemoved;
            party.PartyDeleted -= OnPartyDeleted;

            ResetParty(party.Type);
        }

        private void OnPartyDeleted(Party party)
        {
            party.MemberRemoved -= OnPartyMemberRemoved;
            party.PartyDeleted -= OnPartyDeleted;

            ResetParty(party.Type);
        }

        public void FollowMember(Character character)
        {
            if (m_followedCharacter != null)
                UnfollowMember();

            m_followedCharacter = character;
            character.EnterMap += OnFollowedMemberEnterMap;

            PartyHandler.SendPartyFollowStatusUpdateMessage(Client, Party, true, character.Id);
            CompassHandler.SendCompassUpdatePartyMemberMessage(Client, character, true);
        }

        public void UnfollowMember()
        {
            if (m_followedCharacter == null)
                return;

            m_followedCharacter.EnterMap -= OnFollowedMemberEnterMap;

            PartyHandler.SendPartyFollowStatusUpdateMessage(Client, Party, true, 0);
            CompassHandler.SendCompassUpdatePartyMemberMessage(Client, m_followedCharacter, false);

            m_followedCharacter = null;
        }

        private void OnFollowedMemberEnterMap(RolePlayActor actor, Map map)
        {
            if (!(actor is Character character))
                return;

            CompassHandler.SendCompassUpdatePartyMemberMessage(Client, character, true);
        }

        #endregion Party

        #region Quest

        private List<Quest> m_quests = new List<Quest>();

        public ReadOnlyCollection<Quest> Quests => m_quests.AsReadOnly();

        public void LoadQuests()
        {
            var database = QuestManager.Instance.Database;
            var _quests = database.Query<QuestRecord>(string.Format(QuestRecordRelator.FetchByOwner, Id)).ToList();
            var objective = database.Query<QuestObjectiveStatus>(string.Format(QuestRecordRelator.FetchObjectiveByOwner, Id)).ToList();
            foreach (var test in _quests)
                test.Objectives = objective.Where(x => x.QuestId == test.QuestId && x.OwnerId == test.OwnerId).Select(y => y).ToList();
            m_quests = _quests.Select(x => new Quest(this, x)).ToList();
        }

        public void StartQuest(int questStepId)
        {
            var step = QuestManager.Instance.GetQuestStep(questStepId);

            if (step == null)
                throw new Exception($"Step {questStepId} not found");

            StartQuest(step);
        }

        public void StartQuest(QuestStepTemplate questStep)
        {
            var quest = m_quests.FirstOrDefault(x => x.Template.Steps.Contains(questStep));

            if (quest == null)
            {
                quest = new Quest(this, questStep);
                m_quests.Add(quest);
                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 54, quest.Id);
            }
            else
            {
                quest.ChangeQuestStep(questStep);
            }
            foreach (var actor in Map.Actors.Where(x => x is Npc))
                (actor as Npc).Refresh();


            RefreshActor();

            foreach (var interac in Map.GetInteractiveObjects())
                Map.Refresh(interac);

            //TUTO Animation
            if (quest.Id == 1629 && Map.Id == 153092354)
            {
                Map.ActivateInteractiveObjectForASpecificPlayer(this, 489593);
            }

            if (quest.Id == 1630 && Map.Id == 153092354)
            {
                Map.ActivateInteractiveObjectForASpecificPlayer(this, 489541);
            }
        }

        public event Action<Character, Quest> OnQuestFinished;

        public void QuestCompleted(Quest quest)
        {
            OnQuestFinished?.Invoke(this, quest);
        }

        #endregion

        #region Fight

        public delegate void CharacterContextChangedHandler(Character character, bool inFight);

        public event CharacterContextChangedHandler ContextChanged;

        public delegate void CharacterEnterFightChangedHandler(CharacterFighter fighter);

        public event CharacterEnterFightChangedHandler EnterFight;

        public delegate void CharacterFightReadyStatusChanged(CharacterFighter fighter);

        public event CharacterFightReadyStatusChanged ReadyStatusChanged;

        public delegate void CharacterFightEndedHandler(Character character, CharacterFighter fighter);

        public event CharacterFightEndedHandler FightEnded;

        public delegate void CharacterFightStartedHandler(Character character, CharacterFighter fighter);

        public event CharacterFightStartedHandler FightStarted;

        public delegate void CharacterDiedHandler(Character character);

        public event CharacterDiedHandler Died;

        private void OnDied()
        {
            var energylost = (short)(10 * Level);

            if (SuperArea.Id == 5) //Dimensions divines
                energylost *= 2;

            Energy -= 0;

            if (!IsGhost())
            {
                var dest = GetSpawnPoint() ?? Breed.GetStartPosition();

                NextMap = dest.Map;
                Cell = dest.Cell ?? dest.Map.GetRandomFreeCell();
                Direction = dest.Direction;
            }

            Stats.Health.DamageTaken = (Stats.Health.TotalMax - 1);

            Died?.Invoke(this);
        }

        private void OnFightEnded(CharacterFighter fighter)
        {
            FightEnded?.Invoke(this, fighter);
        }

        public void OnFightStarted(CharacterFighter fighter)
        {
            FightStarted?.Invoke(this, fighter);
        }

        private void OnCharacterContextChanged(bool inFight)
        {
            ContextChanged?.Invoke(this, inFight);
        }

        public void OnCharacterContextReady(int mapId)
        {

        }

        public void OnEnterFight(CharacterFighter fighter)
        {
            EnterFight?.Invoke(fighter);
        }

        public void OnReadyStatusChanged(CharacterFighter fighter)
        {
            ReadyStatusChanged?.Invoke(fighter);
        }

        public FighterRefusedReasonEnum CanRequestFight(Character target)
        {
            if (!target.IsInWorld || target.IsFighting() || target.IsSpectator() || target.IsBusy() || !target.IsAvailable(this, false))
                return FighterRefusedReasonEnum.OPPONENT_OCCUPIED;

            if (!IsInWorld || IsFighting() || IsSpectator() || IsBusy())
                return FighterRefusedReasonEnum.IM_OCCUPIED;

            if (target == this)
                return FighterRefusedReasonEnum.FIGHT_MYSELF;

            if (target.Map != Map || !Map.AllowChallenge)
                return FighterRefusedReasonEnum.WRONG_MAP;


            if (IsGhost())
                return FighterRefusedReasonEnum.GHOST_REFUSED;

            return FighterRefusedReasonEnum.FIGHTER_ACCEPTED;
        }

        public bool CanBattlefield(Character target)
        {
            if (target == this)
                return false;

            if (!target.battleFieldOn)
                return false;

            if (!target.IsInWorld || target.IsFighting() || target.IsSpectator() || target.IsBusy() || !target.IsAvailable(this, false))
                return false;

            if (string.Equals(target.Client.IP, Client.IP) && !IsGameMaster())
                return false;

            if (Math.Abs(Level - target.Level) > 40)
                return false;

            if (IsGhost() || target.IsGhost())
                return false;
            return true;
        }

        public FighterRefusedReasonEnum CanAgress(Character target, bool bypassCheck = false)
        {
            if (target == this)
                return FighterRefusedReasonEnum.FIGHT_MYSELF;

            if (!target.PvPEnabled || !PvPEnabled)
                return FighterRefusedReasonEnum.INSUFFICIENT_RIGHTS;

            if (!target.IsInWorld || target.IsFighting() || target.IsSpectator() || target.IsBusy())
                return FighterRefusedReasonEnum.OPPONENT_OCCUPIED;

            if (!bypassCheck && (!IsInWorld || IsFighting() || IsSpectator() || IsBusy()))
                return FighterRefusedReasonEnum.IM_OCCUPIED;

            if (AlignmentSide <= AlignmentSideEnum.ALIGNMENT_NEUTRAL || target.AlignmentSide <= AlignmentSideEnum.ALIGNMENT_NEUTRAL)
                return FighterRefusedReasonEnum.WRONG_ALIGNMENT;

            if (target.AlignmentSide == AlignmentSide)
                return FighterRefusedReasonEnum.WRONG_ALIGNMENT;

            if (!bypassCheck && (target.Map != Map || !Map.AllowAggression))
                return FighterRefusedReasonEnum.WRONG_MAP;

            if (string.Equals(target.Client.IP, Client.IP) && !IsGameMaster())
                return FighterRefusedReasonEnum.MULTIACCOUNT_NOT_ALLOWED;

            if (Math.Abs((Level > 200 ? 200 : Level) - (target.Level > 200 ? 200 : target.Level)) > 20)
                return FighterRefusedReasonEnum.INSUFFICIENT_RIGHTS;

            if (SubArea.Id == 850)
                return FighterRefusedReasonEnum.WRONG_MAP;

            if (IsGhost() || target.IsGhost())
                return FighterRefusedReasonEnum.GHOST_REFUSED;

            if (AgressionPenality >= DateTime.Now || target.AgressionPenality >= DateTime.Now)
                return FighterRefusedReasonEnum.INSUFFICIENT_RIGHTS;

            return FighterRefusedReasonEnum.FIGHTER_ACCEPTED;
        }

        public FighterRefusedReasonEnum CanAttack(TaxCollectorNpc target)
        {
            if (GuildMember != null && target.IsTaxCollectorOwner(GuildMember))
                return FighterRefusedReasonEnum.WRONG_GUILD;


            if (IsBusy() || IsFighting() || IsSpectator() || !IsInWorld)
                return FighterRefusedReasonEnum.IM_OCCUPIED;

            if (target.IsBusy() || target.IsFighting || !target.IsInWorld)
                return FighterRefusedReasonEnum.OPPONENT_OCCUPIED;

            if (target.Map != Map)
                return FighterRefusedReasonEnum.WRONG_MAP;

            if (IsGhost())
                return FighterRefusedReasonEnum.GHOST_REFUSED;

            return FighterRefusedReasonEnum.FIGHTER_ACCEPTED;
        }

        public FighterRefusedReasonEnum CanAttack(MonsterGroup group)
        {
            if (IsFighting() || IsSpectator() || !IsInWorld)
                return FighterRefusedReasonEnum.IM_OCCUPIED;

            if (!group.IsInWorld)
                return FighterRefusedReasonEnum.OPPONENT_OCCUPIED;

            if (group.Map != Map)
                return FighterRefusedReasonEnum.WRONG_MAP;

            if (IsGhost())
                return FighterRefusedReasonEnum.GHOST_REFUSED;

            return FighterRefusedReasonEnum.FIGHTER_ACCEPTED;
        }

        public CharacterFighter CreateFighter(FightTeam team)
        {
            if (IsFighting() || IsSpectator() || !IsInWorld)
                throw new Exception(string.Format("{0} is already in a fight", this));

            NextMap = Map; // we do not leave the map
            Map.Leave(this);
            StopRegen();

            if (IsInMovement)
                StopMove();

            ContextHandler.SendGameContextDestroyMessage(Client);
            ContextHandler.SendGameContextCreateMessage(Client, 2);

            Fighter = new CharacterFighter(this, team);

            ContextHandler.SendGameFightStartingMessage(Client, team.Fight.FightType, Fight
            .DefendersTeam.Leader == null ? 0 : Fight.DefendersTeam.Leader.Id, team.Leader ==
            null ? 0 : team.Leader.Id, team.Fight);

            if (IsPartyLeader() && Party.RestrictFightToParty && team.Fighters.Count == 0 && !team.IsRestrictedToParty)
                team.ToggleOption(FightOptionsEnum.FIGHT_OPTION_SET_TO_PARTY_ONLY);

            OnCharacterContextChanged(true);

            return Fighter;
        }

        public CompanionActor CreateCompanion(FightTeam team, CharacterFighter characterFighter)
        {
            OnCharacterContextChanged(true);

            if (Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_ENTITY).FirstOrDefault() == null)
                return null;

            CompanionRecord companion =
           Singleton<CompanionsManager>.Instance.GetCompanionById(
               Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_ENTITY).FirstOrDefault().Template.Id).FirstOrDefault();
            if (companion != null)
            {

                Companion = new CompanionActor(this, Fighter?.Team, ActorLook.Parse(companion.Look), companion.SpellsId.Select(spell => Singleton<SpellManager>.Instance.GetRealCompanionSpell(spell))
                        .Select(finalId => new Spell(Singleton<SpellManager>.Instance.GetSpellTemplate(finalId), 1))
                        .ToList(),
                    (byte)companion.Id, Fight.GetNextContextualId())
                { NextMap = Map };
                return Companion;
            }

            return null;

        }

        public FightSpectator CreateSpectator(IFight fight)
        {
            if (IsFighting() || IsSpectator() || !IsInWorld)
                throw new Exception(string.Format("{0} is already in a fight", this));

            if (!fight.CanSpectatorJoin(this))
                throw new Exception(string.Format("{0} cannot join fight in spectator", this));

            NextMap = Map; // we do not leave the map
            Map.Leave(this);
            StopRegen();

            if (IsInMovement)
                StopMove();

            ContextHandler.SendGameContextDestroyMessage(Client);
            ContextHandler.SendGameContextCreateMessage(Client, 2);

            ContextHandler.SendGameFightStartingMessage(Client, fight.FightType, fight
            .ChallengersTeam.Leader.Id, fight.DefendersTeam.Leader.Id, fight);

            Spectator = new FightSpectator(this, fight);

            OnCharacterContextChanged(true);

            return Spectator;
        }

        private CharacterFighter RejoinFightAfterDisconnection(CharacterFighter oldFighter)
        {
            Map.Leave(this);
            Map = oldFighter.Map;
            NextMap = oldFighter.Character.NextMap;

            StopRegen();

            ContextHandler.SendGameContextDestroyMessage(Client);
            ContextHandler.SendGameContextCreateMessage(Client, 2);
            ContextRoleplayHandler.SendCurrentMapMessage(Client, Map.Id);
            ContextRoleplayHandler.SendMapComplementaryInformationsDataMessage(Client);

            oldFighter.RestoreFighterFromDisconnection(this);
            Fighter = oldFighter;

            ContextHandler.SendGameFightStartingMessage(Client, Fighter.Fight.FightType, Fighter.Fight.ChallengersTeam.Leader.Id,
                Fighter.Fight.DefendersTeam.Leader.Id, Fighter.Fight);

            Fighter.Fight.RejoinFightFromDisconnection(Fighter);
            OnCharacterContextChanged(true);

            foreach (var challenge in Fight.Challenges)
            {
                ContextHandler.SendChallengeInfoMessage(Client, challenge);

                if (challenge.Status != ChallengeStatusEnum.RUNNING)
                    ContextHandler.SendChallengeResultMessage(Client, challenge);
            }

            return Fighter;
        }

        /// <summary>
        /// Rejoin the map after a fight
        /// </summary>
        public void RejoinMap()
        {
            if (!IsFighting() && !IsSpectator())
                return;

            if (Fighter != null)
                OnFightEnded(Fighter);

            if (GodMode)
                Stats.Health.DamageTaken = 0;

            else if (Fighter != null && (Fighter.HasLeft() && !Fighter.IsDisconnected || Fight.Losers == Fighter.Team) && !Fight.IsDeathTemporarily)
                OnDied();

            if (Fighter != null && Fighter.HasLeft() && Fight.FightType == FightTypeEnum.FIGHT_TYPE_AGRESSION)
                ToggleAgressionPenality();

            Fighter = null;
            Spectator = null;

            ContextHandler.SendGameContextDestroyMessage(Client);
            ContextHandler.SendGameContextCreateMessage(Client, 1);

            RefreshStats();

            OnCharacterContextChanged(false);
            StartRegen();

            if (Map == null)
                return;

            if (IsLoggedIn)
            {
                if (!NextMap.Area.IsRunning)
                    NextMap.Area.Start();

                NextMap.Area.ExecuteInContext(() =>
                {
                    if (IsLoggedIn)
                    {
                        LastMap = Map;
                        Map = NextMap;
                        Map.Enter(this);
                        NextMap = null;
                    }
                });
            }
            else
                SaveLater(); // if disconnected in fight we must save the change at the end of the fight
        }

        #endregion Fight

        #region Regen

        public bool IsRegenActive()
        {
            return RegenStartTime.HasValue;
        }

        public void StartRegen()
        {
            StartRegen((byte)(10f / Rates.RegenRate));
        }

        public void StartRegen(byte timePerHp)
        {
            if (IsRegenActive())
                StopRegen();

            if (PlayerLifeStatus == PlayerLifeStatusEnum.STATUS_TOMBSTONE)
                return;
            if (Stats.Health.TotalSafe < MaxLifePoints)
                RegenStartTime = DateTime.Now;
            RegenSpeed = timePerHp;

            CharacterHandler.SendLifePointsRegenBeginMessage(Client, (sbyte)RegenSpeed);
        }

        public void StopRegen()
        {
            if (!IsRegenActive())
                return;

            var regainedLife = (int)Math.Floor((DateTime.Now - RegenStartTime).Value.TotalSeconds / (RegenSpeed / 10f));

            if (Stats.Health.TotalSafe + regainedLife > MaxLifePoints)
                regainedLife = MaxLifePoints - Stats.Health.TotalSafe;

            if (regainedLife > 0)
            {
                Stats.Health.DamageTaken -= regainedLife;
            }

            CharacterHandler.SendLifePointsRegenEndMessage(Client, regainedLife);

            RegenStartTime = null;
            RegenSpeed = 0;
            OnLifeRegened(regainedLife);
        }

        public void UpdateRegenedLife()
        {
            if (!IsRegenActive())
                return;

            if (IsInFight())
                return;

            var regainedLife = (int)Math.Floor((DateTime.Now - RegenStartTime).Value.TotalSeconds / (RegenSpeed / 10f));

            if (Stats.Health.TotalSafe + regainedLife > MaxLifePoints)
                regainedLife = MaxLifePoints - Stats.Health.TotalSafe;

            if (regainedLife > 0 && Stats.Health.DamageTaken != 0)
            {
                Stats.Health.DamageTaken -= regainedLife;
                CharacterHandler.SendUpdateLifePointsMessage(Client);
            }
            if (Stats.Health.TotalSafe < MaxLifePoints)
                RegenStartTime = DateTime.Now;

            OnLifeRegened(regainedLife);
        }

        #endregion Regen

        #region Zaaps

        private ObjectPosition m_spawnPoint;

        public List<Map> KnownZaaps
        {
            get { return Record.KnownZaaps; }
        }

        public void DiscoverZaap(Map map, bool message = true)
        {
            if (!KnownZaaps.Contains(map))
                KnownZaaps.Add(map);

            if (message)
                BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 24);
            // new zaap
        }

        public void SetSpawnPoint(Map map)
        {
            Record.SpawnMap = map;
            m_spawnPoint = null;

            BasicHandler.SendTextInformationMessage(Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 6);
            // pos saved

            InteractiveHandler.SendZaapRespawnUpdatedMessage(Client);
        }

        public ObjectPosition GetSpawnPoint()
        {
            if (Record.SpawnMap == null)
                return Breed.GetStartPosition();

            if (m_spawnPoint != null)
                return m_spawnPoint;

            var map = Record.SpawnMap;

            if (map.Zaap == null)
                return new ObjectPosition(map, map.GetRandomFreeCell(), Direction);

            var cell = map.GetRandomAdjacentFreeCell(map.Zaap.Position.Point);
            var direction = map.Zaap.Position.Point.OrientationTo(new MapPoint(cell));

            return new ObjectPosition(map, cell, direction);
        }

        #endregion Zaaps

        #region Emotes


        private LimitedStack<Pair<Emote, DateTime>> m_playedEmotes = new LimitedStack<Pair<Emote, DateTime>>(5);
        private bool m_cancelEmote;

        public ReadOnlyCollection<EmotesEnum> Emotes => Record.Emotes.AsReadOnly();

        public override Pair<Emote, DateTime> LastEmoteUsed => !m_cancelEmote && m_playedEmotes.Count > 0 ? m_playedEmotes.Peek() : null;

        private Pair<Emote, DateTime> GetCurrentEmotePair() => LastEmoteUsed != null && (LastEmoteUsed.First.Duration == 0 || LastEmoteUsed.First.Persistancy || (DateTime.Now - LastEmoteUsed.Second) < TimeSpan.FromMilliseconds(LastEmoteUsed.First.Duration))
            ? LastEmoteUsed
            : null;

        public Emote GetCurrentEmote() => GetCurrentEmotePair()?.First;

        public bool CancelEmote(bool send = true)
        {
            var emote = GetCurrentEmote();

            if (emote == null)
                return false;

            m_cancelEmote = true;
            UpdateLook(emote, false, false);

            if (send)
                ContextRoleplayHandler.SendEmotePlayMessage(CharacterContainer.Clients, this, 0);

            RefreshActor();

            return true;
        }

        public bool HasEmote(EmotesEnum emote) => Emotes.Contains(emote);

        public void AddEmote(EmotesEnum emote)
        {
            if (HasEmote(emote))
                return;

            Record.Emotes.Add(emote);
            ContextRoleplayHandler.SendEmoteAddMessage(Client, (sbyte)emote);
        }

        public bool RemoveEmote(EmotesEnum emote)
        {
            var result = Record.Emotes.Remove(emote);

            if (result)
                ContextRoleplayHandler.SendEmoteRemoveMessage(Client, (sbyte)emote);

            return result;
        }

        public void PlayEmote(EmotesEnum emoteId, bool force = false)
        {
            var emote = ChatManager.Instance.GetEmote((int)emoteId);

            if (emote == null)
            {
                ContextRoleplayHandler.SendEmotePlayErrorMessage(Client, (sbyte)emoteId);
                return;
            }

            if (!HasEmote(emoteId) && !force)
            {
                ContextRoleplayHandler.SendEmotePlayErrorMessage(Client, (sbyte)emoteId);
                return;
            }

            var currentEmote = GetCurrentEmote();

            if (currentEmote != null)
            {
                CancelEmote();

                if (currentEmote == emote)
                {
                    return;
                }
            }

            m_cancelEmote = false;
            m_playedEmotes.Push(new Pair<Emote, DateTime>(emote, DateTime.Now));
            UpdateLook(emote, true, false);

            RefreshActor();

            ContextRoleplayHandler.SendEmotePlayMessage(CharacterContainer.Clients, this, emoteId);
        }

        #endregion Emotes

        #region FinishMove

        public ReadOnlyCollection<FinishMove> FinishMoves => Record.FinishMoves.AsReadOnly();

        public bool HasFinishMove(int finishMove) => FinishMoves.Any(x => x.Id == finishMove);

        public void AddFinishMove(int finishMove)
        {
            if (HasFinishMove(finishMove))
                return;

            Record.FinishMoves.Add(new FinishMove(finishMove, false));
        }

        public bool RemoveFinishMove(int finishMove)
        {
            if (HasFinishMove(finishMove))
                return false;

            return Record.FinishMoves.Remove(GetFinishMove(finishMove));
        }

        public FinishMove GetFinishMove(int finishMove)
        {
            return Record.FinishMoves.FirstOrDefault(x => x.Id == finishMove);
        }

        public FinishMoveInformations[] GetFinishMovesInformations()
        {
            return FinishMoves.Select(x => x.GetInformations()).ToArray();
        }

        #endregion FinishMove

        #region Friend & Ennemies

        public FriendsBook FriendsBook
        {
            get;
            private set;
        }

        #endregion Friend & Ennemies

        #region Merchant

        private Merchant m_merchantToSpawn;

        public bool CanEnableMerchantMode(bool sendError = true)
        {
            if (MerchantBag.Count == 0)
            {
                if (sendError)
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 23);
                return false;
            }

            if (!Map.AllowHumanVendor)
            {
                if (sendError)
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 237);

                return false;
            }

            if (Map.IsMerchantLimitReached())
            {
                if (sendError)
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 25, Map.MaxMerchantsPerMap);
                return false;
            }

            if (!Map.IsCellFree(Cell.Id, this))
            {
                if (sendError)
                    SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 24);
                return false;
            }

            if (Kamas >= (ulong)MerchantBag.GetMerchantTax())
                return true;

            if (sendError)
                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 76);

            return false;
        }

        public bool EnableMerchantMode()
        {
            if (!CanEnableMerchantMode())
                return false;

            m_merchantToSpawn = new Merchant(this);

            Inventory.SubKamas(MerchantBag.GetMerchantTax());
            MerchantManager.Instance.AddMerchantSpawn(m_merchantToSpawn.Record);
            MerchantManager.Instance.ActiveMerchant(m_merchantToSpawn);
            Client.Disconnect();

            return true;
        }

        private void CheckMerchantModeReconnection()
        {
            foreach (var merchant in MerchantManager.Instance.UnActiveMerchantFromAccount(Client.WorldAccount))
            {
                merchant.Save(WorldServer.Instance.DBAccessor.Database);

                if (merchant.Record.CharacterId != Id)
                    continue;

                MerchantBag.LoadMerchantBag(merchant.Bag);

                MerchantManager.Instance.RemoveMerchantSpawn(merchant.Record);
            }

            // if the merchant wasn't active
            var record = MerchantManager.Instance.GetMerchantSpawn(Id);
            if (record == null)
                return;

            MerchantManager.Instance.RemoveMerchantSpawn(record);
        }

        #endregion Merchant

        #region Bank

        public Bank Bank
        {
            get;
            private set;
        }

        #endregion Bank

        #region Drop Items

        public void GetDroppedItem(WorldObjectItem objectItem)
        {
            if (Inventory.IsFull(objectItem.Item, objectItem.Quantity))
            {
                //Vous ne pouvez pas porter autant d'objets.
                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 285);

                //Le nombre maximum d'objets pour cet inventaire est déjà atteint.
                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_POPUP, 6);
                return;
            }

            objectItem.Map.Leave(objectItem);
            Inventory.AddItem(objectItem.Item, objectItem.Effects, objectItem.Quantity);
        }

        public void DropItem(int itemId, int quantity)
        {
            if (quantity <= 0)
                return;

            var cell = Position.Point.GetAdjacentCells(x => Map.Cells[x].Walkable && Map.IsCellFree(x) && !Map.IsObjectItemOnCell(x)).FirstOrDefault();
            if (cell == null)
            {
                //Il n'y a pas assez de place ici.
                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 145);
                return;
            }

            var item = Inventory.TryGetItem(itemId);
            if (item == null)
                return;

            //if (item.IsLinkedToAccount() || item.IsLinkedToPlayer() || item.Template.Id == 20000) //Temporary block orb drop
            //    return;

            if (item.Stack < quantity)
            {
                //Vous ne possédez pas l'objet en quantité suffisante.
                SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 252);
                return;
            }

            Inventory.RemoveItem(item, quantity);

            var objectItem = new WorldObjectItem(item.Guid, Map, Map.Cells[cell.CellId], item.Template, item.Effects.Clone(), quantity);

            Map.Enter(objectItem);
        }

        #endregion Drop Items

        #region Debug

        public void ClearHighlight()
        {
            Client.Send(new DebugClearHighlightCellsMessage());
        }

        public Color HighlightCell(Cell cell)
        {
            var rand = new Random();
            var color = Color.FromArgb(0xFF << 24 | rand.Next(0xFFFFFF));
            HighlightCell(cell, color);

            return color;
        }

        public void HighlightCell(Cell cell, Color color)
        {
            Client.Send(new DebugHighlightCellsMessage(color.ToArgb() & 16777215, new[] { (ushort)cell.Id }));
        }

        public Color HighlightCells(IEnumerable<Cell> cells)
        {
            var rand = new Random();
            var color = Color.FromArgb(0xFF << 24 | rand.Next(0xFFFFFF));

            HighlightCells(cells, color);
            return color;
        }

        public void HighlightCells(IEnumerable<Cell> cells, Color color)
        {
            Client.Send(new DebugHighlightCellsMessage(color.ToArgb() & 16777215, cells.Select(x => (ushort)x.Id)));
        }

        #endregion Debug

        #endregion Actions

        #region Save & Load

        public bool IsLoggedIn
        {
            get;
            private set;
        }

        public bool IsAccountBlocked
        {
            get;
            private set;
        }

        public bool IsAuthSynced
        {
            get;
            set;
        }

        public static String dayOfWeek(DateTime? date)
        {
            return date.Value.ToString("dddd", new CultureInfo("es-ES"));
        }

        /// <summary>
        ///   Spawn the character on the map. It can be called once.
        /// </summary>
        public void LogIn()
        {
            if (IsInWorld)
                return;

            if (!IsInFight()) Task.Factory.StartNewDelayed(400, () => HavenBagManager.Instance.ExitHavenBag(Client));

            CharacterFighter fighter = null;
            if (Record.LeftFightId != null)
            {
                var fight = FightManager.Instance.GetFight(Record.LeftFightId.Value);

                if (fight != null)
                    fighter = fight.GetLeaver(Id);
            }

            if (fighter != null && fighter.IsDisconnected)
            {
                ContextHandler.SendGameContextDestroyMessage(Client);
                ContextHandler.SendGameContextCreateMessage(Client, 1);

                RefreshStats();
                Map.Area.AddMessage(() => {
                    RejoinFightAfterDisconnection(fighter);
                });
            }
            else
            {
                ContextHandler.SendGameContextDestroyMessage(Client);
                ContextHandler.SendGameContextCreateMessage(Client, 1);

                RefreshStats();

                Map.Area.AddMessage(() =>
                {
                    Map.Enter(this);

                    StartRegen();
                });
            }

            World.Instance.Enter(this);
            m_inWorld = true;

            SendServerMessage(Settings.MOTD, Settings.MOTDColor);

            IsLoggedIn = true;
            OnLoggedIn();

            if (Record.UnAcceptedAchievementsCSV != null)
            {
                foreach (var AchievementToUnlock in Record.m_unacceptedAchievements.ToArray())
                {
                    AchievementTemplate achievementTemplate = Singleton<AchievementManager>.Instance.TryGetAchievement(AchievementToUnlock);
                    Achievement.UnLockUnCompletedAchievement(achievementTemplate);
                }
            }
        }

        public void LogOut()
        {
            if (Area == null)
            {
                WorldServer.Instance.IOTaskPool.AddMessage(PerformLoggout);
            }
            else
            {
                Area.AddMessage(PerformLoggout);
            }
        }

        private void PerformLoggout()
        {
            lock (LoggoutSync)
            {
                IsLoggedIn = false;

                try
                {
                    OnLoggedOut();

                    if (!IsInWorld)
                        return;

                    DenyAllInvitations();

                    if (IsInRequest())
                        CancelRequest();

                    if (IsDialoging())
                        Dialog.Close();

                    if (ArenaParty != null)
                        LeaveParty(ArenaParty);

                    if (Party != null)
                        LeaveParty(Party);

                    if (Map != null && Map.IsActor(this))
                        Map.Leave(this);
                    else if (Area != null)
                        Area.Leave(this);

                    if (Map != null && m_merchantToSpawn != null)
                        Map.Enter(m_merchantToSpawn);

                    World.Instance.Leave(this);

                    m_inWorld = false;
                }
                catch (Exception ex)
                {
                    logger.Error("Cannot perfom OnLoggout actions, but trying to Save character : {0}", ex);
                }
                finally
                {
                    BlockAccount();
                    WorldServer.Instance.IOTaskPool.AddMessage(
                        () =>
                        {
                            try
                            {
                                SaveNow();
                                UnLoadRecord();
                            }
                            finally
                            {
                                Delete();
                            }
                        });

                }
            }
        }

        public void SaveLater()
        {
            BlockAccount();
            WorldServer.Instance.IOTaskPool.AddMessage(SaveNow);
        }

        internal void SaveNow()
        {
            try
            {
                WorldServer.Instance.IOTaskPool.EnsureContext();
                var database = WorldServer.Instance.DBAccessor.Database;

                lock (SaveSync)
                {
                    using (var transaction = database.GetTransaction())
                    {
                        Inventory.Save(database, false);
                        Bank.Save(database);
                        MerchantBag.Save(database);
                        Spells.Save();
                        Shortcuts.Save();
                        FriendsBook.Save();
                        Jobs.Save(database);
                        IdolInventory.Save();
                        DoppleCollection.Save(ServerBase<WorldServer>.Instance.DBAccessor.Database);
                        SaveMounts();
                        SaveQuests();

                        m_record.MapId = NextMap != null ? NextMap.Id : Map.Id;
                        m_record.CellId = Cell.Id;
                        m_record.Direction = Direction;

                        if (!CustomStatsActivated)
                        {
                            m_record.AP = PrivateStats[PlayerFields.AP].Base;
                            m_record.MP = PrivateStats[PlayerFields.MP].Base;
                            m_record.Strength = PrivateStats[PlayerFields.Strength].Base;
                            m_record.Agility = PrivateStats[PlayerFields.Agility].Base;
                            m_record.Chance = PrivateStats[PlayerFields.Chance].Base;
                            m_record.Intelligence = PrivateStats[PlayerFields.Intelligence].Base;
                            m_record.Wisdom = PrivateStats[PlayerFields.Wisdom].Base;
                            m_record.Vitality = PrivateStats[PlayerFields.Vitality].Base;

                            m_record.PermanentAddedStrength = (short)PrivateStats[PlayerFields.Strength].Additional;
                            m_record.PermanentAddedAgility = (short)PrivateStats[PlayerFields.Agility].Additional;
                            m_record.PermanentAddedChance = (short)PrivateStats[PlayerFields.Chance].Additional;
                            m_record.PermanentAddedIntelligence = (short)PrivateStats[PlayerFields.Intelligence].Additional;
                            m_record.PermanentAddedWisdom = (short)PrivateStats[PlayerFields.Wisdom].Additional;
                            m_record.PermanentAddedVitality = (short)PrivateStats[PlayerFields.Vitality].Additional;

                            m_record.BaseHealth = Stats.Health.Base;
                            m_record.DamageTaken = Stats.Health.DamageTaken;
                        }


                        database.Update(m_record);
                        database.Update(Client.WorldAccount);

                        transaction.Complete();
                    }
                }

                if (IsAuthSynced)
                    OnSaved();
                else
                {
                    IPCAccessor.Instance.SendRequest<CommonOKMessage>(new UpdateAccountMessage(Account),
                        msg =>
                        {
                            OnSaved();
                        });
                }
            }
            catch
            {
                UnBlockAccount();
                throw;
            }
        }

        public void LoadRecord()
        {
            Breed = BreedManager.Instance.GetBreed(BreedId);
            Head = BreedManager.Instance.GetHead(Record.Head);
            var map = World.Instance.GetMap(m_record.MapId);

            if (map == null)
            {
                map = World.Instance.GetMap(Breed.StartMap);
                m_record.CellId = Breed.StartCell;
                m_record.Direction = Breed.StartDirection;
            }

            Position = new ObjectPosition(
                map,
                map.Cells[m_record.CellId],
                m_record.Direction);

            Stats = new StatsFields(this);
            Stats.Initialize(m_record);
            Level = ExperienceManager.Instance.GetCharacterLevel(Experience);
            LowerBoundExperience = ExperienceManager.Instance.GetCharacterLevelExperience(Level);
            UpperBoundExperience = ExperienceManager.Instance.GetCharacterNextLevelExperience(Level);

            AlignmentGrade = (sbyte)ExperienceManager.Instance.GetAlignementGrade(m_record.Honor);
            LowerBoundHonor = ExperienceManager.Instance.GetAlignementGradeHonor((byte)AlignmentGrade);
            UpperBoundHonor = ExperienceManager.Instance.GetAlignementNextGradeHonor((byte)AlignmentGrade);
            DoppleCollection = new DoppleCollection();
            DoppleCollection.Load(Id);
            Inventory = new Inventory(this);
            Inventory.LoadInventory();
            IdolInventory = new IdolInventory(this);

            Bank = new Bank(this);
            Bank.LoadRecord();

            try
            {
                Achievement = new PlayerAchievement(this);
                Achievement.LoadAchievements();
            }
            catch { }

            MerchantBag = new CharacterMerchantBag(this);
            CheckMerchantModeReconnection();
            MerchantBag.LoadMerchantBag();

            GuildMember = GuildManager.Instance.TryGetGuildMember(Id);

            UpdateLook(false);

            LoadMounts();

            Spells = new SpellInventory(this);
            Spells.LoadSpells();

            Shortcuts = new ShortcutBar(this);
            Shortcuts.Load();

            FriendsBook = new FriendsBook(this);
            FriendsBook.Load();

            ChatHistory = new ChatHistory(this);

            LoadQuests();

            Jobs = new JobsCollection(this);
            Jobs.LoadJobs();

            m_recordLoaded = true;
        }

        private void UnLoadRecord()
        {
            if (!m_recordLoaded)
                return;

            m_recordLoaded = false;
        }

        private void BlockAccount()
        {
            AccountManager.Instance.BlockAccount(Client.WorldAccount, this);
            IsAccountBlocked = true;
        }

        private void UnBlockAccount()
        {
            if (!IsAccountBlocked)
                return;

            AccountManager.Instance.UnBlockAccount(Client.WorldAccount);
            IsAccountBlocked = false;

            OnAccountUnblocked();
        }

        #endregion Save & Load

        #region Exceptions

        private readonly List<KeyValuePair<string, Exception>> m_commandsError = new List<KeyValuePair<string, Exception>>();
        private Mount m_equippedMount;
        private ActorLook m_look;

        public List<KeyValuePair<string, Exception>> CommandsErrors => m_commandsError;

        #endregion Exceptions

        #region Network

        #region GameRolePlayCharacterInformations

        public override GameContextActorInformations GetGameContextActorInformations(Character character)
        {
            return new GameRolePlayCharacterInformations(
                Id,
                Look.GetEntityLook(),
                GetEntityDispositionInformations(),
                Name,
                GetHumanInformations(),
                Account.Id,
                GetActorAlignmentInformations());
        }

        #endregion GameRolePlayCharacterInformations

        #region ActorAlignmentInformations

        public ActorAlignmentInformations GetActorAlignmentInformations()
        {
            return new ActorAlignmentInformations(
                (sbyte)AlignmentSide,
                AlignmentValue,
                PvPEnabled ? AlignmentGrade : (sbyte)0,
                CharacterPower);
        }

        #endregion ActorAlignmentInformations

        #region ActorExtendedAlignmentInformations

        public ActorExtendedAlignmentInformations GetActorAlignmentExtendInformations()
        {
            return new ActorExtendedAlignmentInformations(
                (sbyte)AlignmentSide,
                AlignmentValue,
                PvPEnabled ? AlignmentGrade : (sbyte)0,
                CharacterPower,
                (ushort)Honor,
                (ushort)LowerBoundHonor,
                (ushort)UpperBoundHonor,
                (PvPEnabled ? (sbyte)AggressableStatusEnum.PvP_ENABLED_AGGRESSABLE : (sbyte)AggressableStatusEnum.NON_AGGRESSABLE));
        }

        #endregion ActorExtendedAlignmentInformations

        #region CharacterBaseInformations

        public CharacterBaseInformations GetCharacterBaseInformations()
            => new CharacterBaseInformations(
                (ulong)Id,
                Name,
                (ushort)Level,
                Look.GetEntityLook(),
                (sbyte)BreedId,
                Sex == SexTypeEnum.SEX_FEMALE);

        public CharacterMinimalPlusLookInformations GetCharacterMinimalPlusLookInformations()
            => new CharacterMinimalPlusLookInformations(
                (ulong)Id,
                Name,
                (ushort)Level,
                Look.GetEntityLook(),
                (sbyte)Breed.Id);

        public CharacterMinimalInformations GetCharacterMinimalInformations()
            => new CharacterMinimalInformations(
                (ulong)Id,
                Name,
                (ushort)Level);

        public CharacterCharacteristicsInformations GetCharacterCharacteristicsInformations()
            => new CharacterCharacteristicsInformations(
                        (ulong)Experience, // EXPERIENCE
                        (ulong)LowerBoundExperience, // EXPERIENCE level floor
                        (ulong)UpperBoundExperience, // EXPERIENCE nextlevel floor
                        (ulong)UpperBoundExperience, // TODO: EXPERIENCE bonus limit

                        Kamas, // Amount of kamas.

                        (ushort)StatsPoints, // Stats points
                        0, // Additionnal points
                        (ushort)SpellsPoints, // Spell points

                        // Alignment
                        GetActorAlignmentExtendInformations(),
                        (uint)Stats.Health.Total, // Life points
                        (uint)Stats.Health.TotalMax, // Max Life points

                        (ushort)Energy, // Energy points
                        (ushort)EnergyMax, // maxEnergyPoints

                        (short)Stats[PlayerFields.AP]
                                    .Total, // actionPointsCurrent
                        (short)Stats[PlayerFields.MP]
                                    .Total, // movementPointsCurrent

                        Stats[PlayerFields.Initiative],
                        Stats[PlayerFields.Prospecting],
                        Stats[PlayerFields.AP],
                        Stats[PlayerFields.MP],
                        Stats[PlayerFields.Strength],
                        Stats[PlayerFields.Vitality],
                        Stats[PlayerFields.Wisdom],
                        Stats[PlayerFields.Chance],
                        Stats[PlayerFields.Agility],
                        Stats[PlayerFields.Intelligence],
                        Stats[PlayerFields.Range],
                        Stats[PlayerFields.SummonLimit],
                        Stats[PlayerFields.DamageReflection],
                        Stats[PlayerFields.CriticalHit],
                        (ushort)Inventory.WeaponCriticalHit,
                        Stats[PlayerFields.CriticalMiss],
                        Stats[PlayerFields.HealBonus],
                        Stats[PlayerFields.DamageBonus],
                        Stats[PlayerFields.WeaponDamageBonus],
                        Stats[PlayerFields.DamageBonusPercent],
                        Stats[PlayerFields.TrapBonus],
                        Stats[PlayerFields.TrapBonusPercent],
                        Stats[PlayerFields.GlyphBonusPercent],
                        Stats[PlayerFields.RuneBonusPercent],
                        Stats[PlayerFields.PermanentDamagePercent],
                        Stats[PlayerFields.TackleBlock],
                        Stats[PlayerFields.TackleEvade],
                        Stats[PlayerFields.APAttack],
                        Stats[PlayerFields.MPAttack],
                        Stats[PlayerFields.PushDamageBonus],
                        Stats[PlayerFields.CriticalDamageBonus],
                        Stats[PlayerFields.NeutralDamageBonus],
                        Stats[PlayerFields.EarthDamageBonus],
                        Stats[PlayerFields.WaterDamageBonus],
                        Stats[PlayerFields.AirDamageBonus],
                        Stats[PlayerFields.FireDamageBonus],
                        Stats[PlayerFields.DodgeAPProbability],
                        Stats[PlayerFields.DodgeMPProbability],
                        Stats[PlayerFields.NeutralResistPercent],
                        Stats[PlayerFields.EarthResistPercent],
                        Stats[PlayerFields.WaterResistPercent],
                        Stats[PlayerFields.AirResistPercent],
                        Stats[PlayerFields.FireResistPercent],
                        Stats[PlayerFields.NeutralElementReduction],
                        Stats[PlayerFields.EarthElementReduction],
                        Stats[PlayerFields.WaterElementReduction],
                        Stats[PlayerFields.AirElementReduction],
                        Stats[PlayerFields.FireElementReduction],
                        Stats[PlayerFields.PushDamageReduction],
                        Stats[PlayerFields.CriticalDamageReduction],
                        Stats[PlayerFields.PvpNeutralResistPercent],
                        Stats[PlayerFields.PvpEarthResistPercent],
                        Stats[PlayerFields.PvpWaterResistPercent],
                        Stats[PlayerFields.PvpAirResistPercent],
                        Stats[PlayerFields.PvpFireResistPercent],
                        Stats[PlayerFields.PvpNeutralElementReduction],
                        Stats[PlayerFields.PvpEarthElementReduction],
                        Stats[PlayerFields.PvpWaterElementReduction],
                        Stats[PlayerFields.PvpAirElementReduction],
                        Stats[PlayerFields.PvpFireElementReduction],
                        Stats[PlayerFields.MeleeDamageDonePercent],
                        Stats[PlayerFields.MeleeDamageReceivedPercent],
                        Stats[PlayerFields.RangedDamageDonePercent],
                        Stats[PlayerFields.RangedDamageReceivedPercent],
                        Stats[PlayerFields.WeaponDamageDonePercent],
                        Stats[PlayerFields.WeaponDamageReceivedPercent],
                        Stats[PlayerFields.SpellDamageDonePercent],
                        Stats[PlayerFields.SpellDamageReceivedPercent],
                        SpellsModifications.ToArray(),
                        0);

        #endregion CharacterBaseInformations

        #region PartyMemberInformations

        public PartyInvitationMemberInformations GetPartyInvitationMemberInformations()
            => new PartyInvitationMemberInformations(
                (ulong)Id,
                Name,
                Level,
                Look.GetEntityLook(),
                (sbyte)BreedId,
                Sex == SexTypeEnum.SEX_FEMALE,
                (short)Map.Position.X,
                (short)Map.Position.Y,
                Map.Id,
                (ushort)Map.SubArea.Id,
                Companion == null ? new PartyEntityMemberInformation[0] : new PartyEntityMemberInformation[] { Companion.GetPartyCompanionMemberInformations() });



        public PartyMemberInformations GetPartyMemberInformations()
            => new PartyMemberInformations(
                (ulong)Id,
                Name,
                Level,
                Look.GetEntityLook(),
                (sbyte)BreedId,
                Sex == SexTypeEnum.SEX_FEMALE,
                (uint)LifePoints,
                (uint)MaxLifePoints,
                (ushort)Stats[PlayerFields.Prospecting].Total,
                (byte)RegenSpeed,
                (ushort)Stats[PlayerFields.Initiative].Total,
                (sbyte)AlignmentSide,
                (short)Map.Position.X,
                (short)Map.Position.Y,
                Map.Id,
                (ushort)SubArea.Id,
                Status,
                Companion == null ? new PartyEntityMemberInformation[0] : new PartyEntityMemberInformation[] { Companion.GetPartyCompanionMemberInformations() });

        public PartyGuestInformations GetPartyGuestInformations(Party party)
        {
            if (!m_partyInvitations.ContainsKey(party.Id))
                return new PartyGuestInformations();

            var invitation = m_partyInvitations[party.Id];

            return new PartyGuestInformations(
                (ulong)Id,
                (ulong)invitation.Source.Id,
                Name,
                Look.GetEntityLook(),
                (sbyte)BreedId,
                Sex == SexTypeEnum.SEX_FEMALE,
                Status,
                Companion == null ? new PartyEntityMemberInformation[0] : new PartyEntityMemberInformation[] { Companion.GetPartyCompanionMemberInformations() });
        }

        public PartyMemberArenaInformations GetPartyMemberArenaInformations()
            => new PartyMemberArenaInformations(
                (ulong)Id,
                Name,
                (byte)Level,
                Look.GetEntityLook(),
                (sbyte)BreedId,
                Sex == SexTypeEnum.SEX_FEMALE,
                (uint)LifePoints,
                (uint)MaxLifePoints,
                (ushort)Stats[PlayerFields.Prospecting].Total,
                (byte)RegenSpeed,
                (ushort)Stats[PlayerFields.Initiative].Total,
                (sbyte)AlignmentSide,
                (short)Map.Position.X,
                (short)Map.Position.Y,
                Map.Id,
                (ushort)SubArea.Id,
                Status,
                Companion == null ? new PartyEntityMemberInformation[0] : new PartyEntityMemberInformation[] { Companion.GetPartyCompanionMemberInformations() },
                (ushort)ArenaRank_3vs3);

        #endregion PartyMemberInformations

        public override ActorRestrictionsInformations GetActorRestrictionsInformations()
        {
            return new ActorRestrictionsInformations(
                                !Map.AllowAggression || IsGhost(), // cantBeAgressed
                                !Map.AllowChallenge || IsGhost(), // cantBeChallenged
                                !Map.AllowExchangesBetweenPlayers || IsGhost(), // cantTrade
                                IsGhost(), // cantBeAttackedByMutant
                                false, // cantRun
                                false, // forceSlowWalk
                                false, // cantMinimize
                                PlayerLifeStatus == PlayerLifeStatusEnum.STATUS_TOMBSTONE, // cantMove

                                !Map.AllowAggression || IsGhost(), // cantAggress
                                IsGhost(), // cantChallenge
                                IsGhost(), // cantExchange
                                IsGhost(), // cantAttack
                                false, // cantChat
                                IsGhost(), // cantBeMerchant
                                IsGhost(), // cantUseObject
                                IsGhost(), // cantUseTaxCollector

                                IsGhost(), // cantUseInteractive
                                IsGhost(), // cantSpeakToNPC
                                false, // cantChangeZone
                                IsGhost(), // cantAttackMonster
                                false // cantWalk8Directions
                                );
        }

        public override HumanInformations GetHumanInformations()
        {
            var human = base.GetHumanInformations();

            var options = new List<HumanOption>();

            if (Guild != null)
                options.Add(new HumanOptionGuild(Guild.GetGuildInformations()));

            if (SelectedTitle != null)
                options.Add(new HumanOptionTitle((ushort)SelectedTitle.Value, string.Empty));

            if (SelectedOrnament != null)
                options.Add(new HumanOptionOrnament((ushort)SelectedOrnament.Value, Level, (short)Record.LeagueId, 0));

            if (LastEmoteUsed != null)
                options.Add(new HumanOptionEmote((byte)LastEmoteUsed.First.Id, LastEmoteUsed.Second.GetUnixTimeStampLong()));

            if (LastSkillUsed != null)
                options.Add(new HumanOptionSkillUse((uint)LastSkillUsed.InteractiveObject.Id, (ushort)LastSkillUsed.SkillTemplate.Id, LastSkillUsed.SkillEndTime.GetUnixTimeStampLong()));

            human.Options = options.ToArray();
            return human;
        }

        #endregion Network

        public CharacterRecord Record => m_record;

        public override bool CanBeSee(WorldObject byObj) => base.CanBeSee(byObj) && (byObj == this || !Invisible) && Game.HavenBags.HavenBagManager.Instance.CanBeSeenInHavenBag(byObj, this) && byObj is Character && (byObj == this || !Map.IsInstantiated);

        protected override void OnDisposed()
        {
            if (FriendsBook != null)
                FriendsBook.Dispose();

            if (Inventory != null)
                Inventory.Dispose();

            base.OnDisposed();
        }

        public override string ToString() => string.Format("{0} ({1})", Name, Id);
    }
}

#endregion