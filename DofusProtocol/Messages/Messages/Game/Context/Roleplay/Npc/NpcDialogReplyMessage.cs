namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NpcDialogReplyMessage : Message
    {
        public const uint Id = 5616;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ReplyId { get; set; }

        public NpcDialogReplyMessage(uint replyId)
        {
            this.ReplyId = replyId;
        }

        public NpcDialogReplyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ReplyId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ReplyId = reader.ReadVarUInt();
        }

    }
}
