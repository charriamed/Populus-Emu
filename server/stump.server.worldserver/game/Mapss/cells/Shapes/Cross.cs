using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.World;

namespace Stump.Server.WorldServer.Game.Maps.Cells.Shapes
{
    public class Cross : IShape
    {
        public Cross(byte minRadius, byte radius)
        {
            MinRadius = minRadius;
            Radius = radius;

            DisabledDirections = new List<DirectionsEnum>();
        }

        public bool Diagonal
        {
            get;
            set;
        }

        public List<DirectionsEnum> DisabledDirections
        {
            get;
            set;
        }

        public bool OnlyPerpendicular
        {
            get;
            set;
        }

        public bool AllDirections
        {
            get;
            set;
        }

        #region IShape Members

        public uint Surface
        {
            get
            {
                return (uint)Radius * 4 + 1;
            }
        }

        public byte MinRadius
        {
            get;
            set;
        }

        public DirectionsEnum Direction
        {
            get;
            set;
        }

        public byte Radius
        {
            get;
            set;
        }

        public Cell[] GetCells(Cell centerCell, Map map)
        {
            var result = new List<Cell>();

            if (MinRadius == 0)
                result.Add(centerCell);

            List<DirectionsEnum> disabledDirections = DisabledDirections.ToList();
            if (OnlyPerpendicular)
            {
                switch (Direction)
                {
                    case DirectionsEnum.DIRECTION_SOUTH_EAST:
                    case DirectionsEnum.DIRECTION_NORTH_WEST:
                        {
                            disabledDirections.Add(DirectionsEnum.DIRECTION_SOUTH_EAST);
                            disabledDirections.Add(DirectionsEnum.DIRECTION_NORTH_WEST);
                            break;
                        }
                    case DirectionsEnum.DIRECTION_NORTH_EAST:
                    case DirectionsEnum.DIRECTION_SOUTH_WEST:
                        {
                            disabledDirections.Add(DirectionsEnum.DIRECTION_NORTH_EAST);
                            disabledDirections.Add(DirectionsEnum.DIRECTION_SOUTH_WEST);
                            break;
                        }
                    case DirectionsEnum.DIRECTION_SOUTH:
                    case DirectionsEnum.DIRECTION_NORTH:
                        {
                            disabledDirections.Add(DirectionsEnum.DIRECTION_SOUTH);
                            disabledDirections.Add(DirectionsEnum.DIRECTION_NORTH);
                            break;
                        }
                    case DirectionsEnum.DIRECTION_EAST:
                    case DirectionsEnum.DIRECTION_WEST:
                        {
                            disabledDirections.Add(DirectionsEnum.DIRECTION_EAST);
                            disabledDirections.Add(DirectionsEnum.DIRECTION_WEST);
                            break;
                        }
                }
            }

            var centerPoint = new MapPoint(centerCell);

            for (var i = (int) Radius; i > 0; i--)
            {
                if (i < MinRadius)
                    continue;

                if (!Diagonal)
                {
                    if (!disabledDirections.Contains(DirectionsEnum.DIRECTION_SOUTH_EAST))
                        AddCellIfValid(centerPoint.X + i, centerPoint.Y, map, result);
                    if (!disabledDirections.Contains(DirectionsEnum.DIRECTION_NORTH_WEST))
                        AddCellIfValid(centerPoint.X - i, centerPoint.Y, map, result);
                    if (!disabledDirections.Contains(DirectionsEnum.DIRECTION_NORTH_EAST))
                        AddCellIfValid(centerPoint.X, centerPoint.Y + i, map, result);
                    if (!disabledDirections.Contains(DirectionsEnum.DIRECTION_SOUTH_WEST))
                        AddCellIfValid(centerPoint.X, centerPoint.Y - i, map, result);
                }

                if (Diagonal || AllDirections)
                {
                    if (!disabledDirections.Contains(DirectionsEnum.DIRECTION_SOUTH))
                        AddCellIfValid(centerPoint.X + i, centerPoint.Y - i, map, result);
                    if (!disabledDirections.Contains(DirectionsEnum.DIRECTION_NORTH))
                        AddCellIfValid(centerPoint.X - i, centerPoint.Y + i, map, result);
                    if (!disabledDirections.Contains(DirectionsEnum.DIRECTION_EAST))
                        AddCellIfValid(centerPoint.X + i, centerPoint.Y + i, map, result);
                    if (!disabledDirections.Contains(DirectionsEnum.DIRECTION_WEST))
                        AddCellIfValid(centerPoint.X - i, centerPoint.Y - i, map, result);
                }
            }

            return result.ToArray();
        }
        private static void AddCellIfValid(int x, int y, Map map, IList<Cell> container)
        {
            if (!MapPoint.IsInMap(x, y))
                return;

            container.Add(map.Cells[MapPoint.CoordToCellId(x, y)]);
        }
        #endregion
    }
}