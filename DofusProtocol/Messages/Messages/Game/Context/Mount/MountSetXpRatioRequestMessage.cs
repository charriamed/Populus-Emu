namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountSetXpRatioRequestMessage : Message
    {
        public const uint Id = 5989;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte XpRatio { get; set; }

        public MountSetXpRatioRequestMessage(sbyte xpRatio)
        {
            this.XpRatio = xpRatio;
        }

        public MountSetXpRatioRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(XpRatio);
        }

        public override void Deserialize(IDataReader reader)
        {
            XpRatio = reader.ReadSByte();
        }

    }
}
