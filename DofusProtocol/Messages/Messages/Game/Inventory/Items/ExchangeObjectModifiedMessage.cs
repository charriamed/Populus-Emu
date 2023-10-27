namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectModifiedMessage : ExchangeObjectMessage
    {
        public new const uint Id = 5519;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem @object { get; set; }

        public ExchangeObjectModifiedMessage(bool remote, ObjectItem @object)
        {
            this.Remote = remote;
            this.@object = @object;
        }

        public ExchangeObjectModifiedMessage() { }

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
