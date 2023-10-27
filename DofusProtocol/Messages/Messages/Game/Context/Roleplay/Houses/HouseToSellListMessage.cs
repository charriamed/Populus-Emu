namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HouseToSellListMessage : Message
    {
        public const uint Id = 6140;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort PageIndex { get; set; }
        public ushort TotalPage { get; set; }
        public HouseInformationsForSell[] HouseList { get; set; }

        public HouseToSellListMessage(ushort pageIndex, ushort totalPage, HouseInformationsForSell[] houseList)
        {
            this.PageIndex = pageIndex;
            this.TotalPage = totalPage;
            this.HouseList = houseList;
        }

        public HouseToSellListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(PageIndex);
            writer.WriteVarUShort(TotalPage);
            writer.WriteShort((short)HouseList.Count());
            for (var houseListIndex = 0; houseListIndex < HouseList.Count(); houseListIndex++)
            {
                var objectToSend = HouseList[houseListIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            PageIndex = reader.ReadVarUShort();
            TotalPage = reader.ReadVarUShort();
            var houseListCount = reader.ReadUShort();
            HouseList = new HouseInformationsForSell[houseListCount];
            for (var houseListIndex = 0; houseListIndex < houseListCount; houseListIndex++)
            {
                var objectToAdd = new HouseInformationsForSell();
                objectToAdd.Deserialize(reader);
                HouseList[houseListIndex] = objectToAdd;
            }
        }

    }
}
