namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartedTaxCollectorShopMessage : Message
    {
        public const uint Id = 6664;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem[] Objects { get; set; }
        public ulong Kamas { get; set; }

        public ExchangeStartedTaxCollectorShopMessage(ObjectItem[] objects, ulong kamas)
        {
            this.Objects = objects;
            this.Kamas = kamas;
        }

        public ExchangeStartedTaxCollectorShopMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Objects.Count());
            for (var objectsIndex = 0; objectsIndex < Objects.Count(); objectsIndex++)
            {
                var objectToSend = Objects[objectsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarULong(Kamas);
        }

        public override void Deserialize(IDataReader reader)
        {
            var objectsCount = reader.ReadUShort();
            Objects = new ObjectItem[objectsCount];
            for (var objectsIndex = 0; objectsIndex < objectsCount; objectsIndex++)
            {
                var objectToAdd = new ObjectItem();
                objectToAdd.Deserialize(reader);
                Objects[objectsIndex] = objectToAdd;
            }
            Kamas = reader.ReadVarULong();
        }

    }
}
