namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CompassUpdateMessage : Message
    {
        public const uint Id = 5591;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Type { get; set; }
        public MapCoordinates Coords { get; set; }

        public CompassUpdateMessage(sbyte type, MapCoordinates coords)
        {
            this.Type = type;
            this.Coords = coords;
        }

        public CompassUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Type);
            writer.WriteShort(Coords.TypeId);
            Coords.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Type = reader.ReadSByte();
            Coords = ProtocolTypeManager.GetInstance<MapCoordinates>(reader.ReadShort());
            Coords.Deserialize(reader);
        }

    }
}
