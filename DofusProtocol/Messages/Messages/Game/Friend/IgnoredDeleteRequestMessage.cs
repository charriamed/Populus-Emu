namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IgnoredDeleteRequestMessage : Message
    {
        public const uint Id = 5680;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int AccountId { get; set; }
        public bool Session { get; set; }

        public IgnoredDeleteRequestMessage(int accountId, bool session)
        {
            this.AccountId = accountId;
            this.Session = session;
        }

        public IgnoredDeleteRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(AccountId);
            writer.WriteBoolean(Session);
        }

        public override void Deserialize(IDataReader reader)
        {
            AccountId = reader.ReadInt();
            Session = reader.ReadBoolean();
        }

    }
}
