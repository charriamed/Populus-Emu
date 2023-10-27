namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightDispellSpellMessage : GameActionFightDispellMessage
    {
        public new const uint Id = 6176;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SpellId { get; set; }

        public GameActionFightDispellSpellMessage(ushort actionId, double sourceId, double targetId, bool verboseCast, ushort spellId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.VerboseCast = verboseCast;
            this.SpellId = spellId;
        }

        public GameActionFightDispellSpellMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(SpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SpellId = reader.ReadVarUShort();
        }

    }
}
