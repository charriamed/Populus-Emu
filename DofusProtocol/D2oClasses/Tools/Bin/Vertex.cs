using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.DofusProtocol.D2oClasses.Tools.Bin
{
    public class Vertex
    {
        public double MapId
        {
            get;
            set;
        }

        public int ZoneId
        {
            get;
            set;
        }

        public Vertex(double mapId, int zoneId)
        {
            MapId = mapId;
            ZoneId = zoneId;
        }

        public static string GetVertexUID(double mapId, int zoneId)
        {
            return mapId + "|" + zoneId;
        }

        public static double GetMapId(string vertexUid)
        {
            return double.Parse(vertexUid.Split('|')[0]);
        }

        public static int GetZoneId(string vertexUid)
        {
            return int.Parse(vertexUid.Split('|')[1]);
        }

        public string UID
        {
            get
            {
                return GetVertexUID(MapId, ZoneId);
            }
        }

        public override string ToString()
        {
            return "Vertex{_mapId=" + MapId + ",_zoneId" + ZoneId + "}";
        }
    }
}
