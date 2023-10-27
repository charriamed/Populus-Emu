namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AnomalySubareaInformation
    {
        public const short Id  = 565;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public short RewardRate { get; set; }
        public bool HasAnomaly { get; set; }
        public ulong AnomalyClosingTime { get; set; }

        public AnomalySubareaInformation(ushort subAreaId, short rewardRate, bool hasAnomaly, ulong anomalyClosingTime)
        {
            this.SubAreaId = subAreaId;
            this.RewardRate = rewardRate;
            this.HasAnomaly = hasAnomaly;
            this.AnomalyClosingTime = anomalyClosingTime;
        }

        public AnomalySubareaInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteVarShort(RewardRate);
            writer.WriteBoolean(HasAnomaly);
            writer.WriteVarULong(AnomalyClosingTime);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            RewardRate = reader.ReadVarShort();
            HasAnomaly = reader.ReadBoolean();
            AnomalyClosingTime = reader.ReadVarULong();
        }

    }
}
