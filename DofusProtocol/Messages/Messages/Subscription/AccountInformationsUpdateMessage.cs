namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AccountInformationsUpdateMessage : Message
    {
        public const uint Id = 6740;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double SubscriptionEndDate { get; set; }
        public double UnlimitedRestatEndDate { get; set; }

        public AccountInformationsUpdateMessage(double subscriptionEndDate, double unlimitedRestatEndDate)
        {
            this.SubscriptionEndDate = subscriptionEndDate;
            this.UnlimitedRestatEndDate = unlimitedRestatEndDate;
        }

        public AccountInformationsUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(SubscriptionEndDate);
            writer.WriteDouble(UnlimitedRestatEndDate);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubscriptionEndDate = reader.ReadDouble();
            UnlimitedRestatEndDate = reader.ReadDouble();
        }

    }
}
