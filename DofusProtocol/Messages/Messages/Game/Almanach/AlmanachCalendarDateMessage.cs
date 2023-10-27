namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AlmanachCalendarDateMessage : Message
    {
        public const uint Id = 6341;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int Date { get; set; }

        public AlmanachCalendarDateMessage(int date)
        {
            this.Date = date;
        }

        public AlmanachCalendarDateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(Date);
        }

        public override void Deserialize(IDataReader reader)
        {
            Date = reader.ReadInt();
        }

    }
}
