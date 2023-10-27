namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class StorageInventoryContentMessage : InventoryContentMessage
    {
        public new const uint Id = 5646;
        public override uint MessageId
        {
            get { return Id; }
        }

        public StorageInventoryContentMessage(ObjectItem[] objects, ulong kamas)
        {
            this.Objects = objects;
            this.Kamas = kamas;
        }

        public StorageInventoryContentMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
