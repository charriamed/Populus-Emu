namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectAveragePricesMessage : Message
    {
        public const uint Id = 6335;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] Ids { get; set; }
        public ulong[] AvgPrices { get; set; }

        public ObjectAveragePricesMessage(ushort[] ids, ulong[] avgPrices)
        {
            this.Ids = ids;
            this.AvgPrices = avgPrices;
        }

        public ObjectAveragePricesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Ids.Count());
            for (var idsIndex = 0; idsIndex < Ids.Count(); idsIndex++)
            {
                writer.WriteVarUShort(Ids[idsIndex]);
            }
            writer.WriteShort((short)AvgPrices.Count());
            for (var avgPricesIndex = 0; avgPricesIndex < AvgPrices.Count(); avgPricesIndex++)
            {
                writer.WriteVarULong(AvgPrices[avgPricesIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var idsCount = reader.ReadUShort();
            Ids = new ushort[idsCount];
            for (var idsIndex = 0; idsIndex < idsCount; idsIndex++)
            {
                Ids[idsIndex] = reader.ReadVarUShort();
            }
            var avgPricesCount = reader.ReadUShort();
            AvgPrices = new ulong[avgPricesCount];
            for (var avgPricesIndex = 0; avgPricesIndex < avgPricesCount; avgPricesIndex++)
            {
                AvgPrices[avgPricesIndex] = reader.ReadVarULong();
            }
        }

    }
}
