namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class QuestStepValidatedMessage : Message
    {
        public const uint Id = 6099;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort QuestId { get; set; }
        public ushort StepId { get; set; }

        public QuestStepValidatedMessage(ushort questId, ushort stepId)
        {
            this.QuestId = questId;
            this.StepId = stepId;
        }

        public QuestStepValidatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(QuestId);
            writer.WriteVarUShort(StepId);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestId = reader.ReadVarUShort();
            StepId = reader.ReadVarUShort();
        }

    }
}
