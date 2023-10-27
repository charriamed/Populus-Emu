namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatisticDataInt : StatisticData
    {
        public new const short Id = 485;
        public override short TypeId
        {
            get { return Id; }
        }
        public int Value { get; set; }

        public StatisticDataInt(int value)
        {
            this.Value = value;
        }

        public StatisticDataInt() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Value = reader.ReadInt();
        }

    }
}
