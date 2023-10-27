namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StorageKamasUpdateMessage : Message
    {
        public const uint Id = 5645;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong KamasTotal { get; set; }

        public StorageKamasUpdateMessage(ulong kamasTotal)
        {
            this.KamasTotal = kamasTotal;
        }

        public StorageKamasUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(KamasTotal);
        }

        public override void Deserialize(IDataReader reader)
        {
            KamasTotal = reader.ReadVarULong();
        }

    }
}
