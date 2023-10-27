using Stump.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.DofusProtocol.D2oClasses.Tools.Bin
{
    public class WorldGraphEntry
    {
        public Dictionary<string, Vertex> Vertices { get; } = new Dictionary<string, Vertex>();

        public Dictionary<string, Edge> Edges { get; } = new Dictionary<string, Edge>();

        public Dictionary<string, List<Edge>> OutgoingEdges { get; } = new Dictionary<string, List<Edge>>();

        public string FilePath
        {
            get;
            protected set;
        }

        public WorldGraphEntry(string uri)
        {
            if (!File.Exists(uri))
                throw new Exception($"File {uri} doesn't exist");

            FilePath = uri;
            Initialize();
        }

        private void Initialize()
        {
            using (FastBigEndianReader reader = new FastBigEndianReader(File.ReadAllBytes(FilePath)))
            {
                var edgeCount = reader.ReadInt();

                for(var i = 0; i < edgeCount; i++)
                {
                    var from = AddVertex(reader.ReadDouble(), reader.ReadInt());
                    var to = AddVertex(reader.ReadDouble(), reader.ReadInt());
                    var edge = AddEdge(from, to);

                    var transitionCount = reader.ReadInt();
                    for(var j = 0; j < transitionCount; j++)
                    {
                        edge.AddTransition(reader.ReadByte(), reader.ReadByte(), reader.ReadInt(), reader.ReadUTFBytes((ushort)reader.ReadInt()), reader.ReadDouble(), reader.ReadInt(), reader.ReadDouble());
                    }
                }
            }

        }

        private Vertex AddVertex(double mapId, int zone)
        {
            Vertex vertex = null;
            var vertexUid = Vertex.GetVertexUID(mapId, zone);

            if (Vertices.ContainsKey(vertexUid))
                vertex = Vertices[vertexUid];

            if (vertex == null)
            {
                vertex = new Vertex(mapId, zone);
                Vertices.Add(vertexUid, vertex);
            }

            return vertex;
        }

        private Edge AddEdge(Vertex from, Vertex to)
        {
            var edge = GetEdge(from, to);
            if (edge != null)
                return edge;

            if (!DoesVertexExist(from) || !DoesVertexExist(to))
                return null;

            edge = new Edge(from, to);

            var internalId = GetInternalEdgeId(from, to);
            Edges.Add(internalId, edge);


            List<Edge> outgoing = null;
            if (OutgoingEdges.ContainsKey(from.UID))
                outgoing = OutgoingEdges[from.UID];

            if (outgoing == null)
            {
                outgoing = new List<Edge>();
                OutgoingEdges.Add(from.UID, outgoing);
            }

            outgoing.Add(edge);
            return edge;
        }

        private Edge GetEdge(Vertex from, Vertex to)
        {
            var internalId = GetInternalEdgeId(from, to);

            if (Edges.ContainsKey(internalId))
                return Edges[internalId];

            return null;
        }

        private static string GetInternalEdgeId(Vertex from, Vertex to)
        {
            return from.UID + "|" + to.UID;
        }

        public bool DoesVertexExist(Vertex v)
        {
            return this.Vertices.ContainsKey(v.UID);
        }

}
}
