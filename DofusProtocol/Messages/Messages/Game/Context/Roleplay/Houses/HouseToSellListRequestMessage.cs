namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseToSellListRequestMessage : Message
    {
        public const uint Id = 6139;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort PageIndex { get; set; }

        public HouseToSellListRequestMessage(ushort pageIndex)
        {
            this.PageIndex = pageIndex;
        }

        public HouseToSellListRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(PageIndex);
        }

        public override void Deserialize(IDataReader reader)
        {
            PageIndex = reader.ReadVarUShort();
        }

    }
}
