namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class QuestListMessage : Message
    {
        public const uint Id = 5626;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] FinishedQuestsIds { get; set; }
        public ushort[] FinishedQuestsCounts { get; set; }
        public QuestActiveInformations[] ActiveQuests { get; set; }
        public ushort[] ReinitDoneQuestsIds { get; set; }

        public QuestListMessage(ushort[] finishedQuestsIds, ushort[] finishedQuestsCounts, QuestActiveInformations[] activeQuests, ushort[] reinitDoneQuestsIds)
        {
            this.FinishedQuestsIds = finishedQuestsIds;
            this.FinishedQuestsCounts = finishedQuestsCounts;
            this.ActiveQuests = activeQuests;
            this.ReinitDoneQuestsIds = reinitDoneQuestsIds;
        }

        public QuestListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)FinishedQuestsIds.Count());
            for (var finishedQuestsIdsIndex = 0; finishedQuestsIdsIndex < FinishedQuestsIds.Count(); finishedQuestsIdsIndex++)
            {
                writer.WriteVarUShort(FinishedQuestsIds[finishedQuestsIdsIndex]);
            }
            writer.WriteShort((short)FinishedQuestsCounts.Count());
            for (var finishedQuestsCountsIndex = 0; finishedQuestsCountsIndex < FinishedQuestsCounts.Count(); finishedQuestsCountsIndex++)
            {
                writer.WriteVarUShort(FinishedQuestsCounts[finishedQuestsCountsIndex]);
            }
            writer.WriteShort((short)ActiveQuests.Count());
            for (var activeQuestsIndex = 0; activeQuestsIndex < ActiveQuests.Count(); activeQuestsIndex++)
            {
                var objectToSend = ActiveQuests[activeQuestsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)ReinitDoneQuestsIds.Count());
            for (var reinitDoneQuestsIdsIndex = 0; reinitDoneQuestsIdsIndex < ReinitDoneQuestsIds.Count(); reinitDoneQuestsIdsIndex++)
            {
                writer.WriteVarUShort(ReinitDoneQuestsIds[reinitDoneQuestsIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var finishedQuestsIdsCount = reader.ReadUShort();
            FinishedQuestsIds = new ushort[finishedQuestsIdsCount];
            for (var finishedQuestsIdsIndex = 0; finishedQuestsIdsIndex < finishedQuestsIdsCount; finishedQuestsIdsIndex++)
            {
                FinishedQuestsIds[finishedQuestsIdsIndex] = reader.ReadVarUShort();
            }
            var finishedQuestsCountsCount = reader.ReadUShort();
            FinishedQuestsCounts = new ushort[finishedQuestsCountsCount];
            for (var finishedQuestsCountsIndex = 0; finishedQuestsCountsIndex < finishedQuestsCountsCount; finishedQuestsCountsIndex++)
            {
                FinishedQuestsCounts[finishedQuestsCountsIndex] = reader.ReadVarUShort();
            }
            var activeQuestsCount = reader.ReadUShort();
            ActiveQuests = new QuestActiveInformations[activeQuestsCount];
            for (var activeQuestsIndex = 0; activeQuestsIndex < activeQuestsCount; activeQuestsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<QuestActiveInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                ActiveQuests[activeQuestsIndex] = objectToAdd;
            }
            var reinitDoneQuestsIdsCount = reader.ReadUShort();
            ReinitDoneQuestsIds = new ushort[reinitDoneQuestsIdsCount];
            for (var reinitDoneQuestsIdsIndex = 0; reinitDoneQuestsIdsIndex < reinitDoneQuestsIdsCount; reinitDoneQuestsIdsIndex++)
            {
                ReinitDoneQuestsIds[reinitDoneQuestsIdsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
