namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class QuestObjectiveInformationsWithCompletion : QuestObjectiveInformations
    {
        public new const short Id = 386;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort CurCompletion { get; set; }
        public ushort MaxCompletion { get; set; }

        public QuestObjectiveInformationsWithCompletion(ushort objectiveId, bool objectiveStatus, string[] dialogParams, ushort curCompletion, ushort maxCompletion)
        {
            this.ObjectiveId = objectiveId;
            this.ObjectiveStatus = objectiveStatus;
            this.DialogParams = dialogParams;
            this.CurCompletion = curCompletion;
            this.MaxCompletion = maxCompletion;
        }

        public QuestObjectiveInformationsWithCompletion() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(CurCompletion);
            writer.WriteVarUShort(MaxCompletion);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CurCompletion = reader.ReadVarUShort();
            MaxCompletion = reader.ReadVarUShort();
        }

    }
}
