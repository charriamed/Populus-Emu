namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class StorageObjectsRemoveMessage : Message
    {
        public const uint Id = 6035;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint[] ObjectUIDList { get; set; }

        public StorageObjectsRemoveMessage(uint[] objectUIDList)
        {
            this.ObjectUIDList = objectUIDList;
        }

        public StorageObjectsRemoveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ObjectUIDList.Count());
            for (var objectUIDListIndex = 0; objectUIDListIndex < ObjectUIDList.Count(); objectUIDListIndex++)
            {
                writer.WriteVarUInt(ObjectUIDList[objectUIDListIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var objectUIDListCount = reader.ReadUShort();
            ObjectUIDList = new uint[objectUIDListCount];
            for (var objectUIDListIndex = 0; objectUIDListIndex < objectUIDListCount; objectUIDListIndex++)
            {
                ObjectUIDList[objectUIDListIndex] = reader.ReadVarUInt();
            }
        }

    }
}
