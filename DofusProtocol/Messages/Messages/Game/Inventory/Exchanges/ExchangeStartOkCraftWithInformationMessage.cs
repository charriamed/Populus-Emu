namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkCraftWithInformationMessage : ExchangeStartOkCraftMessage
    {
        public new const uint Id = 5941;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint SkillId { get; set; }

        public ExchangeStartOkCraftWithInformationMessage(uint skillId)
        {
            this.SkillId = skillId;
        }

        public ExchangeStartOkCraftWithInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(SkillId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SkillId = reader.ReadVarUInt();
        }

    }
}
