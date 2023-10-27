namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AccountLoggingKickedMessage : Message
    {
        public const uint Id = 6029;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Days { get; set; }
        public sbyte Hours { get; set; }
        public sbyte Minutes { get; set; }

        public AccountLoggingKickedMessage(ushort days, sbyte hours, sbyte minutes)
        {
            this.Days = days;
            this.Hours = hours;
            this.Minutes = minutes;
        }

        public AccountLoggingKickedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(Days);
            writer.WriteSByte(Hours);
            writer.WriteSByte(Minutes);
        }

        public override void Deserialize(IDataReader reader)
        {
            Days = reader.ReadVarUShort();
            Hours = reader.ReadSByte();
            Minutes = reader.ReadSByte();
        }

    }
}
