namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectDate : ObjectEffect
    {
        public new const short Id = 72;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Year { get; set; }
        public sbyte Month { get; set; }
        public sbyte Day { get; set; }
        public sbyte Hour { get; set; }
        public sbyte Minute { get; set; }

        public ObjectEffectDate(ushort actionId, ushort year, sbyte month, sbyte day, sbyte hour, sbyte minute)
        {
            this.ActionId = actionId;
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
        }

        public ObjectEffectDate() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Year);
            writer.WriteSByte(Month);
            writer.WriteSByte(Day);
            writer.WriteSByte(Hour);
            writer.WriteSByte(Minute);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Year = reader.ReadVarUShort();
            Month = reader.ReadSByte();
            Day = reader.ReadSByte();
            Hour = reader.ReadSByte();
            Minute = reader.ReadSByte();
        }

    }
}
