namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class UpdateMountBooleanCharacteristic : UpdateMountCharacteristic
    {
        public new const short Id = 538;
        public override short TypeId
        {
            get { return Id; }
        }
        public bool Value { get; set; }

        public UpdateMountBooleanCharacteristic(sbyte type, bool value)
        {
            this.Type = type;
            this.Value = value;
        }

        public UpdateMountBooleanCharacteristic() { }

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
