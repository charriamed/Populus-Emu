namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectAddedMessage : Message
    {
        public const uint Id = 3025;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem @object { get; set; }
        public sbyte Origin { get; set; }

        public ObjectAddedMessage(ObjectItem @object, sbyte origin)
        {
            this.@object = @object;
            this.Origin = origin;
        }

        public ObjectAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            @object.Serialize(writer);
            writer.WriteSByte(Origin);
        }

        public override void Deserialize(IDataReader reader)
        {
            @object = new ObjectItem();
            @object.Deserialize(reader);
            Origin = reader.ReadSByte();
        }

    }
}
