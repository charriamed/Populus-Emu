namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveUseEndedMessage : Message
    {
        public const uint Id = 6112;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ElemId { get; set; }
        public ushort SkillId { get; set; }

        public InteractiveUseEndedMessage(uint elemId, ushort skillId)
        {
            this.ElemId = elemId;
            this.SkillId = skillId;
        }

        public InteractiveUseEndedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ElemId);
            writer.WriteVarUShort(SkillId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ElemId = reader.ReadVarUInt();
            SkillId = reader.ReadVarUShort();
        }

    }
}
