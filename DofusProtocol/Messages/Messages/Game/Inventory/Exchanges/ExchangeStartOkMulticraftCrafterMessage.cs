namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkMulticraftCrafterMessage : Message
    {
        public const uint Id = 5818;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint SkillId { get; set; }

        public ExchangeStartOkMulticraftCrafterMessage(uint skillId)
        {
            this.SkillId = skillId;
        }

        public ExchangeStartOkMulticraftCrafterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(SkillId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SkillId = reader.ReadVarUInt();
        }

    }
}
