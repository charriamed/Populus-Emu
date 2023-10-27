namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockToSellListRequestMessage : Message
    {
        public const uint Id = 6141;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort PageIndex { get; set; }

        public PaddockToSellListRequestMessage(ushort pageIndex)
        {
            this.PageIndex = pageIndex;
        }

        public PaddockToSellListRequestMessage() { }

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
