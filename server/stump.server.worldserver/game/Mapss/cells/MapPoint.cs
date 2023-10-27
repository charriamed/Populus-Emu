using System;
using System.Collections.Generic;
using System.Drawing;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Maps.Cells.Shapes.Set;

namespace Stump.Server.WorldServer.Game.Maps.Cells
{
    /// <summary>
    /// Represents a point on a 2 dimensional plan from a map cell
    /// </summary>
    public class MapPoint
    {
        public const uint MapWidth = 14;
        public const uint MapHeight = 20;

        public const uint MapSize = MapWidth * MapHeight * 2;

        static readonly Point VectorRight = new Point(1, 1);
        static readonly Point VectorDownRight = new Point(1, 0);
        static readonly Point VectorDown = new Point(1, -1);
        static readonly Point VectorDownLeft = new Point(0, -1);
        static readonly Point VectorLeft = new Point(-1, -1);
        static readonly Point VectorUpLeft = new Point(-1, 0);
        static readonly Point VectorUp = new Point(-1, 1);
        static readonly Point VectorUpRight = new Point(0, 1);

        static bool m_initialized;
        static readonly MapPoint[] OrthogonalGridReference = new MapPoint[MapSize];


        short m_cellId;
        int m_x;
        int m_y;

        public MapPoint(short cellId)
        {
            m_cellId = cellId;

            SetFromCellId();
        }

        public MapPoint(Cell cell)
        {
            m_cellId = cell.Id;

            SetFromCellId();
        }

        public MapPoint(int x, int y)
        {
            m_x = x;
            m_y = y;

            SetFromCoords();
        }

        public MapPoint(Point point)
        {
            m_x = point.X;
            m_y = point.Y;

            SetFromCoords();
        }

        public short CellId
        {
            get { return m_cellId; }
            private set
            {
                m_cellId = value;
                SetFromCellId();
            }
        }

        public int X
        {
            get { return m_x; }
            private set
            {
                m_x = value;
                SetFromCoords();
            }
        }

        public int Y
        {
            get { return m_y; }
            private set
            {
                m_y = value;
                SetFromCoords();
            }
        }

        private void SetFromCoords()
        {
            if (!m_initialized)
                InitializeStaticGrid();

            m_cellId = (short)((m_x - m_y) * MapWidth + m_y + (m_x - m_y) / 2);
        }

        private void SetFromCellId()
        {
            if (!m_initialized)
                InitializeStaticGrid();

            if (m_cellId < 0 || m_cellId > MapSize)
                throw new IndexOutOfRangeException("Cell identifier out of bounds (" + m_cellId + ").");

            var point = OrthogonalGridReference[m_cellId];
            m_x = point.X;
            m_y = point.Y;
        }

        public uint EuclideanDistanceTo(MapPoint point)
        {
            return (uint)Math.Sqrt((point.X - m_x) * (point.X - m_x) + (point.Y - m_y) * (point.Y - m_y));
        }

        public uint ManhattanDistanceTo(MapPoint point)
        {
            return (uint)(Math.Abs(m_x - point.X) + Math.Abs(m_y - point.Y));
        }

        public uint SquareDistanceTo(MapPoint point)
        {
            return (uint)Math.Max(Math.Abs(m_x - point.X), Math.Abs(m_y - point.Y));
        }

        public bool IsAdjacentTo(MapPoint point)
        {
            return ManhattanDistanceTo(point) == 1;
        }

        public DirectionsEnum OrientationToAdjacent(MapPoint point)
        {
            var vector = new Point
            {
                X = point.X > m_x ? (1) : (point.X < m_x ? (-1) : (0)),
                Y = point.Y > m_y ? (1) : (point.Y < m_y ? (-1) : (0))
            };

            if (vector == VectorRight)
            {
                return DirectionsEnum.DIRECTION_EAST;
            }
            if (vector == VectorDownRight)
            {
                return DirectionsEnum.DIRECTION_SOUTH_EAST;
            }
            if (vector == VectorDown)
            {
                return DirectionsEnum.DIRECTION_SOUTH;
            }
            if (vector == VectorDownLeft)
            {
                return DirectionsEnum.DIRECTION_SOUTH_WEST;
            }
            if (vector == VectorLeft)
            {
                return DirectionsEnum.DIRECTION_WEST;
            }
            if (vector == VectorUpLeft)
            {
                return DirectionsEnum.DIRECTION_NORTH_WEST;
            }
            if (vector == VectorUp)
            {
                return DirectionsEnum.DIRECTION_NORTH;
            }
            if (vector == VectorUpRight)
            {
                return DirectionsEnum.DIRECTION_NORTH_EAST;
            }

            return DirectionsEnum.DIRECTION_EAST;
        }

        public DirectionsEnum OrientationTo(MapPoint point, Boolean diagonal = true)
        {
            int dx = point.X - m_x;
            int dy = m_y - point.Y;

            double distance = Math.Sqrt(dx * dx + dy * dy);
            double angleInRadians = Math.Acos(dx / distance);

            double angleInDegrees = angleInRadians * 180 / Math.PI;
            double transformedAngle = angleInDegrees * (point.Y > m_y ? (-1) : (1));

            double orientation = !diagonal ? Math.Round(transformedAngle / 90) * 2 + 1 : Math.Round(transformedAngle / 45) + 1;

            if (orientation < 0)
            {
                orientation = orientation + 8;
            }

            return (DirectionsEnum)(uint)orientation;
        }

        public IEnumerable<MapPoint> GetAllCellsInRectangle(MapPoint oppositeCell, bool skipStartAndEndCells = true, Func<MapPoint, bool> predicate = null)
        {
            int x1 = Math.Min(oppositeCell.X, X),
                y1 = Math.Min(oppositeCell.Y, Y),
                x2 = Math.Max(oppositeCell.X, X),
                y2 = Math.Max(oppositeCell.Y, Y);
            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                    if (!skipStartAndEndCells || (!(x == X && y == Y) && !(x == oppositeCell.X && y == oppositeCell.Y)))
                    {
                        var cell = GetPoint(x, y);
                        if (cell != null && (predicate == null || predicate(cell))) yield return cell;
                    }
        }
        public uint DistanceToCellX(MapPoint point)
        {
            return (uint)(Math.Abs(m_x - point.X));
        }
        public uint DistanceToCellY(MapPoint point)
        {
            return (uint)(Math.Abs(m_y - point.Y));
        }
        public DirectionsEnum OrientationToX(MapPoint point, Boolean diagonal = false)
        {
            int dx = point.X - m_x;

            double distance = Math.Sqrt(dx * dx);
            double angleInRadians = Math.Acos(dx / distance);

            double angleInDegrees = angleInRadians * 180 / Math.PI;
            double transformedAngle = angleInDegrees;

            double orientation = !diagonal ? Math.Round(transformedAngle / 90) * 2 + 1 : Math.Round(transformedAngle / 45) + 1;

            if (orientation < 0)
            {
                orientation = orientation + 8;
            }

            return (DirectionsEnum)(uint)orientation;
        }
        public DirectionsEnum OrientationToY(MapPoint point, Boolean diagonal = false)
        {
            int dx = 0;
            int dy = m_y - point.Y;

            double distance = Math.Sqrt(dx * dx + dy * dy);
            double angleInRadians = Math.Acos(dx / distance);

            double angleInDegrees = angleInRadians * 180 / Math.PI;
            double transformedAngle = angleInDegrees * (point.Y > m_y ? (-1) : (1));

            double orientation = !diagonal ? Math.Round(transformedAngle / 90) * 2 + 1 : Math.Round(transformedAngle / 45) + 1;

            if (orientation < 0)
            {
                orientation = orientation + 8;
            }
            return (DirectionsEnum)(uint)orientation;
        }

        public bool IsOnSameLine(MapPoint point)
        {
            return point.X == X || point.Y == Y;
        }

        public bool IsOnSameDiagonal(MapPoint point)
        {
            return point.X + point.Y == X + Y ||
                point.X - point.Y == X - Y;
        }

        /// <summary>
        /// Returns true whenever this point is between points A and B
        /// </summary>
        /// <returns></returns>
        public bool IsBetween(MapPoint A, MapPoint B)
        {
            // check colinearity
            if ((X - A.X) * (B.Y - Y) - (Y - A.Y) * (B.X - X) != 0)
                return false;

            var min = new Point(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y));
            var max = new Point(Math.Max(A.X, B.X), Math.Max(A.Y, B.Y));
            // check position
            return (X >= min.X && X <= max.X && Y >= min.Y && Y <= max.Y);
        }

        public MapPoint[] GetCellsOnLineBetween(MapPoint destination)
        {
            var result = new List<MapPoint>();
            var direction = OrientationTo(destination);
            var current = this;
            for (int i = 0; i < MapHeight * MapWidth / 2; i++)
            {
                current = current.GetCellInDirection(direction, 1);

                if (current == null)
                    break;

                if (current.CellId == destination.CellId)
                    break;

                result.Add(current);
            }

            return result.ToArray();
        }

        public IEnumerable<MapPoint> GetCellsInLine(MapPoint destination) => GetCellsInLine(this, destination);

        public static IEnumerable<MapPoint> GetCellsInLine(MapPoint source, MapPoint destination)
        {
            // http://playtechs.blogspot.fr/2007/03/raytracing-on-grid.html

            var dx = Math.Abs(destination.X - source.X);
            var dy = Math.Abs(destination.Y - source.Y);
            var x = source.X;
            var y = source.Y;
            var n = 1 + dx + dy;
            var vectorX = (destination.X > source.X) ? 1 : -1;
            var vectorY = (destination.Y > source.Y) ? 1 : -1;
            var error = dx - dy;
            dx *= 2;
            dy *= 2;

            for (; n > 0; --n)
            {
                yield return GetPoint(x, y);

                if (error > 0)
                {
                    x += vectorX;
                    error -= dy;
                }
                else if (error == 0)
                {
                    x += vectorX;
                    y += vectorY;
                    n--;
                }
                else
                {
                    y += vectorY;
                    error += dx;
                }
            }
        }

        public bool IsOnMapBorder()
        {
            return
                new LineSet(new MapPoint(27), new MapPoint(559)).BelongToSet(this) ||
                new LineSet(new MapPoint(546), new MapPoint(559)).BelongToSet(this) ||
                new LineSet(new MapPoint(0), new MapPoint(13)).BelongToSet(this) ||
                new LineSet(new MapPoint(0), new MapPoint(532)).BelongToSet(this);
        }

        public static IEnumerable<MapPoint> GetBorderCells(MapNeighbour mapNeighbour)
        {
            switch (mapNeighbour)
            {
                case MapNeighbour.Top:
                    return GetCellsInLine(new MapPoint(546), new MapPoint(559));
                case MapNeighbour.Left:
                    return GetCellsInLine(new MapPoint(27), new MapPoint(559));
                case MapNeighbour.Bottom:
                    return GetCellsInLine(new MapPoint(0), new MapPoint(13));
                case MapNeighbour.Right:
                    return GetCellsInLine(new MapPoint(0), new MapPoint(532));
            }

            return new MapPoint[0];
        }

        public MapPoint GetCellInDirection(DirectionsEnum direction, int step)
        {
            MapPoint mapPoint = null;
            switch (direction)
            {
                case DirectionsEnum.DIRECTION_EAST:
                    {
                        mapPoint = GetPoint(m_x + step, m_y + step);
                        break;
                    }
                case DirectionsEnum.DIRECTION_SOUTH_EAST:
                    {
                        mapPoint = GetPoint(m_x + step, m_y);
                        break;
                    }
                case DirectionsEnum.DIRECTION_SOUTH:
                    {
                        mapPoint = GetPoint(m_x + step, m_y - step);
                        break;
                    }
                case DirectionsEnum.DIRECTION_SOUTH_WEST:
                    {
                        mapPoint = GetPoint(m_x, m_y - step);
                        break;
                    }
                case DirectionsEnum.DIRECTION_WEST:
                    {
                        mapPoint = GetPoint(m_x - step, m_y - step);
                        break;
                    }
                case DirectionsEnum.DIRECTION_NORTH_WEST:
                    {
                        mapPoint = GetPoint(m_x - step, m_y);
                        break;
                    }
                case DirectionsEnum.DIRECTION_NORTH:
                    {
                        mapPoint = GetPoint(m_x - step, m_y + step);
                        break;
                    }
                case DirectionsEnum.DIRECTION_NORTH_EAST:
                    {
                        mapPoint = GetPoint(m_x, m_y + step);
                        break;
                    }
            }

            if (mapPoint != null)
                return IsInMap(mapPoint.X, mapPoint.Y) ? mapPoint : null;

            return null;
        }

        public static MapPoint FromCellId(Cell cell)
        {
            MapPoint point = new MapPoint(cell.Id);
            point.SetFromCellId();

            return point;
        }

        public MapPoint GetNearestCellInDirection(DirectionsEnum direction)
        {
            return GetCellInDirection(direction, 1);
        }

        public IEnumerable<MapPoint> GetAdjacentCells(bool diagonal = false)
        {
            return GetAdjacentCells(x => true, diagonal);
        }

        public IEnumerable<MapPoint> GetAdjacentCells(Func<short, bool> predicate, bool diagonal = false)
        {
            var southEast = new MapPoint(m_x + 1, m_y);
            if (IsInMap(southEast.X, southEast.Y) && predicate(southEast.CellId))
                yield return southEast;

            var southWest = new MapPoint(m_x, m_y - 1);
            if (IsInMap(southWest.X, southWest.Y) && predicate(southWest.CellId))
                yield return southWest;

            var northEast = new MapPoint(m_x, m_y + 1);
            if (IsInMap(northEast.X, northEast.Y) && predicate(northEast.CellId))
                yield return northEast;

            var northWest = new MapPoint(m_x - 1, m_y);
            if (IsInMap(northWest.X, northWest.Y) && predicate(northWest.CellId))
                yield return northWest;

            if (diagonal)
            {
                var south = new MapPoint(m_x + 1, m_y - 1);
                if (IsInMap(south.X, south.Y) && predicate(south.CellId))
                    yield return south;

                var west = new MapPoint(m_x - 1, m_y - 1);
                if (IsInMap(west.X, west.Y) && predicate(west.CellId))
                    yield return west;

                var north = new MapPoint(m_x - 1, m_y + 1);
                if (IsInMap(north.X, north.Y) && predicate(north.CellId))
                    yield return northEast;

                var east = new MapPoint(m_x + 1, m_y + 1);
                if (IsInMap(east.X, east.Y) && predicate(east.CellId))
                    yield return northWest;

            }
        }

        public bool IsInMap()
        {
            return IsInMap(m_x, m_y);
        }

        public static bool IsInMap(int x, int y)
        {
            return x + y >= 0 && x - y >= 0 && x - y < MapHeight * 2 && x + y < MapWidth * 2;
        }

        public static uint CoordToCellId(int x, int y)
        {
            if (!m_initialized)
                InitializeStaticGrid();

            return (uint)((x - y) * MapWidth + y + (x - y) / 2);
        }

        public static Point CellIdToCoord(uint cellId)
        {
            if (!m_initialized)
                InitializeStaticGrid();

            var point = GetPoint((short)cellId);

            return new Point(point.X, point.Y);
        }

        /// <summary>
        /// Initialize a static 2D plan that is used as reference to convert a cell to a (X,Y) point
        /// </summary>
        private static void InitializeStaticGrid()
        {
            m_initialized = true;

            var posX = 0;
            var posY = 0;
            var cellCount = 0;

            for (var x = 0; x < MapHeight; x++)
            {
                for (var y = 0; y < MapWidth; y++)
                    OrthogonalGridReference[cellCount++] = new MapPoint(posX + y, posY + y);

                posX++;

                for (var y = 0; y < MapWidth; y++)
                    OrthogonalGridReference[cellCount++] = new MapPoint(posX + y, posY + y);

                posY--;
            }

        }

        public static MapPoint GetPoint(int x, int y)
        {
            return new MapPoint(x, y);
        }

        public static MapPoint GetPoint(short cell)
        {
            return OrthogonalGridReference[cell];
        }

        public static MapPoint GetPoint(Cell cell)
        {
            return GetPoint(cell.Id);
        }

        public static MapPoint[] GetAllPoints()
        {
            if (!m_initialized)
                InitializeStaticGrid();

            return OrthogonalGridReference;
        }

        public static implicit operator MapPoint(Cell cell)
        {
            return new MapPoint(cell);
        }

        public override string ToString()
        {
            return "[MapPoint(x:" + m_x + ", y:" + m_y + ", id:" + m_cellId + ")]";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MapPoint)) return false;
            return Equals((MapPoint)obj);
        }

        public bool Equals(MapPoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.m_cellId == m_cellId && other.m_x == m_x && other.m_y == m_y;
        }

        public uint DistanceToCell(MapPoint point)
        {
            return (uint)(Math.Abs(m_x - point.X) + Math.Abs(m_y - point.Y));
        }

        public uint DistanceTo(MapPoint point)
        {
            return (uint)Math.Sqrt((point.X - m_x) * (point.X - m_x) + (point.Y - m_y) * (point.Y - m_y));
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int result = m_cellId;
                result = (result * 397) ^ m_x;
                result = (result * 397) ^ m_y;
                return result;
            }

            
        }
    }
}