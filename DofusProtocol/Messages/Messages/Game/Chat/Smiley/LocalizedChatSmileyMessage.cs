namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LocalizedChatSmileyMessage : ChatSmileyMessage
    {
        public new const uint Id = 6185;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort CellId { get; set; }

        public LocalizedChatSmileyMessage(double entityId, ushort smileyId, int accountId, ushort cellId)
        {
            this.EntityId = entityId;
            this.SmileyId = smileyId;
            this.AccountId = accountId;
            this.CellId = cellId;
        }

        public LocalizedChatSmileyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(CellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CellId = reader.ReadVarUShort();
        }

    }
}
