namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectUseMultipleMessage : ObjectUseMessage
    {
        public new const uint Id = 6234;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Quantity { get; set; }

        public ObjectUseMultipleMessage(uint objectUID, uint quantity)
        {
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
        }

        public ObjectUseMultipleMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Quantity = reader.ReadVarUInt();
        }

    }
}
