namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CurrentMapMessage : Message
    {
        public const uint Id = 220;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MapId { get; set; }
        public string MapKey { get; set; }

        public CurrentMapMessage(double mapId, string mapKey)
        {
            this.MapId = mapId;
            this.MapKey = mapKey;
        }

        public CurrentMapMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
            writer.WriteUTF(MapKey);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
            MapKey = reader.ReadUTF();
        }

    }
}
