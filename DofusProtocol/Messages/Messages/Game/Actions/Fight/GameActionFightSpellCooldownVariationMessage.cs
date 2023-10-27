namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightSpellCooldownVariationMessage : AbstractGameActionMessage
    {
        public new const uint Id = 6219;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public ushort SpellId { get; set; }
        public short Value { get; set; }

        public GameActionFightSpellCooldownVariationMessage(ushort actionId, double sourceId, double targetId, ushort spellId, short value)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.SpellId = spellId;
            this.Value = value;
        }

        public GameActionFightSpellCooldownVariationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteVarUShort(SpellId);
            writer.WriteVarShort(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            SpellId = reader.ReadVarUShort();
            Value = reader.ReadVarShort();
        }

    }
}
