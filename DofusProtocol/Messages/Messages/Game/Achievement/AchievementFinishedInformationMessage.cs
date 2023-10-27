namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementFinishedInformationMessage : AchievementFinishedMessage
    {
        public new const uint Id = 6381;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public ulong PlayerId { get; set; }

        public AchievementFinishedInformationMessage(AchievementAchievedRewardable achievement, string name, ulong playerId)
        {
            this.Achievement = achievement;
            this.Name = name;
            this.PlayerId = playerId;
        }

        public AchievementFinishedInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
            PlayerId = reader.ReadVarULong();
        }

    }
}
