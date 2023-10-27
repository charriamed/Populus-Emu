namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectRemovedFromBagMessage : ExchangeObjectMessage
    {
        public new const uint Id = 6010;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }

        public ExchangeObjectRemovedFromBagMessage(bool remote, uint objectUID)
        {
            this.Remote = remote;
            this.ObjectUID = objectUID;
        }

        public ExchangeObjectRemovedFromBagMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(ObjectUID);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectUID = reader.ReadVarUInt();
        }

    }
}
