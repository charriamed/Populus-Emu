namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementAchieved
    {
        public const short Id  = 514;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectId { get; set; }
        public ulong AchievedBy { get; set; }

        public AchievementAchieved(ushort objectId, ulong achievedBy)
        {
            this.ObjectId = objectId;
            this.AchievedBy = achievedBy;
        }

        public AchievementAchieved() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectId);
            writer.WriteVarULong(AchievedBy);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUShort();
            AchievedBy = reader.ReadVarULong();
        }

    }
}
