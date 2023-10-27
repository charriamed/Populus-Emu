namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class StorageObjectsUpdateMessage : Message
    {
        public const uint Id = 6036;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem[] ObjectList { get; set; }

        public StorageObjectsUpdateMessage(ObjectItem[] objectList)
        {
            this.ObjectList = objectList;
        }

        public StorageObjectsUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ObjectList.Count());
            for (var objectListIndex = 0; objectListIndex < ObjectList.Count(); objectListIndex++)
            {
                var objectToSend = ObjectList[objectListIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var objectListCount = reader.ReadUShort();
            ObjectList = new ObjectItem[objectListCount];
            for (var objectListIndex = 0; objectListIndex < objectListCount; objectListIndex++)
            {
                var objectToAdd = new ObjectItem();
                objectToAdd.Deserialize(reader);
                ObjectList[objectListIndex] = objectToAdd;
            }
        }

    }
}
