namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectDuration : ObjectEffect
    {
        public new const short Id = 75;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Days { get; set; }
        public sbyte Hours { get; set; }
        public sbyte Minutes { get; set; }

        public ObjectEffectDuration(ushort actionId, ushort days, sbyte hours, sbyte minutes)
        {
            this.ActionId = actionId;
            this.Days = days;
            this.Hours = hours;
            this.Minutes = minutes;
        }

        public ObjectEffectDuration() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Days);
            writer.WriteSByte(Hours);
            writer.WriteSByte(Minutes);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Days = reader.ReadVarUShort();
            Hours = reader.ReadSByte();
            Minutes = reader.ReadSByte();
        }

    }
}
