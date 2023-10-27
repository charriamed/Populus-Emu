namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildPaddockRemovedMessage : Message
    {
        public const uint Id = 5955;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double PaddockId { get; set; }

        public GuildPaddockRemovedMessage(double paddockId)
        {
            this.PaddockId = paddockId;
        }

        public GuildPaddockRemovedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(PaddockId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PaddockId = reader.ReadDouble();
        }

    }
}
