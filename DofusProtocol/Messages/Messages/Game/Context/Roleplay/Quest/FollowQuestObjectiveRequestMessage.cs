namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FollowQuestObjectiveRequestMessage : Message
    {
        public const uint Id = 6724;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort QuestId { get; set; }
        public short ObjectiveId { get; set; }

        public FollowQuestObjectiveRequestMessage(ushort questId, short objectiveId)
        {
            this.QuestId = questId;
            this.ObjectiveId = objectiveId;
        }

        public FollowQuestObjectiveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(QuestId);
            writer.WriteShort(ObjectiveId);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestId = reader.ReadVarUShort();
            ObjectiveId = reader.ReadShort();
        }

    }
}
