namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementStartedObjective : AchievementObjective
    {
        public new const short Id = 402;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Value { get; set; }

        public AchievementStartedObjective(uint objectId, ushort maxValue, ushort value)
        {
            this.ObjectId = objectId;
            this.MaxValue = maxValue;
            this.Value = value;
        }

        public AchievementStartedObjective() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Value = reader.ReadVarUShort();
        }

    }
}
