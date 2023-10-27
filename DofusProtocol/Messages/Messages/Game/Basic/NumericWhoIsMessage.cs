namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NumericWhoIsMessage : Message
    {
        public const uint Id = 6297;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public int AccountId { get; set; }

        public NumericWhoIsMessage(ulong playerId, int accountId)
        {
            this.PlayerId = playerId;
            this.AccountId = accountId;
        }

        public NumericWhoIsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(PlayerId);
            writer.WriteInt(AccountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarULong();
            AccountId = reader.ReadInt();
        }

    }
}
