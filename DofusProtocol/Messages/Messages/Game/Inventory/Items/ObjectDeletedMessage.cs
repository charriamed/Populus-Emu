namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectDeletedMessage : Message
    {
        public const uint Id = 3024;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }

        public ObjectDeletedMessage(uint objectUID)
        {
            this.ObjectUID = objectUID;
        }

        public ObjectDeletedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
        }

    }
}
