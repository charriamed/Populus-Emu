namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ServerSessionConstantInteger : ServerSessionConstant
    {
        public new const short Id = 433;
        public override short TypeId
        {
            get { return Id; }
        }
        public int Value { get; set; }

        public ServerSessionConstantInteger(ushort objectId, int value)
        {
            this.ObjectId = objectId;
            this.Value = value;
        }

        public ServerSessionConstantInteger() { }

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
