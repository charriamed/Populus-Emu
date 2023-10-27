﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Game.Maps.Placements
{
    public class PlacementComplexityCalculator
    {
        private class PointsGroup
        {
            public PointsGroup(Point[] points, Point center)
            {
                Points = points;
                Center = center;
            }

            public readonly Point[] Points;
            public readonly Point Center;
        }

        private readonly Point[] m_points;

        public PlacementComplexityCalculator(Point[] points)
        {
            m_points = points;
        }

        public int Compute()
        {
            var groups = GetPointsGroups();
            if (groups.Length == 0)
                return 0;

            var distanceSum = 0d;

            var exclusions = new List<PointsGroup>();
            foreach (var @group in groups)
            {
                distanceSum += groups.Where(entry => !exclusions.Contains(entry)).Sum(entry => DistanceTo(entry.Center, @group.Center));

                exclusions.Add(@group);
            }

            var distanceAverage = distanceSum / groups.Length;
            var counts = m_points.Length;

            return (int)(counts * distanceAverage + groups.Length * groups.Average(entry => entry.Points.Length));
        }

        private PointsGroup[] GetPointsGroups()
        {
            var result = new List<PointsGroup>();
            var exclusions = new List<Point>();

            foreach (var point in m_points)
            {
                if (exclusions.Contains(point))
                    continue;

                var adjacents = FindAllAdjacentsPoints(point, new List<Point>(new[] { point }));
                adjacents.Add(point);

                var group = adjacents.ToArray();

                if (@group.Length <= 0)
                    continue;

                exclusions.Add(point);
                exclusions.AddRange(adjacents);
                result.Add(new PointsGroup(@group, GetCenter(@group)));
            }

            return result.ToArray();
        }

        private List<Point> FindAllAdjacentsPoints(Point point, ICollection<Point> exclusions)
        {
            var result = new List<Point>();

            foreach (var adjacentPoint in GetAdjacentPoints(point).Where(entry => m_points.Contains(entry)).Where(adjacentPoint => !exclusions.Contains(adjacentPoint)))
            {
                exclusions.Add(adjacentPoint);
                result.Add(adjacentPoint);

                result.AddRange(FindAllAdjacentsPoints(adjacentPoint, exclusions));
            }

            return result;
        }

        private static Point GetCenter(ICollection<Point> points)
        {
            return new Point(points.Sum(entry => entry.X) / points.Count, points.Sum(entry => entry.Y) / points.Count);
        }

        private static double DistanceTo(Point ptA, Point ptB)
        {
            return Math.Sqrt((ptB.X - ptA.X) * (ptB.X - ptA.X) + (ptB.Y - ptA.Y) * (ptB.Y - ptA.Y));
        }

        private static IEnumerable<Point> GetAdjacentPoints(Point point)
        {
            return new[] {
                              point + new Size(1, 0),
                              point + new Size(0, 1),
                              point + new Size(-1, 0),
                              point + new Size(0, -1),
                              point + new Size(1, 1),
                              point + new Size(-1, 1),
                              point + new Size(1, -1),
                              point + new Size(-1, -1),
                          };
        }
    }
}
