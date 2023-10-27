namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangePlayerMultiCraftRequestMessage : ExchangeRequestMessage
    {
        public new const uint Id = 5784;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Target { get; set; }
        public uint SkillId { get; set; }

        public ExchangePlayerMultiCraftRequestMessage(sbyte exchangeType, ulong target, uint skillId)
        {
            this.ExchangeType = exchangeType;
            this.Target = target;
            this.SkillId = skillId;
        }

        public ExchangePlayerMultiCraftRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(Target);
            writer.WriteVarUInt(SkillId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Target = reader.ReadVarULong();
            SkillId = reader.ReadVarUInt();
        }

    }
}
