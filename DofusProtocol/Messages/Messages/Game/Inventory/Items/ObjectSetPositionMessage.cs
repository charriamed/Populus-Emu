namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectSetPositionMessage : Message
    {
        public const uint Id = 3021;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }
        public short Position { get; set; }
        public uint Quantity { get; set; }

        public ObjectSetPositionMessage(uint objectUID, short position, uint quantity)
        {
            this.ObjectUID = objectUID;
            this.Position = position;
            this.Quantity = quantity;
        }

        public ObjectSetPositionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
            writer.WriteShort(Position);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
            Position = reader.ReadShort();
            Quantity = reader.ReadVarUInt();
        }

    }
}
