namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartedWithStorageMessage : ExchangeStartedMessage
    {
        public new const uint Id = 6236;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint StorageMaxSlot { get; set; }

        public ExchangeStartedWithStorageMessage(sbyte exchangeType, uint storageMaxSlot)
        {
            this.ExchangeType = exchangeType;
            this.StorageMaxSlot = storageMaxSlot;
        }

        public ExchangeStartedWithStorageMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(StorageMaxSlot);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            StorageMaxSlot = reader.ReadVarUInt();
        }

    }
}
