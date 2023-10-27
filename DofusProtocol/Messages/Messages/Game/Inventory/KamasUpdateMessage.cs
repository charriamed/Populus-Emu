namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class KamasUpdateMessage : Message
    {
        public const uint Id = 5537;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong KamasTotal { get; set; }

        public KamasUpdateMessage(ulong kamasTotal)
        {
            this.KamasTotal = kamasTotal;
        }

        public KamasUpdateMessage() { }

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
