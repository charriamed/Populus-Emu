namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectTransfertListFromInvMessage : Message
    {
        public const uint Id = 6183;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint[] Ids { get; set; }

        public ExchangeObjectTransfertListFromInvMessage(uint[] ids)
        {
            this.Ids = ids;
        }

        public ExchangeObjectTransfertListFromInvMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Ids.Count());
            for (var idsIndex = 0; idsIndex < Ids.Count(); idsIndex++)
            {
                writer.WriteVarUInt(Ids[idsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var idsCount = reader.ReadUShort();
            Ids = new uint[idsCount];
            for (var idsIndex = 0; idsIndex < idsCount; idsIndex++)
            {
                Ids[idsIndex] = reader.ReadVarUInt();
            }
        }

    }
}
