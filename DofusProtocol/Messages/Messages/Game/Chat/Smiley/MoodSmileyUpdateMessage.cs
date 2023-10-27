namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MoodSmileyUpdateMessage : Message
    {
        public const uint Id = 6388;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int AccountId { get; set; }
        public ulong PlayerId { get; set; }
        public ushort SmileyId { get; set; }

        public MoodSmileyUpdateMessage(int accountId, ulong playerId, ushort smileyId)
        {
            this.AccountId = accountId;
            this.PlayerId = playerId;
            this.SmileyId = smileyId;
        }

        public MoodSmileyUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(AccountId);
            writer.WriteVarULong(PlayerId);
            writer.WriteVarUShort(SmileyId);
        }

        public override void Deserialize(IDataReader reader)
        {
            AccountId = reader.ReadInt();
            PlayerId = reader.ReadVarULong();
            SmileyId = reader.ReadVarUShort();
        }

    }
}
