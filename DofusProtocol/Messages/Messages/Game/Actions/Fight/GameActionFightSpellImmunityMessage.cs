namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightSpellImmunityMessage : AbstractGameActionMessage
    {
        public new const uint Id = 6221;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public ushort SpellId { get; set; }

        public GameActionFightSpellImmunityMessage(ushort actionId, double sourceId, double targetId, ushort spellId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.SpellId = spellId;
        }

        public GameActionFightSpellImmunityMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteVarUShort(SpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            SpellId = reader.ReadVarUShort();
        }

    }
}
