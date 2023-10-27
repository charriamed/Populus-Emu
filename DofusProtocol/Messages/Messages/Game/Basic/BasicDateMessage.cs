namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicDateMessage : Message
    {
        public const uint Id = 177;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Day { get; set; }
        public sbyte Month { get; set; }
        public short Year { get; set; }

        public BasicDateMessage(sbyte day, sbyte month, short year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }

        public BasicDateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Day);
            writer.WriteSByte(Month);
            writer.WriteShort(Year);
        }

        public override void Deserialize(IDataReader reader)
        {
            Day = reader.ReadSByte();
            Month = reader.ReadSByte();
            Year = reader.ReadShort();
        }

    }
}
