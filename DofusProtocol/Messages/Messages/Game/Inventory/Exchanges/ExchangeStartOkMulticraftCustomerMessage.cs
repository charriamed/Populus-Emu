namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkMulticraftCustomerMessage : Message
    {
        public const uint Id = 5817;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint SkillId { get; set; }
        public byte CrafterJobLevel { get; set; }

        public ExchangeStartOkMulticraftCustomerMessage(uint skillId, byte crafterJobLevel)
        {
            this.SkillId = skillId;
            this.CrafterJobLevel = crafterJobLevel;
        }

        public ExchangeStartOkMulticraftCustomerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(SkillId);
            writer.WriteByte(CrafterJobLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            SkillId = reader.ReadVarUInt();
            CrafterJobLevel = reader.ReadByte();
        }

    }
}
