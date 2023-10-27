namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountXpRatioMessage : Message
    {
        public const uint Id = 5970;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Ratio { get; set; }

        public MountXpRatioMessage(sbyte ratio)
        {
            this.Ratio = ratio;
        }

        public MountXpRatioMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Ratio);
        }

        public override void Deserialize(IDataReader reader)
        {
            Ratio = reader.ReadSByte();
        }

    }
}
