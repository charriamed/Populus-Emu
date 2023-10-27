namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class UnfollowQuestObjectiveRequestMessage : Message
    {
        public const uint Id = 6723;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort QuestId { get; set; }
        public short ObjectiveId { get; set; }

        public UnfollowQuestObjectiveRequestMessage(ushort questId, short objectiveId)
        {
            this.QuestId = questId;
            this.ObjectiveId = objectiveId;
        }

        public UnfollowQuestObjectiveRequestMessage() { }

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
