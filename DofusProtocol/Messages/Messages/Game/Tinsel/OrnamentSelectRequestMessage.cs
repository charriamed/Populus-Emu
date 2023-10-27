namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class OrnamentSelectRequestMessage : Message
    {
        public const uint Id = 6374;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort OrnamentId { get; set; }

        public OrnamentSelectRequestMessage(ushort ornamentId)
        {
            this.OrnamentId = ornamentId;
        }

        public OrnamentSelectRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(OrnamentId);
        }

        public override void Deserialize(IDataReader reader)
        {
            OrnamentId = reader.ReadVarUShort();
        }

    }
}
