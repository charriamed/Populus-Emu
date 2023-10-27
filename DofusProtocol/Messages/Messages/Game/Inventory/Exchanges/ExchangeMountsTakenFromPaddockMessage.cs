namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMountsTakenFromPaddockMessage : Message
    {
        public const uint Id = 6554;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public string Ownername { get; set; }

        public ExchangeMountsTakenFromPaddockMessage(string name, short worldX, short worldY, string ownername)
        {
            this.Name = name;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.Ownername = ownername;
        }

        public ExchangeMountsTakenFromPaddockMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteUTF(Ownername);
        }

        public override void Deserialize(IDataReader reader)
        {
            Name = reader.ReadUTF();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            Ownername = reader.ReadUTF();
        }

    }
}
