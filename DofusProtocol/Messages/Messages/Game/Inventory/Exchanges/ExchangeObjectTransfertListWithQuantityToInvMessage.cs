namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectTransfertListWithQuantityToInvMessage : Message
    {
        public const uint Id = 6470;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint[] Ids { get; set; }
        public uint[] Qtys { get; set; }

        public ExchangeObjectTransfertListWithQuantityToInvMessage(uint[] ids, uint[] qtys)
        {
            this.Ids = ids;
            this.Qtys = qtys;
        }

        public ExchangeObjectTransfertListWithQuantityToInvMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Ids.Count());
            for (var idsIndex = 0; idsIndex < Ids.Count(); idsIndex++)
            {
                writer.WriteVarUInt(Ids[idsIndex]);
            }
            writer.WriteShort((short)Qtys.Count());
            for (var qtysIndex = 0; qtysIndex < Qtys.Count(); qtysIndex++)
            {
                writer.WriteVarUInt(Qtys[qtysIndex]);
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
            var qtysCount = reader.ReadUShort();
            Qtys = new uint[qtysCount];
            for (var qtysIndex = 0; qtysIndex < qtysCount; qtysIndex++)
            {
                Qtys[qtysIndex] = reader.ReadVarUInt();
            }
        }

    }
}
