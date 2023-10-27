namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementListMessage : Message
    {
        public const uint Id = 6205;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AchievementAchieved[] FinishedAchievements { get; set; }

        public AchievementListMessage(AchievementAchieved[] finishedAchievements)
        {
            this.FinishedAchievements = finishedAchievements;
        }

        public AchievementListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)FinishedAchievements.Count());
            for (var finishedAchievementsIndex = 0; finishedAchievementsIndex < FinishedAchievements.Count(); finishedAchievementsIndex++)
            {
                var objectToSend = FinishedAchievements[finishedAchievementsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var finishedAchievementsCount = reader.ReadUShort();
            FinishedAchievements = new AchievementAchieved[finishedAchievementsCount];
            for (var finishedAchievementsIndex = 0; finishedAchievementsIndex < finishedAchievementsCount; finishedAchievementsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<AchievementAchieved>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                FinishedAchievements[finishedAchievementsIndex] = objectToAdd;
            }
        }

    }
}
