namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectsModifiedMessage : ExchangeObjectMessage
    {
        public new const uint Id = 6533;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem[] @object { get; set; }

        public ExchangeObjectsModifiedMessage(bool remote, ObjectItem[] @object)
        {
            this.Remote = remote;
            this.@object = @object;
        }

        public ExchangeObjectsModifiedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)@object.Count());
            for (var @objectIndex = 0; @objectIndex < @object.Count(); @objectIndex++)
            {
                var objectToSend = @object[@objectIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var @objectCount = reader.ReadUShort();
            @object = new ObjectItem[@objectCount];
            for (var @objectIndex = 0; @objectIndex < @objectCount; @objectIndex++)
            {
                var objectToAdd = new ObjectItem();
                objectToAdd.Deserialize(reader);
                @object[@objectIndex] = objectToAdd;
            }
        }

    }
}
