namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TeleportRequestMessage : Message
    {
        public const uint Id = 5961;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte SourceType { get; set; }
        public sbyte DestinationType { get; set; }
        public double MapId { get; set; }

        public TeleportRequestMessage(sbyte sourceType, sbyte destinationType, double mapId)
        {
            this.SourceType = sourceType;
            this.DestinationType = destinationType;
            this.MapId = mapId;
        }

        public TeleportRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(SourceType);
            writer.WriteSByte(DestinationType);
            writer.WriteDouble(MapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SourceType = reader.ReadSByte();
            DestinationType = reader.ReadSByte();
            MapId = reader.ReadDouble();
        }

    }
}
