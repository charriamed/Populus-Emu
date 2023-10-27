namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectInteger : ObjectEffect
    {
        public new const short Id = 70;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint Value { get; set; }

        public ObjectEffectInteger(ushort actionId, uint value)
        {
            this.ActionId = actionId;
            this.Value = value;
        }

        public ObjectEffectInteger() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Value = reader.ReadVarUInt();
        }

    }
}
