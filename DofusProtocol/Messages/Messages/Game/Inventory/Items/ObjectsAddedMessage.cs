namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectsAddedMessage : Message
    {
        public const uint Id = 6033;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem[] @object { get; set; }

        public ObjectsAddedMessage(ObjectItem[] @object)
        {
            this.@object = @object;
        }

        public ObjectsAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)@object.Count());
            for (var @objectIndex = 0; @objectIndex < @object.Count(); @objectIndex++)
            {
                var objectToSend = @object[@objectIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
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
