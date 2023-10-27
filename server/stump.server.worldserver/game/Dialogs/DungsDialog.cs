using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Dialogs;
using Stump.DofusProtocol.Types;

namespace Stump.Server.WorldServer.Game.Dialogs.Interactives
{
    public class DungsDialog : IDialog
    {
        readonly Dictionary<Map, int> m_destinations = new Dictionary<Map, int>();

        public DungsDialog(Character character, Dictionary<Map, int> destinations, bool isDungeon = false)
        {
            Character = character;
            IsDungeon = isDungeon;
            m_destinations = destinations;
        }

        public DialogTypeEnum DialogType => DialogTypeEnum.DIALOG_TELEPORTER;

        public Character Character
        {
            get;
        }

        private bool IsDungeon
        {
            get;
            set;
        }

        public void AddDestination(Map map, int cellId)
        {
            m_destinations.Add(map, cellId);
        }

        public void Open()
        {
            Character.SetDialog(this);
            SendZaapListMessage(Character.Client);
        }

        public void Close()
        {
            Character.CloseDialog(this);
            DialogHandler.SendLeaveDialogMessage(Character.Client, DialogType);
        }

        public void Teleport(Map map)
        {
            if (!m_destinations.ContainsKey(map))
                return;

            Cell cell;
            
            cell = map.GetCell(m_destinations.Where(x => x.Key == map).FirstOrDefault().Value);

            if (!map.IsCellWalkable(cell))
                cell = map.GetFirstFreeCellNearMiddle();

            Character.Teleport(map, cell);

            if (IsDungeon)
                Character.Record.DungeonDone.Add(map.Id);

            Close();
        }

        public void SendZaapListMessage(IPacketReceiver client)
        {
            client.Send(
                new TeleportDestinationsMessage
                (
                (sbyte)TeleporterTypeEnum.TELEPORTER_ZAAP,
                m_destinations.Select(entry => new TeleportDestination((sbyte)TeleporterTypeEnum.TELEPORTER_ZAAP, entry.Key.Id, (ushort)entry.Key.SubArea.Id, (ushort)entry.Key.SubArea.Record.Level, (ushort)entry.Key.SubArea.Record.Level)).ToArray()
                ));
        }

        public short GetCostTo(Map map)
        {
            return 0;
        }
    }
}