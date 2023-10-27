namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class QuestObjectiveInformations
    {
        public const short Id  = 385;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectiveId { get; set; }
        public bool ObjectiveStatus { get; set; }
        public string[] DialogParams { get; set; }

        public QuestObjectiveInformations(ushort objectiveId, bool objectiveStatus, string[] dialogParams)
        {
            this.ObjectiveId = objectiveId;
            this.ObjectiveStatus = objectiveStatus;
            this.DialogParams = dialogParams;
        }

        public QuestObjectiveInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectiveId);
            writer.WriteBoolean(ObjectiveStatus);
            writer.WriteShort((short)DialogParams.Count());
            for (var dialogParamsIndex = 0; dialogParamsIndex < DialogParams.Count(); dialogParamsIndex++)
            {
                writer.WriteUTF(DialogParams[dialogParamsIndex]);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectiveId = reader.ReadVarUShort();
            ObjectiveStatus = reader.ReadBoolean();
            var dialogParamsCount = reader.ReadUShort();
            DialogParams = new string[dialogParamsCount];
            for (var dialogParamsIndex = 0; dialogParamsIndex < dialogParamsCount; dialogParamsIndex++)
            {
                DialogParams[dialogParamsIndex] = reader.ReadUTF();
            }
        }

    }
}
