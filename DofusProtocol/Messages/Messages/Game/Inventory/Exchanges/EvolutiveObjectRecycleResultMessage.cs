namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class EvolutiveObjectRecycleResultMessage : Message
    {
        public const uint Id = 6779;
        public override uint MessageId
        {
            get { return Id; }
        }
        public RecycledItem[] RecycledItems { get; set; }

        public EvolutiveObjectRecycleResultMessage(RecycledItem[] recycledItems)
        {
            this.RecycledItems = recycledItems;
        }

        public EvolutiveObjectRecycleResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)RecycledItems.Count());
            for (var recycledItemsIndex = 0; recycledItemsIndex < RecycledItems.Count(); recycledItemsIndex++)
            {
                var objectToSend = RecycledItems[recycledItemsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var recycledItemsCount = reader.ReadUShort();
            RecycledItems = new RecycledItem[recycledItemsCount];
            for (var recycledItemsIndex = 0; recycledItemsIndex < recycledItemsCount; recycledItemsIndex++)
            {
                var objectToAdd = new RecycledItem();
                objectToAdd.Deserialize(reader);
                RecycledItems[recycledItemsIndex] = objectToAdd;
            }
        }

    }
}
