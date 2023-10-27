namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendDeleteRequestMessage : Message
    {
        public const uint Id = 5603;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int AccountId { get; set; }

        public FriendDeleteRequestMessage(int accountId)
        {
            this.AccountId = accountId;
        }

        public FriendDeleteRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(AccountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            AccountId = reader.ReadInt();
        }

    }
}
