namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatSmileyMessage : Message
    {
        public const uint Id = 801;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double EntityId { get; set; }
        public ushort SmileyId { get; set; }
        public int AccountId { get; set; }

        public ChatSmileyMessage(double entityId, ushort smileyId, int accountId)
        {
            this.EntityId = entityId;
            this.SmileyId = smileyId;
            this.AccountId = accountId;
        }

        public ChatSmileyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(EntityId);
            writer.WriteVarUShort(SmileyId);
            writer.WriteInt(AccountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            EntityId = reader.ReadDouble();
            SmileyId = reader.ReadVarUShort();
            AccountId = reader.ReadInt();
        }

    }
}
