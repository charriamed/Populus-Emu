namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SpellVariantActivationMessage : Message
    {
        public const uint Id = 6705;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SpellId { get; set; }
        public bool Result { get; set; }

        public SpellVariantActivationMessage(ushort spellId, bool result)
        {
            this.SpellId = spellId;
            this.Result = result;
        }

        public SpellVariantActivationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SpellId);
            writer.WriteBoolean(Result);
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellId = reader.ReadVarUShort();
            Result = reader.ReadBoolean();
        }

    }
}
