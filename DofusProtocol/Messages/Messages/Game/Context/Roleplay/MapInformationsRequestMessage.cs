namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapInformationsRequestMessage : Message
    {
        public const uint Id = 225;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MapId { get; set; }

        public MapInformationsRequestMessage(double mapId)
        {
            this.MapId = mapId;
        }

        public MapInformationsRequestMessage() { }

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
