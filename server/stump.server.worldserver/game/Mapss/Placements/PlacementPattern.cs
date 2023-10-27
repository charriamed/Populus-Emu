using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Database.World.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Stump.Server.WorldServer.Game.Maps.Placements
{
    [Serializable]
    public class PlacementPattern
    {
        public bool Relative
        {
            get;
            set;
        }

        public Point[] Blues
        {
            get;
            set;
        }

        public Point[] Reds
        {
            get;
            set;
        }

        public Point Center
        {
            get;
            set;
        }

        [XmlIgnore]
        public int Complexity
        {
            get;
            set;
        }

        public bool TestPattern(MapRecord map)
        {
            try
            {
                bool bluesOk;
                bool redsOk;
                if (Relative)
                {
                    bluesOk = Blues.All(entry => GetCell(map, entry.X + Center.X, entry.Y + Center.Y).Walkable
                                                    && !GetCell(map, entry.X + Center.X, entry.Y + Center.Y).NonWalkableDuringFight);
                    redsOk = Reds.All(entry => GetCell(map, entry.X + Center.X, entry.Y + Center.Y).Walkable
                                                    && !GetCell(map, entry.X + Center.X, entry.Y + Center.Y).NonWalkableDuringFight);
                }
                else
                {
                    bluesOk = Blues.All(entry => GetCell(map, entry.X, entry.Y).Walkable
                                                    && !GetCell(map, entry.X, entry.Y).NonWalkableDuringFight);
                    redsOk = Reds.All(entry => GetCell(map, entry.X, entry.Y).Walkable
                                                    && !GetCell(map, entry.X, entry.Y).NonWalkableDuringFight);
                }

                return bluesOk && redsOk;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private Cell GetCell(MapRecord map, int x, int y)
        {
            var point = new MapPoint(x, y);
            return map.Cells[point.CellId];
        }

        public bool TestPattern(Point center, MapRecord map)
        {
            try
            {
                bool bluesOk;
                bool redsOk;
                if (Relative)
                {
                    bluesOk = Blues.All(entry => GetCell(map, entry.X + center.X, entry.Y + center.Y).Walkable
                                                    && !GetCell(map, entry.X + center.X, entry.Y + center.Y).NonWalkableDuringFight);
                    redsOk = Reds.All(entry => GetCell(map, entry.X + center.X, entry.Y + center.Y).Walkable
                                                    && !GetCell(map, entry.X + center.X, entry.Y + center.Y).NonWalkableDuringFight);
                }
                else
                {
                    bluesOk = Blues.All(entry => GetCell(map, entry.X, entry.Y).Walkable
                                                    && !GetCell(map, entry.X, entry.Y).NonWalkableDuringFight);
                    redsOk = Reds.All(entry => GetCell(map, entry.X, entry.Y).Walkable
                                                    && !GetCell(map, entry.X, entry.Y).NonWalkableDuringFight);
                }

                return bluesOk && redsOk;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
