namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveUseErrorMessage : Message
    {
        public const uint Id = 6384;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ElemId { get; set; }
        public uint SkillInstanceUid { get; set; }

        public InteractiveUseErrorMessage(uint elemId, uint skillInstanceUid)
        {
            this.ElemId = elemId;
            this.SkillInstanceUid = skillInstanceUid;
        }

        public InteractiveUseErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ElemId);
            writer.WriteVarUInt(SkillInstanceUid);
        }

        public override void Deserialize(IDataReader reader)
        {
            ElemId = reader.ReadVarUInt();
            SkillInstanceUid = reader.ReadVarUInt();
        }

    }
}
