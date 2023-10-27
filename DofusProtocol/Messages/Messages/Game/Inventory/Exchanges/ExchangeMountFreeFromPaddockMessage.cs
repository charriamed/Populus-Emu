namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMountFreeFromPaddockMessage : Message
    {
        public const uint Id = 6055;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public string Liberator { get; set; }

        public ExchangeMountFreeFromPaddockMessage(string name, short worldX, short worldY, string liberator)
        {
            this.Name = name;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.Liberator = liberator;
        }

        public ExchangeMountFreeFromPaddockMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteUTF(Liberator);
        }

        public override void Deserialize(IDataReader reader)
        {
            Name = reader.ReadUTF();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            Liberator = reader.ReadUTF();
        }

    }
}
