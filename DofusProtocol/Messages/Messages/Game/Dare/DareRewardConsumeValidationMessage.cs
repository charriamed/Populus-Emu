namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareRewardConsumeValidationMessage : Message
    {
        public const uint Id = 6675;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double DareId { get; set; }
        public sbyte Type { get; set; }

        public DareRewardConsumeValidationMessage(double dareId, sbyte type)
        {
            this.DareId = dareId;
            this.Type = type;
        }

        public DareRewardConsumeValidationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(DareId);
            writer.WriteSByte(Type);
        }

        public override void Deserialize(IDataReader reader)
        {
            DareId = reader.ReadDouble();
            Type = reader.ReadSByte();
        }

    }
}
