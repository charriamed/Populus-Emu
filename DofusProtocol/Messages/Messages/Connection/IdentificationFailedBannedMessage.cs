namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdentificationFailedBannedMessage : IdentificationFailedMessage
    {
        public new const uint Id = 6174;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double BanEndDate { get; set; }

        public IdentificationFailedBannedMessage(sbyte reason, double banEndDate)
        {
            this.Reason = reason;
            this.BanEndDate = banEndDate;
        }

        public IdentificationFailedBannedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(BanEndDate);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            BanEndDate = reader.ReadDouble();
        }

    }
}
