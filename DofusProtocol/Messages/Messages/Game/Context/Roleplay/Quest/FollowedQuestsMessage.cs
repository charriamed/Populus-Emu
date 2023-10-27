namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FollowedQuestsMessage : Message
    {
        public const uint Id = 6717;
        public override uint MessageId
        {
            get { return Id; }
        }
        public QuestActiveDetailedInformations[] Quests { get; set; }

        public FollowedQuestsMessage(QuestActiveDetailedInformations[] quests)
        {
            this.Quests = quests;
        }

        public FollowedQuestsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Quests.Count());
            for (var questsIndex = 0; questsIndex < Quests.Count(); questsIndex++)
            {
                var objectToSend = Quests[questsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var questsCount = reader.ReadUShort();
            Quests = new QuestActiveDetailedInformations[questsCount];
            for (var questsIndex = 0; questsIndex < questsCount; questsIndex++)
            {
                var objectToAdd = new QuestActiveDetailedInformations();
                objectToAdd.Deserialize(reader);
                Quests[questsIndex] = objectToAdd;
            }
        }

    }
}
