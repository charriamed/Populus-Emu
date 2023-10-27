namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ZaapDestinationsMessage : TeleportDestinationsMessage
    {
        public new const uint Id = 6830;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double SpawnMapId { get; set; }

        public ZaapDestinationsMessage(sbyte type, TeleportDestination[] destinations, double spawnMapId)
        {
            this.Type = type;
            this.Destinations = destinations;
            this.SpawnMapId = spawnMapId;
        }

        public ZaapDestinationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(SpawnMapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SpawnMapId = reader.ReadDouble();
        }

    }
}
