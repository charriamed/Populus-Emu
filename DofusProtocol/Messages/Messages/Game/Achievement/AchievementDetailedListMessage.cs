namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementDetailedListMessage : Message
    {
        public const uint Id = 6358;
        public override uint MessageId
        {
            get { return Id; }
        }
        public Achievement[] StartedAchievements { get; set; }
        public Achievement[] FinishedAchievements { get; set; }

        public AchievementDetailedListMessage(Achievement[] startedAchievements, Achievement[] finishedAchievements)
        {
            this.StartedAchievements = startedAchievements;
            this.FinishedAchievements = finishedAchievements;
        }

        public AchievementDetailedListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)StartedAchievements.Count());
            for (var startedAchievementsIndex = 0; startedAchievementsIndex < StartedAchievements.Count(); startedAchievementsIndex++)
            {
                var objectToSend = StartedAchievements[startedAchievementsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)FinishedAchievements.Count());
            for (var finishedAchievementsIndex = 0; finishedAchievementsIndex < FinishedAchievements.Count(); finishedAchievementsIndex++)
            {
                var objectToSend = FinishedAchievements[finishedAchievementsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var startedAchievementsCount = reader.ReadUShort();
            StartedAchievements = new Achievement[startedAchievementsCount];
            for (var startedAchievementsIndex = 0; startedAchievementsIndex < startedAchievementsCount; startedAchievementsIndex++)
            {
                var objectToAdd = new Achievement();
                objectToAdd.Deserialize(reader);
                StartedAchievements[startedAchievementsIndex] = objectToAdd;
            }
            var finishedAchievementsCount = reader.ReadUShort();
            FinishedAchievements = new Achievement[finishedAchievementsCount];
            for (var finishedAchievementsIndex = 0; finishedAchievementsIndex < finishedAchievementsCount; finishedAchievementsIndex++)
            {
                var objectToAdd = new Achievement();
                objectToAdd.Deserialize(reader);
                FinishedAchievements[finishedAchievementsIndex] = objectToAdd;
            }
        }

    }
}
