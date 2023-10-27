namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractContactInformations
    {
        public const short Id  = 380;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int AccountId { get; set; }
        public string AccountName { get; set; }

        public AbstractContactInformations(int accountId, string accountName)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
        }

        public AbstractContactInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(AccountId);
            writer.WriteUTF(AccountName);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            AccountId = reader.ReadInt();
            AccountName = reader.ReadUTF();
        }

    }
}
