namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StorageObjectRemoveMessage : Message
    {
        public const uint Id = 5648;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }

        public StorageObjectRemoveMessage(uint objectUID)
        {
            this.ObjectUID = objectUID;
        }

        public StorageObjectRemoveMessage() { }

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
