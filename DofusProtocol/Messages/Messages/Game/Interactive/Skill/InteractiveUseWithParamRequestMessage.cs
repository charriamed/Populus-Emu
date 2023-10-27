namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveUseWithParamRequestMessage : InteractiveUseRequestMessage
    {
        public new const uint Id = 6715;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int ObjectId { get; set; }

        public InteractiveUseWithParamRequestMessage(uint elemId, uint skillInstanceUid, int objectId)
        {
            this.ElemId = elemId;
            this.SkillInstanceUid = skillInstanceUid;
            this.ObjectId = objectId;
        }

        public InteractiveUseWithParamRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectId = reader.ReadInt();
        }

    }
}
