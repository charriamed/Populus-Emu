namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockToSellListMessage : Message
    {
        public const uint Id = 6138;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort PageIndex { get; set; }
        public ushort TotalPage { get; set; }
        public PaddockInformationsForSell[] PaddockList { get; set; }

        public PaddockToSellListMessage(ushort pageIndex, ushort totalPage, PaddockInformationsForSell[] paddockList)
        {
            this.PageIndex = pageIndex;
            this.TotalPage = totalPage;
            this.PaddockList = paddockList;
        }

        public PaddockToSellListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(PageIndex);
            writer.WriteVarUShort(TotalPage);
            writer.WriteShort((short)PaddockList.Count());
            for (var paddockListIndex = 0; paddockListIndex < PaddockList.Count(); paddockListIndex++)
            {
                var objectToSend = PaddockList[paddockListIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            PageIndex = reader.ReadVarUShort();
            TotalPage = reader.ReadVarUShort();
            var paddockListCount = reader.ReadUShort();
            PaddockList = new PaddockInformationsForSell[paddockListCount];
            for (var paddockListIndex = 0; paddockListIndex < paddockListCount; paddockListIndex++)
            {
                var objectToAdd = new PaddockInformationsForSell();
                objectToAdd.Deserialize(reader);
                PaddockList[paddockListIndex] = objectToAdd;
            }
        }

    }
}
