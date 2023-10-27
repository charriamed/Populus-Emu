namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MailStatusMessage : Message
    {
        public const uint Id = 6275;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Unread { get; set; }
        public ushort Total { get; set; }

        public MailStatusMessage(ushort unread, ushort total)
        {
            this.Unread = unread;
            this.Total = total;
        }

        public MailStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(Unread);
            writer.WriteVarUShort(Total);
        }

        public override void Deserialize(IDataReader reader)
        {
            Unread = reader.ReadVarUShort();
            Total = reader.ReadVarUShort();
        }

    }
}
