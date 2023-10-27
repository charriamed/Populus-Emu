namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareRewardConsumeRequestMessage : Message
    {
        public const uint Id = 6676;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double DareId { get; set; }
        public sbyte Type { get; set; }

        public DareRewardConsumeRequestMessage(double dareId, sbyte type)
        {
            this.DareId = dareId;
            this.Type = type;
        }

        public DareRewardConsumeRequestMessage() { }

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
