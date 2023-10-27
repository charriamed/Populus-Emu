using Stump.Core.Attributes;
using Stump.Core.Extensions;
using Stump.Core.Reflection;
using Stump.Core.Xml;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Database.World.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Game.Maps.Placements
{
    public class PlacementManager : Singleton<PlacementManager>
    {
        [Variable]
        public static string PatternsDir = "./patterns";

        [Variable]
        public static byte SearchDeep = 5;

        [Variable]
        public static bool SortByComplexity = true;

        private List<PlacementPattern> m_patterns = new List<PlacementPattern>();
        private List<PlacementPattern> m_relativePatterns = new List<PlacementPattern>();

        [Initialization(InitializationPass.Fourth)]
        public void Initialize()
        {
            List<PlacementPattern> temp_patterns = new List<PlacementPattern>();

            foreach (var file in Directory.EnumerateFiles(PatternsDir, "*.xml", SearchOption.AllDirectories))
            {
                var pattern = XmlUtils.Deserialize<PlacementPattern>(file);

                if (SortByComplexity)
                {
                    var calc = new PlacementComplexityCalculator(pattern.Blues.Concat(pattern.Reds).ToArray());
                    pattern.Complexity = calc.Compute();
                }

                temp_patterns.Add(pattern);
            }

            var fixPatternsComplx = temp_patterns.Where(x => !x.Relative).Select(x => x.Complexity).ToArray();
            m_patterns = temp_patterns.Where(x => !x.Relative).ShuffleWithProbabilities(fixPatternsComplx).ToList();

            fixPatternsComplx = temp_patterns.Where(x => x.Relative).Select(x => x.Complexity).ToArray();
            m_relativePatterns = temp_patterns.Where(x => x.Relative).ShuffleWithProbabilities(fixPatternsComplx).ToList();
        }

        public void GeneratePattern(Map map, bool forceUpdate)
        {
            var pattern = m_patterns.FirstOrDefault(x => x.TestPattern(map.Record) && x != map.Pattern);

            if (pattern != null)
            {
                map.Pattern = pattern;
                map.SetPlacements(pattern.Blues.Select(x => map.Cells[MapPoint.CoordToCellId(x.X, x.Y)]).ToArray(), pattern.Reds.Select(x => map.Cells[MapPoint.CoordToCellId(x.X, x.Y)]).ToArray());
            }
            else
            {
                foreach (var center in GetCellsCircle(map.Cells[300], map.Record, SearchDeep, 0))
                {
                    var centerPoint = MapPoint.CellIdToCoord((uint)center.Id);
                    pattern = m_relativePatterns.FirstOrDefault(x => x.TestPattern(centerPoint, map.Record) && x != map.Pattern);

                    if (pattern != null)
                    {
                        map.Pattern = pattern;
                        map.SetPlacements(pattern.Blues.Select(x => map.Cells[MapPoint.CoordToCellId(x.X + centerPoint.X, x.Y + centerPoint.Y)]).ToArray(), pattern.Reds.Select(x => map.Cells[MapPoint.CoordToCellId(x.X + centerPoint.X, x.Y + centerPoint.Y)]).ToArray());
                        break;
                    }
                }
            }
        }

        private static Cell[] GetCellsCircle(Cell centerCell, MapRecord map, int radius, int minradius)
        {
            var centerPoint = new MapPoint(centerCell);
            var result = new List<Cell>();

            if (radius == 0)
            {
                if (minradius == 0)
                    result.Add(centerCell);

                return result.ToArray();
            }

            int x = (int)(centerPoint.X - radius);
            int y = 0;
            int i = 0;
            int j = 1;
            while (x <= centerPoint.X + radius)
            {
                y = -i;

                while (y <= i)
                {
                    if (minradius == 0 || Math.Abs(centerPoint.X - x) + Math.Abs(y) >= minradius)
                        AddCellIfValid(x, y + centerPoint.Y, map, result);

                    y++;
                }

                if (i == radius)
                {
                    j = -j;
                }

                i = i + j;
                x++;
            }

            return result.ToArray();
        }

        private static void AddCellIfValid(int x, int y, MapRecord map, IList<Cell> container)
        {
            if (!MapPoint.IsInMap(x, y))
                return;

            container.Add(map.Cells[MapPoint.CoordToCellId(x, y)]);
        }
    }
}