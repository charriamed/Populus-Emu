namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMountSterilizeFromPaddockMessage : Message
    {
        public const uint Id = 6056;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public string Sterilizator { get; set; }

        public ExchangeMountSterilizeFromPaddockMessage(string name, short worldX, short worldY, string sterilizator)
        {
            this.Name = name;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.Sterilizator = sterilizator;
        }

        public ExchangeMountSterilizeFromPaddockMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteUTF(Sterilizator);
        }

        public override void Deserialize(IDataReader reader)
        {
            Name = reader.ReadUTF();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            Sterilizator = reader.ReadUTF();
        }

    }
}
