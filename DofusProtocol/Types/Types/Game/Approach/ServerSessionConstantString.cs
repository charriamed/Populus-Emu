namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ServerSessionConstantString : ServerSessionConstant
    {
        public new const short Id = 436;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Value { get; set; }

        public ServerSessionConstantString(ushort objectId, string value)
        {
            this.ObjectId = objectId;
            this.Value = value;
        }

        public ServerSessionConstantString() { }

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
