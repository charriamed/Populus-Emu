namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectString : ObjectEffect
    {
        public new const short Id = 74;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Value { get; set; }

        public ObjectEffectString(ushort actionId, string value)
        {
            this.ActionId = actionId;
            this.Value = value;
        }

        public ObjectEffectString() { }

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
