namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectsDeletedMessage : Message
    {
        public const uint Id = 6034;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint[] ObjectUID { get; set; }

        public ObjectsDeletedMessage(uint[] objectUID)
        {
            this.ObjectUID = objectUID;
        }

        public ObjectsDeletedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ObjectUID.Count());
            for (var objectUIDIndex = 0; objectUIDIndex < ObjectUID.Count(); objectUIDIndex++)
            {
                writer.WriteVarUInt(ObjectUID[objectUIDIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var objectUIDCount = reader.ReadUShort();
            ObjectUID = new uint[objectUIDCount];
            for (var objectUIDIndex = 0; objectUIDIndex < objectUIDCount; objectUIDIndex++)
            {
                ObjectUID[objectUIDIndex] = reader.ReadVarUInt();
            }
        }

    }
}
