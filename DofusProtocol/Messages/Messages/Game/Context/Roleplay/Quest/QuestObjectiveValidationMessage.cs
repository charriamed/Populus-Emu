namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class QuestObjectiveValidationMessage : Message
    {
        public const uint Id = 6085;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort QuestId { get; set; }
        public ushort ObjectiveId { get; set; }

        public QuestObjectiveValidationMessage(ushort questId, ushort objectiveId)
        {
            this.QuestId = questId;
            this.ObjectiveId = objectiveId;
        }

        public QuestObjectiveValidationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(QuestId);
            writer.WriteVarUShort(ObjectiveId);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestId = reader.ReadVarUShort();
            ObjectiveId = reader.ReadVarUShort();
        }

    }
}
