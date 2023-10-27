namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TrustStatusMessage : Message
    {
        public const uint Id = 6267;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Trusted { get; set; }
        public bool Certified { get; set; }

        public TrustStatusMessage(bool trusted, bool certified)
        {
            this.Trusted = trusted;
            this.Certified = certified;
        }

        public TrustStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Trusted);
            flag = BooleanByteWrapper.SetFlag(flag, 1, Certified);
            writer.WriteByte(flag);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            Trusted = BooleanByteWrapper.GetFlag(flag, 0);
            Certified = BooleanByteWrapper.GetFlag(flag, 1);
        }

    }
}
