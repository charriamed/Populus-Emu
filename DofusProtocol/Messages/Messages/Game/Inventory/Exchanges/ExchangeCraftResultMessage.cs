namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeCraftResultMessage : Message
    {
        public const uint Id = 5790;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte CraftResult { get; set; }

        public ExchangeCraftResultMessage(sbyte craftResult)
        {
            this.CraftResult = craftResult;
        }

        public ExchangeCraftResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(CraftResult);
        }

        public override void Deserialize(IDataReader reader)
        {
            CraftResult = reader.ReadSByte();
        }

    }
}
