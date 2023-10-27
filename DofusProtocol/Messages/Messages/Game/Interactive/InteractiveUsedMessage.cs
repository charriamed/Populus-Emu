namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveUsedMessage : Message
    {
        public const uint Id = 5745;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong EntityId { get; set; }
        public uint ElemId { get; set; }
        public ushort SkillId { get; set; }
        public ushort Duration { get; set; }
        public bool CanMove { get; set; }

        public InteractiveUsedMessage(ulong entityId, uint elemId, ushort skillId, ushort duration, bool canMove)
        {
            this.EntityId = entityId;
            this.ElemId = elemId;
            this.SkillId = skillId;
            this.Duration = duration;
            this.CanMove = canMove;
        }

        public InteractiveUsedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(EntityId);
            writer.WriteVarUInt(ElemId);
            writer.WriteVarUShort(SkillId);
            writer.WriteVarUShort(Duration);
            writer.WriteBoolean(CanMove);
        }

        public override void Deserialize(IDataReader reader)
        {
            EntityId = reader.ReadVarULong();
            ElemId = reader.ReadVarUInt();
            SkillId = reader.ReadVarUShort();
            Duration = reader.ReadVarUShort();
            CanMove = reader.ReadBoolean();
        }

    }
}
