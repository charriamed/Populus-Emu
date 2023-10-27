namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectPutInBagMessage : ExchangeObjectMessage
    {
        public new const uint Id = 6009;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem @object { get; set; }

        public ExchangeObjectPutInBagMessage(bool remote, ObjectItem @object)
        {
            this.Remote = remote;
            this.@object = @object;
        }

        public ExchangeObjectPutInBagMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            @object.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            @object = new ObjectItem();
            @object.Deserialize(reader);
        }

    }
}
