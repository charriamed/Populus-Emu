﻿using System;
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
    public class ZaapDialog : IDialog
    {
        readonly List<Map> m_destinations = new List<Map>();

        public ZaapDialog(Character character, InteractiveObject zaap)
        {
            Character = character;
            Zaap = zaap;
        }

        public ZaapDialog(Character character, InteractiveObject zaap, IEnumerable<Map> destinations)
        {
            Character = character;
            Zaap = zaap;
            m_destinations = destinations.ToList();
        }

        public DialogTypeEnum DialogType => DialogTypeEnum.DIALOG_TELEPORTER;

        public Character Character
        {
            get;
        }

        public InteractiveObject Zaap
        {
            get;
        }

        public void AddDestination(Map map)
        {
            m_destinations.Add(map);
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
            if (!m_destinations.Contains(map))
                return;

            Cell cell;

            if (map.Zaap != null)
            {
                cell = map.GetCell(map.Zaap.Position.Point.GetCellInDirection(DirectionsEnum.DIRECTION_SOUTH_WEST, 1).CellId);

                if (!cell.Walkable)
                {
                    var adjacents = map.Zaap.Position.Point.GetAdjacentCells(entry => map.GetCell(entry).Walkable).ToArray();

                    if (adjacents.Length == 0)
                        throw new Exception(string.Format("Cannot find a free adjacent cell near the zaap (id:{0}) on map {1}", map.Zaap.Id, map.Id));

                    cell = map.GetCell(adjacents[0].CellId);
                }
            }
            else
                cell = map.GetFirstFreeCellNearMiddle();

            var cost = GetCostTo(map);

            if (Character.Kamas < (ulong)cost)
                return;

            Character.Inventory.SubKamas((ulong)cost);
            Character.Teleport(map, cell);

            //HAVENBAGS
            Character.Record.IsInHavenBag = false;

            Close();
        }

        public void SendZaapListMessage(IPacketReceiver client)
        {
            client.Send(
                new ZaapDestinationsMessage
                (
                (sbyte)TeleporterTypeEnum.TELEPORTER_ZAAP,
                m_destinations.Select(entry => new TeleportDestination((sbyte)TeleporterTypeEnum.TELEPORTER_ZAAP, entry.Id, (ushort)entry.SubArea.Id, (ushort)entry.SubArea.Record.Level, (ushort)GetCostTo(entry))).ToArray(),
                Character.Record.SpawnMapId ?? 0
                ));
        }

        public short GetCostTo(Map map)
        {
            var pos = map.Position;
            var pos2 = Zaap.Map.Position;

            return (short)Math.Floor(Math.Sqrt(( pos2.X - pos.X ) * ( pos2.X - pos.X ) + ( pos2.Y - pos.Y ) * ( pos2.Y - pos.Y )) * 10);
        }
    }
}