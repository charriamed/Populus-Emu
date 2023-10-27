namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class NewMailMessage : MailStatusMessage
    {
        public new const uint Id = 6292;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int[] SendersAccountId { get; set; }

        public NewMailMessage(ushort unread, ushort total, int[] sendersAccountId)
        {
            this.Unread = unread;
            this.Total = total;
            this.SendersAccountId = sendersAccountId;
        }

        public NewMailMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)SendersAccountId.Count());
            for (var sendersAccountIdIndex = 0; sendersAccountIdIndex < SendersAccountId.Count(); sendersAccountIdIndex++)
            {
                writer.WriteInt(SendersAccountId[sendersAccountIdIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var sendersAccountIdCount = reader.ReadUShort();
            SendersAccountId = new int[sendersAccountIdCount];
            for (var sendersAccountIdIndex = 0; sendersAccountIdIndex < sendersAccountIdCount; sendersAccountIdIndex++)
            {
                SendersAccountId[sendersAccountIdIndex] = reader.ReadInt();
            }
        }

    }
}
