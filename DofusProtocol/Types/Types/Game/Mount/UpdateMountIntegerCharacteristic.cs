namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class UpdateMountIntegerCharacteristic : UpdateMountCharacteristic
    {
        public new const short Id = 537;
        public override short TypeId
        {
            get { return Id; }
        }
        public int Value { get; set; }

        public UpdateMountIntegerCharacteristic(sbyte type, int value)
        {
            this.Type = type;
            this.Value = value;
        }

        public UpdateMountIntegerCharacteristic() { }

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
