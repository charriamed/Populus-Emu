namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectMinMax : ObjectEffect
    {
        public new const short Id = 82;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint Min { get; set; }
        public uint Max { get; set; }

        public ObjectEffectMinMax(ushort actionId, uint min, uint max)
        {
            this.ActionId = actionId;
            this.Min = min;
            this.Max = max;
        }

        public ObjectEffectMinMax() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Min);
            writer.WriteVarUInt(Max);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Min = reader.ReadVarUInt();
            Max = reader.ReadVarUInt();
        }

    }
}
