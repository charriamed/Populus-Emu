namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementObjective
    {
        public const short Id  = 404;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint ObjectId { get; set; }
        public ushort MaxValue { get; set; }

        public AchievementObjective(uint objectId, ushort maxValue)
        {
            this.ObjectId = objectId;
            this.MaxValue = maxValue;
        }

        public AchievementObjective() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectId);
            writer.WriteVarUShort(MaxValue);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUInt();
            MaxValue = reader.ReadVarUShort();
        }

    }
}
