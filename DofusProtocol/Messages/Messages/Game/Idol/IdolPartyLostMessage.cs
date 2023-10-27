namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdolPartyLostMessage : Message
    {
        public const uint Id = 6580;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort IdolId { get; set; }

        public IdolPartyLostMessage(ushort idolId)
        {
            this.IdolId = idolId;
        }

        public IdolPartyLostMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(IdolId);
        }

        public override void Deserialize(IDataReader reader)
        {
            IdolId = reader.ReadVarUShort();
        }

    }
}
