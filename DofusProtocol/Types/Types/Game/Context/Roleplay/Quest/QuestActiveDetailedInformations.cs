namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class QuestActiveDetailedInformations : QuestActiveInformations
    {
        public new const short Id = 382;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort StepId { get; set; }
        public QuestObjectiveInformations[] Objectives { get; set; }

        public QuestActiveDetailedInformations(ushort questId, ushort stepId, QuestObjectiveInformations[] objectives)
        {
            this.QuestId = questId;
            this.StepId = stepId;
            this.Objectives = objectives;
        }

        public QuestActiveDetailedInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(StepId);
            writer.WriteShort((short)Objectives.Count());
            for (var objectivesIndex = 0; objectivesIndex < Objectives.Count(); objectivesIndex++)
            {
                var objectToSend = Objectives[objectivesIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            StepId = reader.ReadVarUShort();
            var objectivesCount = reader.ReadUShort();
            Objectives = new QuestObjectiveInformations[objectivesCount];
            for (var objectivesIndex = 0; objectivesIndex < objectivesCount; objectivesIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<QuestObjectiveInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Objectives[objectivesIndex] = objectToAdd;
            }
        }

    }
}
