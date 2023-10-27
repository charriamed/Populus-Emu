namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ZaapRespawnUpdatedMessage : Message
    {
        public const uint Id = 6571;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MapId { get; set; }

        public ZaapRespawnUpdatedMessage(double mapId)
        {
            this.MapId = mapId;
        }

        public ZaapRespawnUpdatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
        }

    }
}
