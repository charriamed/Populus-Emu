namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IgnoredInformations : AbstractContactInformations
    {
        public new const short Id = 106;
        public override short TypeId
        {
            get { return Id; }
        }

        public IgnoredInformations(int accountId, string accountName)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
        }

        public IgnoredInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
