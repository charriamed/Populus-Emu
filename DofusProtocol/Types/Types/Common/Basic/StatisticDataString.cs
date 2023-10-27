namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatisticDataString : StatisticData
    {
        public new const short Id = 487;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Value { get; set; }

        public StatisticDataString(string value)
        {
            this.Value = value;
        }

        public StatisticDataString() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Value = reader.ReadUTF();
        }

    }
}
