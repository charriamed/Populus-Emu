namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class OrnamentGainedMessage : Message
    {
        public const uint Id = 6368;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short OrnamentId { get; set; }

        public OrnamentGainedMessage(short ornamentId)
        {
            this.OrnamentId = ornamentId;
        }

        public OrnamentGainedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(OrnamentId);
        }

        public override void Deserialize(IDataReader reader)
        {
            OrnamentId = reader.ReadShort();
        }

    }
}
