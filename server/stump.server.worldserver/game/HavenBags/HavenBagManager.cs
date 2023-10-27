using NLog;
using Stump.Core.Extensions;
using Stump.Core.IO;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Core.Network;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.Items.Shops;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items.Player;
using System.Collections.Generic;
using Stump.Server.WorldServer.Database.HavenBags;
using System.IO;
using System.Linq;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Database;

namespace Stump.Server.WorldServer.Game.HavenBags
{
    //saad:ENCORE MOI QUI FAIT TOUT
    //nex:ftg fdp avec ton code dégeulasse
    public class HavenBagManager : DataManager<HavenBagManager>, ISaveable
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        readonly object m_lock = new object();

        private List<HavenBagRecord> m_havenBags = new List<HavenBagRecord>();
        private List<HavenBagRecord> m_newhavenBags = new List<HavenBagRecord>();
        private List<HavenBagRecord> m_deletehavenBags = new List<HavenBagRecord>();

        private List<HavenBagFurnitureRecord> m_havenBagFurnitures = new List<HavenBagFurnitureRecord>();
        private List<HavenBagFurnitureRecord> m_newhavenBagFurnitures = new List<HavenBagFurnitureRecord>();
        private List<HavenBagFurnitureRecord> m_deletehavenBagFurnitures = new List<HavenBagFurnitureRecord>();

        private List<HavenBagThemes> m_havenBagThemes = new List<HavenBagThemes>();
        private List<HavenBagInvitationRequest> Invitations = new List<HavenBagInvitationRequest>();

        [Initialization(InitializationPass.Seventh)]
        public override void Initialize()
        {
            var database = WorldServer.Instance.DBAccessor.Database;
            m_havenBags = database.Fetch<HavenBagRecord>(HavenBagRelator.FetchQuery);
            m_havenBagFurnitures = database.Fetch<HavenBagFurnitureRecord>(HavenBagFurnitureRelator.FetchQuery);
            m_havenBagThemes = database.Fetch<HavenBagThemes>(HavenBagThemesRelator.FetchQuery);
            World.Instance.RegisterSaveableInstance(this);

        }

        public void AddInvitation(HavenBagInvitationRequest request)
        {
            if (Invitations.Any(x => x.Target == request.Target && x.Source == request.Source))
            {
                Invitations.FirstOrDefault(x => x.Target == request.Target && x.Source == request.Source).Open();
            }
            else if (Invitations.Any(x => x.Target == request.Target))
            {
                return;
            }
            else
            {
                request.Open();
                Invitations.Add(request);
            }
        }

        public void HandleInvitation(Character character, bool Accept)
        {
            var invit = Invitations.FirstOrDefault(x => x.Target == character);
            if (invit == null) return;
            if (Accept && !character.IsInFight()) invit.Accept();
            else invit.Cancel();
            Invitations.Remove(invit);
        }

        public List<HavenBagRecord> GetHavenBags()
        {
            return m_havenBags;
        }
        public List<HavenBagThemes> GetHavenBagThemes()
        {
            return m_havenBagThemes;
        }
        public List<HavenBagFurnitureRecord> GetHavenBagFurnitures()
        {
            return m_havenBagFurnitures;
        }

        public int GetNextHavenBagId()
        {
            if (m_havenBags.Count() < 1) return 1;
            return m_havenBags.OrderByDescending(x => x.HavenBagId).FirstOrDefault().HavenBagId + 1;
        }

        public void HavenBagFurnituresUpdate(WorldClient client, HavenBagFurnituresRequestMessage message)
        {
            var havenbag = m_havenBags.FirstOrDefault(x => x.OwnerId == client.Character.Id);
            if (havenbag == null)
                return;
            var havenbagfurnitures = m_havenBagFurnitures.Where(x => x.HavenBagId == havenbag.HavenBagId).ToList();
            if (havenbagfurnitures.Count() > 0)
            {
                foreach (var x in havenbagfurnitures)
                {
                    m_deletehavenBagFurnitures.Add(x);
                    m_havenBagFurnitures.Remove(x);
                }
            }
            if (message.FunitureIds.Count() < 1)
            {
                return;
            }
            

            int index;
            for (index = 0; index < message.FunitureIds.Count(); index++)
            {
                var furniture = message.FunitureIds.ElementAt(index);
                var cellid = message.CellIds.ElementAt(index);
                var Orientation = message.Orientations.ElementAt(index);
                var record = new HavenBagFurnitureRecord()
                {
                    HavenBagId = havenbag.HavenBagId,
                    CellId = (ushort)cellid,
                    Orientation = (sbyte)Orientation,
                    FurnitureId = furniture
                };
                m_newhavenBagFurnitures.Add(record);
                m_havenBagFurnitures.Add(record);
            }

            var havenbagfurnituresx = m_havenBagFurnitures.Where(x => x.HavenBagId == havenbag.HavenBagId).Select(v => v.HavenBagFurnitureInformation).ToArray();
            foreach (var cli in World.Instance.GetCharacters().Where(x => x.Record.IsInHavenBag && x.Record.HavenBagOwnerId == client.Character.Id).Select(c => c.Client))
                ContextHandler.SendHavenBagFurnituresMessage(cli, havenbagfurnituresx);
            client.Send(new EditHavenBagFinishedMessage());
        }

        public void HandleHavenBagEnter(WorldClient client, int HavenBagOwnerId, bool Force = false)
        {
            if (client.Character.Area.Id == 42) return;
            if (!client.Character.Record.IsInHavenBag)
            {
                var previousmapid = client.Character.Map.Id;
                var previousCellId = client.Character.Cell.Id;
                if (EnterHavenBag(client, HavenBagOwnerId, Force))
                {
                    if (!client.Character.Record.IsInHavenBag)
                    {
                        client.Character.Record.MapBeforeHavenBagId = previousmapid;
                        client.Character.Record.CellBeforeHavenBagId = previousCellId;
                    }
                    client.Character.Record.HavenBagOwnerId = HavenBagOwnerId;
                    client.Character.Record.IsInHavenBag = true;

                }
            }
            else
            {
                ExitHavenBag(client);
            }
        }

        public void AddHavenBag(Character character, sbyte ThemeId)
        {
            if (character.Record.HavenBagsCSV == null || character.Record.HavenBagsCSV.Length < 1 || !character.Record.HavenBags.Contains(ThemeId))
            {
                var newcsv = character.Record.HavenBagsCSV + ";" + ThemeId.ToString();
                character.Record.HavenBagsCSV = newcsv;
            }
            SendHavenBagPackMessage(character.Client);
        }

        public void DeleteHavenBag(Character character, sbyte ThemeId)
        {
            if (character.Record.HavenBags.Contains(ThemeId))
            {
                var newhb = character.Record.HavenBags.ToList();
                newhb.Remove(ThemeId);
                character.Record.HavenBags = newhb.ToArray();
            }
            SendHavenBagPackMessage(character.Client);
        }

        public void SendHavenBagPackMessage(WorldClient client)
        {
            client.Send(new HavenBagPackListMessage(client.Character.Record.HavenBags));
        }

        public void UpdateHavenBagTheme(WorldClient client, ChangeThemeRequestMessage message)
        {
            var havenbag = m_havenBags.FirstOrDefault(x => x.OwnerId == client.Character.Id);
            if (havenbag == null) return;
            havenbag.ThemeId = message.Theme;
            var havenbagfurnitures = m_havenBagFurnitures.Where(x => x.HavenBagId == havenbag.HavenBagId).ToList();
            if (havenbagfurnitures.Count() > 0) foreach (var x in havenbagfurnitures)
                {
                    m_deletehavenBagFurnitures.Add(x);
                    m_havenBagFurnitures.Remove(x);
                }
            var havenbagfurniturerecord = new HavenBagFurnitureRecord()
            {
                HavenBagId = havenbag.HavenBagId
            };
            m_newhavenBagFurnitures.Add(havenbagfurniturerecord);
            m_havenBagFurnitures.Add(havenbagfurniturerecord);

            World.Instance.GetCharacters().Where(x => x.Record.IsInHavenBag && x.Record.HavenBagOwnerId == client.Character.Id && x != client.Character).ToList().ForEach(c => ExitHavenBag(c.Client));
            EnterHavenBag(client, client.Character.Id);
        }

        public void UpdateHavenBagPermissions(WorldClient client, HavenBagPermissionsUpdateRequestMessage message)
        {
            var havenbag = m_havenBags.FirstOrDefault(x => x.OwnerId == client.Character.Id);
            if (havenbag != null)
            {
                switch (message.Permissions)
                {
                    case 0:
                        havenbag.GuildAllowed = false; havenbag.FriendsAllowed = false;
                        break;
                    case 1:
                        havenbag.GuildAllowed = false; havenbag.FriendsAllowed = true;
                        break;
                    case 2:
                        havenbag.GuildAllowed = true; havenbag.FriendsAllowed = false;
                        break;
                    case 3:
                        havenbag.GuildAllowed = true; havenbag.FriendsAllowed = true;
                        break;
                }
            }
        }

        public int GetHavenBagPermissions(bool AllowedFriends, bool AllowedGuild)
        {
            bool n0 = !AllowedFriends && !AllowedGuild;
            bool n1 = AllowedFriends && !AllowedGuild;
            bool n2 = !AllowedFriends && AllowedGuild;
            bool n3 = AllowedFriends && AllowedGuild;

            if (n0) return 0;
            else if (n1) return 1;
            else if (n2) return 2;
            else if (n3) return 3;
            return 0;
        }

        public void ExitHavenBag(WorldClient client)
        {
            if (!client.Character.Record.IsInHavenBag && !GetHavenBagThemes().Any(x => x.MapId == client.Character.Map.Id)) return;
            client.Character.Record.IsInHavenBag = false;
            client.Character.Teleport(new Maps.Cells.ObjectPosition(World.Instance.GetMap(client.Character.Record.MapBeforeHavenBagId), World.Instance.GetMap(client.Character.Record.MapBeforeHavenBagId).GetCell(client.Character.Record.CellBeforeHavenBagId), DirectionsEnum.DIRECTION_SOUTH_EAST));
        }

        public bool EnterHavenBag(WorldClient client, int HavenBagOwnerId, bool Force = false)
        {
            if (client.Character.Id == HavenBagOwnerId)
            {
                var havenbag = m_havenBags.FirstOrDefault(x => x.OwnerId == HavenBagOwnerId);
                if (havenbag == null)
                {
                    var newhavenbag = new HavenBagRecord()
                    {
                        HavenBagId = GetNextHavenBagId(),
                        OwnerId = HavenBagOwnerId,
                        ThemeId = 2,
                        RoomId = 0,
                        MaxRoom = 1,
                        FriendsAllowed = false,
                        GuildAllowed = false
                    };

                    m_newhavenBags.Add(newhavenbag);
                    m_havenBags.Add(newhavenbag);

                    var havenbagfurniturerecord = new HavenBagFurnitureRecord()
                    {
                        HavenBagId = newhavenbag.HavenBagId
                    };
                    m_newhavenBagFurnitures.Add(havenbagfurniturerecord);
                    m_havenBagFurnitures.Add(havenbagfurniturerecord);
                }
                havenbag = m_havenBags.FirstOrDefault(x => x.OwnerId == HavenBagOwnerId);
                int havenBagMapId = m_havenBagThemes.FirstOrDefault(x => x.Id == havenbag.ThemeId).MapId;
                client.Character.Teleport(new Maps.Cells.ObjectPosition(World.Instance.GetMap(havenBagMapId), (short)m_havenBagThemes.FirstOrDefault(x => x.Id == havenbag.ThemeId).CellId, DirectionsEnum.DIRECTION_SOUTH_EAST));
                return true;
            }
            else
            {
                var havenbag = m_havenBags.FirstOrDefault(x => x.OwnerId == HavenBagOwnerId);
                if (havenbag == null)
                    return false;
                var owner = World.Instance.GetCharacter(havenbag.OwnerId);
                bool Friend = client.Character.FriendsBook.IsFriend(owner.Account.Id) && havenbag.FriendsAllowed;
                bool Guild = client.Character.Guild != null && owner.Guild != null && client.Character.Guild == owner.Guild && havenbag.GuildAllowed;
                if (Guild || Friend || Force)
                {
                    int havenBagMapId = m_havenBagThemes.FirstOrDefault(x => x.Id == havenbag.ThemeId).MapId;
                    client.Character.Teleport(new Maps.Cells.ObjectPosition(World.Instance.GetMap(havenBagMapId), (short)m_havenBagThemes.FirstOrDefault(x => x.Id == havenbag.ThemeId).CellId, DirectionsEnum.DIRECTION_SOUTH_EAST));
                    return true;
                }

                return false;
            }
        }

        #region GetHaveBagInformations
        public bool IsHavenBagMap(int MapId)
        {
            return m_havenBagThemes.FirstOrDefault(x => x.MapId == MapId) != null;
        }

        public bool CanBeSeenInHavenBag(WorldObject x, Character character)
        {
            if (!(x is Character)) return true;
            var tobeseen = x as Character;
            if (!IsHavenBagMap(tobeseen.Map.Id)) return true;
            if (tobeseen.Record.HavenBagOwnerId != character.Record.HavenBagOwnerId) return false;
            return true;
        }
        public HavenBagRecord GetHavenBagByOwner(Character character)
        {
            if (character == null) return null;
            return m_havenBags.FirstOrDefault(x => x.OwnerId == character.Id);
        }
        public HavenBagRecord GetHavenBagByOwner(int id)
        {
            return m_havenBags.FirstOrDefault(x => x.OwnerId == id);
        }
        public Character GetHavenBagOwner(Character character)
        {
            if (character == null) return null;
            return World.Instance.GetCharacter(m_havenBags.FirstOrDefault(x => x.OwnerId == character.Record.HavenBagOwnerId).OwnerId);
        }

        public sbyte GetHavenBagTheme(Character character)
        {
            if (character == null) return 0;
            return m_havenBags.FirstOrDefault(x => x.OwnerId == character.Record.HavenBagOwnerId).ThemeId;
        }

        public sbyte GetHavenBagRoomId(Character character)
        {
            if (character == null) return 0;
            return m_havenBags.FirstOrDefault(x => x.OwnerId == character.Record.HavenBagOwnerId).RoomId;
        }

        public sbyte GetHavenBagMaxRoom(Character character)
        {
            if (character == null) return 0;
            return m_havenBags.FirstOrDefault(x => x.OwnerId == character.Record.HavenBagOwnerId).MaxRoom;
        }
        #endregion

        public void Save()
        {
            lock (m_lock)
            {
                #region havenbags

                if (m_newhavenBags.Count > 0)
                {
                    foreach (var record in m_newhavenBags)
                    {
                        Database.Insert(record);
                    }
                    m_newhavenBags.Clear();
                }

                if (m_deletehavenBags.Count > 0)
                {
                    foreach (var record in m_deletehavenBags)
                    {
                        Database.Delete(record);
                    }
                    m_deletehavenBags.Clear();
                }

                if (m_havenBags.Count > 0)
                {
                    foreach (var record in m_havenBags)
                    {
                        Database.Save(record);
                    }
                }

                #endregion

                #region havenbags furnitures

                if (m_newhavenBagFurnitures.Count > 0)
                {
                    foreach (var record in m_newhavenBagFurnitures)
                    {
                        Database.Insert(record);
                    }
                    m_newhavenBagFurnitures.Clear();
                }

                if (m_deletehavenBagFurnitures.Count > 0)
                {
                    foreach (var record in m_deletehavenBagFurnitures)
                    {
                        Database.Delete(record);
                    }
                    m_deletehavenBagFurnitures.Clear();
                }

                if (m_havenBagFurnitures.Count > 0)
                {
                    foreach (var record in m_havenBagFurnitures)
                    {
                        Database.Save(record);
                    }
                }

                #endregion
            }
        }

    }
}