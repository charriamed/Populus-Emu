using Stump.Core.IO;
using System;
using System.IO;

namespace D2pReader.MapInformations.Elements
{
    public abstract class BasicElement
    {
        //private BigEndianReader EleReader = new BigEndianReader(File.ReadAllBytes("elements.ele"));
        public static uint ElementId
        {
            get;
            set;
        }
        public static BasicElement GetElementFromType(int type, BigEndianReader _reader, sbyte mapVersion)
        {
            switch (type)
            {
                case 2:
                    var graph = new GraphicalElement(_reader, mapVersion);
                    ElementId = graph.Identifier;
                        
                    return graph;
                case 33:
                    ElementId = 0;
                    return new SoundElement(_reader);
                default:
                    throw new ArgumentException("Invalid Element type " + type.ToString());
            }
        }
        public byte[] Serialize(int CellId, int ElementId)
        {
            return new byte[]
            {
                (byte)(CellId >> 8),
                (byte)(CellId & 255),
                (byte)(ElementId >> 24),
                (byte)(ElementId >> 16 & 255u),
                (byte)(ElementId >> 8 & 255u),
                (byte)(ElementId & 255u)
            };
        }
    }
}
