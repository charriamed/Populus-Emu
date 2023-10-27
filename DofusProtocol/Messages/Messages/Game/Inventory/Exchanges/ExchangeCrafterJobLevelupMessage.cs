namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeCrafterJobLevelupMessage : Message
    {
        public const uint Id = 6598;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte CrafterJobLevel { get; set; }

        public ExchangeCrafterJobLevelupMessage(byte crafterJobLevel)
        {
            this.CrafterJobLevel = crafterJobLevel;
        }

        public ExchangeCrafterJobLevelupMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(CrafterJobLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            CrafterJobLevel = reader.ReadByte();
        }

    }
}
