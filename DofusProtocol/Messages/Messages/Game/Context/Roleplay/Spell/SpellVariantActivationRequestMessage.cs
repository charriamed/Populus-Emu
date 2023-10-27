namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SpellVariantActivationRequestMessage : Message
    {
        public const uint Id = 6707;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SpellId { get; set; }

        public SpellVariantActivationRequestMessage(ushort spellId)
        {
            this.SpellId = spellId;
        }

        public SpellVariantActivationRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellId = reader.ReadVarUShort();
        }

    }
}
