namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightTriggerGlyphTrapMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5741;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short MarkId { get; set; }
        public ushort MarkImpactCell { get; set; }
        public double TriggeringCharacterId { get; set; }
        public ushort TriggeredSpellId { get; set; }

        public GameActionFightTriggerGlyphTrapMessage(ushort actionId, double sourceId, short markId, ushort markImpactCell, double triggeringCharacterId, ushort triggeredSpellId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.MarkId = markId;
            this.MarkImpactCell = markImpactCell;
            this.TriggeringCharacterId = triggeringCharacterId;
            this.TriggeredSpellId = triggeredSpellId;
        }

        public GameActionFightTriggerGlyphTrapMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(MarkId);
            writer.WriteVarUShort(MarkImpactCell);
            writer.WriteDouble(TriggeringCharacterId);
            writer.WriteVarUShort(TriggeredSpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MarkId = reader.ReadShort();
            MarkImpactCell = reader.ReadVarUShort();
            TriggeringCharacterId = reader.ReadDouble();
            TriggeredSpellId = reader.ReadVarUShort();
        }

    }
}
