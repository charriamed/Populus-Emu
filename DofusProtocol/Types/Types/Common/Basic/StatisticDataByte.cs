namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatisticDataByte : StatisticData
    {
        public new const short Id = 486;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte Value { get; set; }

        public StatisticDataByte(sbyte value)
        {
            this.Value = value;
        }

        public StatisticDataByte() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Value = reader.ReadSByte();
        }

    }
}
