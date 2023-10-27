namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeCraftResultMagicWithObjectDescMessage : ExchangeCraftResultWithObjectDescMessage
    {
        public new const uint Id = 6188;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte MagicPoolStatus { get; set; }

        public ExchangeCraftResultMagicWithObjectDescMessage(sbyte craftResult, ObjectItemNotInContainer objectInfo, sbyte magicPoolStatus)
        {
            this.CraftResult = craftResult;
            this.ObjectInfo = objectInfo;
            this.MagicPoolStatus = magicPoolStatus;
        }

        public ExchangeCraftResultMagicWithObjectDescMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(MagicPoolStatus);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MagicPoolStatus = reader.ReadSByte();
        }

    }
}
