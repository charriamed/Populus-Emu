namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StorageObjectUpdateMessage : Message
    {
        public const uint Id = 5647;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem @object { get; set; }

        public StorageObjectUpdateMessage(ObjectItem @object)
        {
            this.@object = @object;
        }

        public StorageObjectUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            @object.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            @object = new ObjectItem();
            @object.Deserialize(reader);
        }

    }
}
