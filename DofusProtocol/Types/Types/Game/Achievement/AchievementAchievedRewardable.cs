namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementAchievedRewardable : AchievementAchieved
    {
        public new const short Id = 515;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Finishedlevel { get; set; }

        public AchievementAchievedRewardable(ushort objectId, ulong achievedBy, ushort finishedlevel)
        {
            this.ObjectId = objectId;
            this.AchievedBy = achievedBy;
            this.Finishedlevel = finishedlevel;
        }

        public AchievementAchievedRewardable() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Finishedlevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Finishedlevel = reader.ReadVarUShort();
        }

    }
}
