namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatisticDataShort : StatisticData
    {
        public new const short Id = 488;
        public override short TypeId
        {
            get { return Id; }
        }
        public short Value { get; set; }

        public StatisticDataShort(short value)
        {
            this.Value = value;
        }

        public StatisticDataShort() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Value = reader.ReadShort();
        }

    }
}
