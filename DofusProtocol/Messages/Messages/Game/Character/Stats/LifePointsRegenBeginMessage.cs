namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LifePointsRegenBeginMessage : Message
    {
        public const uint Id = 5684;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte RegenRate { get; set; }

        public LifePointsRegenBeginMessage(byte regenRate)
        {
            this.RegenRate = regenRate;
        }

        public LifePointsRegenBeginMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(RegenRate);
        }

        public override void Deserialize(IDataReader reader)
        {
            RegenRate = reader.ReadByte();
        }

    }
}
