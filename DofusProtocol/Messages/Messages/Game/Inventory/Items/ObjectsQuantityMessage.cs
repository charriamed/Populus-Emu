namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectsQuantityMessage : Message
    {
        public const uint Id = 6206;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItemQuantity[] ObjectsUIDAndQty { get; set; }

        public ObjectsQuantityMessage(ObjectItemQuantity[] objectsUIDAndQty)
        {
            this.ObjectsUIDAndQty = objectsUIDAndQty;
        }

        public ObjectsQuantityMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ObjectsUIDAndQty.Count());
            for (var objectsUIDAndQtyIndex = 0; objectsUIDAndQtyIndex < ObjectsUIDAndQty.Count(); objectsUIDAndQtyIndex++)
            {
                var objectToSend = ObjectsUIDAndQty[objectsUIDAndQtyIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var objectsUIDAndQtyCount = reader.ReadUShort();
            ObjectsUIDAndQty = new ObjectItemQuantity[objectsUIDAndQtyCount];
            for (var objectsUIDAndQtyIndex = 0; objectsUIDAndQtyIndex < objectsUIDAndQtyCount; objectsUIDAndQtyIndex++)
            {
                var objectToAdd = new ObjectItemQuantity();
                objectToAdd.Deserialize(reader);
                ObjectsUIDAndQty[objectsUIDAndQtyIndex] = objectToAdd;
            }
        }

    }
}
