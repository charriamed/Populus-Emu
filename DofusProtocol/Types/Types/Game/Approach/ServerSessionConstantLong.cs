namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ServerSessionConstantLong : ServerSessionConstant
    {
        public new const short Id = 429;
        public override short TypeId
        {
            get { return Id; }
        }
        public double Value { get; set; }

        public ServerSessionConstantLong(ushort objectId, double value)
        {
            this.ObjectId = objectId;
            this.Value = value;
        }

        public ServerSessionConstantLong() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Value = reader.ReadDouble();
        }

    }
}
