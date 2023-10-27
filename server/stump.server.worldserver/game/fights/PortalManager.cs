using Stump.ORM.SubSonic.Extensions;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightPortalsManager
    {
        public const int PortalsLimitPerTeam = 4;

        public FightPortalsManager(IFight fight)
        {
            Fight = fight;
            Map = fight.Map;
        }

        private IFight Fight
        {
            get;
            set;
        }
        private Map Map
        {
            get;
            set;
        }

        #region Edit & Refresh Portals

        public void RefreshGamePortals()
        {
            if (Fight.GetTriggers().OfType<Portal>() == null || Fight.GetTriggers().OfType<Portal>().Count() < 1) return;
            foreach (var portal in Fight.GetTriggers().OfType<Portal>())
            {
                if (!portal.IsNeutral)
                {
                    if (!portal.IsActive)
                    {
                        portal.IsActive = true;
                    }
                }
                else
                {
                    if (Fight.FighterPlaying == portal.Caster)
                    {
                        portal.IsActive = true;
                        portal.IsNeutral = false;
                    }
                    else
                        portal.IsActive = false;
                }
            }
            RefreshClientsPortals();
        }

        public void RefreshClientsPortals()
        {
            try
            {
                if (Fight.GetTriggers().OfType<Portal>() == null) return;
                foreach (var verifportal in Fight.GetTriggers().OfType<Portal>())
                {
                    if (GetActivableGamePortalsCount(verifportal.Caster.Team) < 2 && verifportal.IsActive)
                    {
                        if (verifportal.IsInGameActive)
                        {
                            ContextHandler.SendGameActionFightActivateGlyphTrapMessage(Fight.Clients, verifportal, 1181, verifportal.Caster, false);
                            verifportal.IsInGameActive = false;
                        }
                    }
                    else if (GetActivableGamePortalsCount(verifportal.Caster.Team) >= 2 && verifportal.IsActive)
                    {
                        if (!verifportal.IsInGameActive && Fight.GetOneFighter(verifportal.CenterCell) == null)
                        {
                            ContextHandler.SendGameActionFightActivateGlyphTrapMessage(Fight.Clients, verifportal, 1181, verifportal.Caster, true);
                            verifportal.IsInGameActive = true;
                        }
                    }
                }
            }
            finally
            {

            }
        }

        public void RefreshTriggers(WorldClient client)
        {
            try
            {
                if (Fight.GetTriggers() == null) return;
                foreach (var trigger in Fight.GetTriggers())
                {
                    ContextHandler.SendGameActionFightMarkCellsMessage(client, trigger, client.Character.Fighter != null && trigger.DoesSeeTrigger(client.Character.Fighter));
                }
            }
            finally
            {

            }
        }

        public void RefreshClientPortals(WorldClient client)
        {
            try
            {
                if (Fight.GetTriggers().OfType<Portal>() == null) return;
                foreach (var verifportal in Fight.GetTriggers().OfType<Portal>())
                {
                    if (GetActivableGamePortalsCount(verifportal.Caster.Team) >= 2 && verifportal.IsActive && Fight.GetOneFighter(verifportal.CenterCell) == null)
                    {
                        ContextHandler.SendGameActionFightActivateGlyphTrapMessage(client, verifportal, 1181, verifportal.Caster, true);
                    }
                    else
                    {
                        ContextHandler.SendGameActionFightActivateGlyphTrapMessage(client, verifportal, 1181, verifportal.Caster, false);
                    }
                }
            }
            finally
            {

            }
        }

        public void RemoveFirstPortal(FightTeam team)
        {
            var portals = Fight.GetTriggers().OfType<Portal>();
            var portal = portals.FirstOrDefault(x => x.Caster.Team == team);
            portal?.Remove();
        }

        public void RemoveAllPortals(FightTeam team)
        {
            foreach (var portal in Fight.GetTriggers().OfType<Portal>().Where(x => x.Caster.Team == team))
            {
                portal.Remove();
            }
        }

        #endregion

        #region Get Portals Informations

        public List<Portal> GetAllPortalsByTeam(FightTeam team) => Fight.GetTriggers().OfType<Portal>().Where(x => x.Caster.Team == team).ToList();

        public bool CanPortal(FightTeam team) => GetAllPortalsByTeam(team) == null ? true : GetAllPortalsByTeam(team).Count < PortalsLimitPerTeam;

        public byte GetActivePortalsCount(FightTeam team) => GetAllPortalsByTeam(team) == null ? (byte)0 : (byte)GetAllPortalsByTeam(team).Where(x => x.IsActive).Count();

        public byte GetActivableGamePortalsCount(FightTeam team) => GetAllPortalsByTeam(team) == null ? (byte)0 : (byte)GetAllPortalsByTeam(team).Where(x => x.IsActive && Fight.GetOneFighter(x.CenterCell) == null).Count();

        public byte GetNoneNeutraledPortalsCount(FightTeam team) => GetAllPortalsByTeam(team) == null ? (byte)0 : (byte)GetAllPortalsByTeam(team).Where(portal => !portal.IsNeutral).Count();

        public List<MapPoint> GetMarksMapPoint(FightTeam team) => Enumerable.ToList<MapPoint>(Enumerable.Select<Portal, MapPoint>(Enumerable.Where<Portal>((IEnumerable<Portal>)this.GetAllPortalsByTeam(team), (Func<Portal, bool>)(portal => portal.IsActive && Fight.GetOneFighter(portal.CenterCell) == null)), (Func<Portal, MapPoint>)(portal => new MapPoint(portal.CenterCell))));

        public List<short> GetLinks(MapPoint startPoint, List<MapPoint> checkPoints)
        {
            if (checkPoints.Count == 1 && (int)startPoint.CellId == (int)checkPoints[0].CellId)
                return new List<short>()
        {
          startPoint.CellId
        };
            List<MapPoint> Source = new List<MapPoint>();
            for (int index = 0; index < checkPoints.Count; ++index)
            {
                if ((int)checkPoints[index].CellId != (int)startPoint.CellId)
                    Source.Add(checkPoints[index]);
            }
            List<short> list1 = new List<short>();
            MapPoint refMapPoint = startPoint;
            int num = Source.Count + 1;
            while (num > 0)
            {
                --num;
                list1.Add(refMapPoint.CellId);
                int Start = Source.IndexOf(refMapPoint);
                if (Start != -1)
                    Stump.Core.Extensions.StringExtensions.Splice<MapPoint>(Source, Start, 1);
                MapPoint closestPortal = this.GetClosestPortal(refMapPoint, (IEnumerable<MapPoint>)Source);
                if (closestPortal != null)
                    refMapPoint = closestPortal;
                else
                    break;
            }
            List<short> list2;
            if (list1.Count >= 2)
            {
                list2 = list1;
            }
            else
            {
                list2 = new List<short>();
                list2.Add(startPoint.CellId);
            }
            return list2;
        }

        private MapPoint GetClosestPortal(MapPoint refMapPoint, IEnumerable<MapPoint> portals)
        {
            List<MapPoint> list = new List<MapPoint>();
            int num1 = 63;
            foreach (MapPoint point in portals)
            {
                uint num2 = refMapPoint.ManhattanDistanceTo(point);
                if ((long)num2 < (long)num1)
                {
                    list.Clear();
                    list.Add(point);
                    num1 = (int)num2;
                }
                else if ((long)num2 == (long)num1)
                    list.Add(point);
            }
            switch (list.Count)
            {
                case 0:
                    return (MapPoint)null;
                case 1:
                    return list[0];
                default:
                    return this.GetBestNextPortal(refMapPoint, (IEnumerable<MapPoint>)list);
            }
        }

        private MapPoint GetBestNextPortal(MapPoint refCell, IEnumerable<MapPoint> closests)
        {
            Point nudge = new Point(refCell.X, refCell.Y + 1);
            IOrderedEnumerable<MapPoint> orderedEnumerable = Enumerable.OrderByDescending<MapPoint, double>(closests, (Func<MapPoint, double>)(point => this.GetPositiveOrientedAngle(new Point(refCell.X, refCell.Y), nudge, new Point(point.X, point.Y))));
            return GetBestPortalWhenRefIsNotInsideClosests(refCell, (IReadOnlyList<MapPoint>)Enumerable.ToList<MapPoint>((IEnumerable<MapPoint>)orderedEnumerable)) ?? Enumerable.ToList<MapPoint>((IEnumerable<MapPoint>)orderedEnumerable)[0];
        }

        private static MapPoint GetBestPortalWhenRefIsNotInsideClosests(MapPoint refCell, IReadOnlyList<MapPoint> sortedClosests)
        {
            if (sortedClosests.Count < 2)
                return (MapPoint)null;
            MapPoint mapPoint1 = sortedClosests[sortedClosests.Count - 1];
            foreach (MapPoint mapPoint2 in (IEnumerable<MapPoint>)sortedClosests)
            {
                switch (CompareAngles(new Point(refCell.X, refCell.Y), new Point(mapPoint1.X, mapPoint1.Y), new Point(mapPoint2.X, mapPoint2.Y)))
                {
                    case 1:
                        if (sortedClosests.Count <= 2)
                            return (MapPoint)null;
                        break;
                    case 3:
                        return mapPoint1;
                }
                mapPoint1 = mapPoint2;
            }
            return (MapPoint)null;
        }

        #endregion

        #region Calculate Functions

        private double GetPositiveOrientedAngle(Point refCell, Point cellA, Point cellB)
        {
            switch (CompareAngles(refCell, cellA, cellB))
            {
                case 0:
                    return 0.0;
                case 1:
                    return Math.PI;
                case 2:
                    return GetAngle(refCell, cellA, cellB);
                case 3:
                    return 2.0 * Math.PI - GetAngle(refCell, cellA, cellB);
                default:
                    return 0.0;
            }
        }

        private static double GetAngle(Point refCell, Point cellA, Point cellB)
        {
            double complexDistance1 = GetComplexDistance(cellA, cellB);
            double complexDistance2 = GetComplexDistance(refCell, cellA);
            double complexDistance3 = GetComplexDistance(refCell, cellB);
            return Math.Acos((complexDistance2 * complexDistance2 + complexDistance3 * complexDistance3 - complexDistance1 * complexDistance1) / (2.0 * complexDistance2 * complexDistance3));
        }

        private static double GetComplexDistance(Point cellA, Point cellB) => Math.Sqrt(Math.Pow((double)(cellA.X - cellB.X), 2.0) + Math.Pow((double)(cellA.Y - cellB.Y), 2.0));


        private static int CompareAngles(Point refer, Point start, Point end)
        {
            Point point1 = Vector(refer, start);
            Point point2 = Vector(refer, end);
            int num = point1.X * point2.Y - point1.Y * point2.X;
            if ((uint)num > 0U)
                return num > 0 ? 2 : 3;
            return point1.X >= 0 != point2.X >= 0 || point1.Y >= 0 != point2.Y >= 0 ? 1 : 0;
        }
        private static Point Vector(Point start, Point end) => new Point(end.X - start.X, end.Y - start.Y);

        #endregion

        #region Other

        public SpellCastInformations TeleportCast(SpellCastInformations cast, FightActor caster)
        {
            var portal = Fight.GetTriggers().FirstOrDefault(x => x is Portal && x.ContainsCell(cast.TargetedCell)) as Portal;
            if (portal != null)
            {
                if (GetActivableGamePortalsCount(portal.Caster.Team) < 2) return cast;

                if (portal.IsActive)
                {
                    var mpWithPortals = GetMarksMapPoint(portal.Caster.Team);
                    var links = GetLinks(new MapPoint(portal.CenterCell), mpWithPortals);
                    if (links.Count > 0)
                    {
                        try
                        {
                            var finalPortal = links[links.Count - 1];
                            var finalPortalCell = Map.GetCell(finalPortal);
                            var distX = caster.Position.Point.DistanceToCellX(new MapPoint(cast.TargetedCell));
                            var distY = caster.Position.Point.DistanceToCellY(new MapPoint(cast.TargetedCell));
                            var dirx = caster.Position.Point.OrientationToX(new MapPoint(cast.TargetedCell));
                            var diry = caster.Position.Point.OrientationToY(new MapPoint(cast.TargetedCell));
                            var point = new MapPoint(finalPortalCell).GetCellInDirection(dirx, (short)distX);
                            var point2 = point.GetCellInDirection(diry, (short)distY);
                            cast.CastDistance = new MapPoint(portal.CenterCell).ManhattanDistanceTo(new MapPoint(finalPortalCell));
                            cast.TargetedCell = Map.Cells[point2.CellId];
                            cast.CastCell = Map.Cells[finalPortal];
                            cast.IsCastedInPortal = true;
                            cast.PortalEntryCellId = portal.CenterCell.Id;
                        }
                        finally
                        {

                        }
                    }
                }
            }
            return cast;
        }

        public short[] PortalAnimation(SpellCastHandler castHandler)
        {
            try
            {
                if (castHandler.IsCastByPortal)
                {
                    var portalenter = Fight.GetTriggers().OfType<Portal>().Where(x => x.CenterCell.Id == castHandler.PortalEntryCellId).FirstOrDefault();
                    var portalout = Fight.GetTriggers().OfType<Portal>().Where(x => x.CenterCell.Id == castHandler.CastCell.Id).FirstOrDefault();

                    if (Fight.GetTriggers().OfType<Portal>().Where(x => x.Caster.Team == portalenter.Caster.Team && x.IsInGameActive).Count() == 3)
                    {
                        var portallast = Fight.GetTriggers().OfType<Portal>().Where(x => x != portalenter && x != portalout && x.IsInGameActive).FirstOrDefault();
                        return new short[] { portalenter.Id, portallast.Id, portalout.Id };
                    }
                    else if (Fight.GetTriggers().OfType<Portal>().Where(x => x.Caster.Team == portalenter.Caster.Team && x.IsInGameActive).Count() == 4)
                    {
                        var portalslast = Fight.GetTriggers().OfType<Portal>().Where(x => x != portalenter && x != portalout && x.IsInGameActive).OrderBy(x => new MapPoint(portalenter.CenterCell).ManhattanDistanceTo(x.CenterCell)).ToArray();
                        return new short[] { portalenter.Id, portalslast[0].Id, portalslast[1].Id, portalout.Id };
                    }
                    else
                    {
                        return new short[] { portalenter.Id, portalout.Id };
                    }
                }
                return new short[0];
            }
            catch
            {
                return new short[0];
            }
        }

        #endregion
    }
}
