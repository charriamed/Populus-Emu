namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class OrnamentLostMessage : Message
    {
        public const uint Id = 6770;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short OrnamentId { get; set; }

        public OrnamentLostMessage(short ornamentId)
        {
            this.OrnamentId = ornamentId;
        }

        public OrnamentLostMessage() { }

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
