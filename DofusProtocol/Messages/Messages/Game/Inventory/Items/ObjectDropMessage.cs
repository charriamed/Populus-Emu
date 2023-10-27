namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectDropMessage : Message
    {
        public const uint Id = 3005;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }
        public uint Quantity { get; set; }

        public ObjectDropMessage(uint objectUID, uint quantity)
        {
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
        }

        public ObjectDropMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
            Quantity = reader.ReadVarUInt();
        }

    }
}
