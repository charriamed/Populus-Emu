namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectQuantityMessage : Message
    {
        public const uint Id = 3023;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }
        public uint Quantity { get; set; }
        public sbyte Origin { get; set; }

        public ObjectQuantityMessage(uint objectUID, uint quantity, sbyte origin)
        {
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
            this.Origin = origin;
        }

        public ObjectQuantityMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
            writer.WriteVarUInt(Quantity);
            writer.WriteSByte(Origin);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
            Quantity = reader.ReadVarUInt();
            Origin = reader.ReadSByte();
        }

    }
}
