namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatisticDataBoolean : StatisticData
    {
        public new const short Id = 482;
        public override short TypeId
        {
            get { return Id; }
        }
        public bool Value { get; set; }

        public StatisticDataBoolean(bool value)
        {
            this.Value = value;
        }

        public StatisticDataBoolean() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Value = reader.ReadBoolean();
        }

    }
}
