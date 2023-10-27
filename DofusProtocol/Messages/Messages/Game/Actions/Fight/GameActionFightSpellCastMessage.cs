namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightSpellCastMessage : AbstractGameActionFightTargetedAbilityMessage
    {
        public new const uint Id = 1010;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SpellId { get; set; }
        public short SpellLevel { get; set; }
        public short[] PortalsIds { get; set; }

        public GameActionFightSpellCastMessage(ushort actionId, double sourceId, bool silentCast, bool verboseCast, double targetId, short destinationCellId, sbyte critical, ushort spellId, short spellLevel, short[] portalsIds)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.SilentCast = silentCast;
            this.VerboseCast = verboseCast;
            this.TargetId = targetId;
            this.DestinationCellId = destinationCellId;
            this.Critical = critical;
            this.SpellId = spellId;
            this.SpellLevel = spellLevel;
            this.PortalsIds = portalsIds;
        }

        public GameActionFightSpellCastMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(SpellId);
            writer.WriteShort(SpellLevel);
            writer.WriteShort((short)PortalsIds.Count());
            for (var portalsIdsIndex = 0; portalsIdsIndex < PortalsIds.Count(); portalsIdsIndex++)
            {
                writer.WriteShort(PortalsIds[portalsIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SpellId = reader.ReadVarUShort();
            SpellLevel = reader.ReadShort();
            var portalsIdsCount = reader.ReadUShort();
            PortalsIds = new short[portalsIdsCount];
            for (var portalsIdsIndex = 0; portalsIdsIndex < portalsIdsCount; portalsIdsIndex++)
            {
                PortalsIds[portalsIdsIndex] = reader.ReadShort();
            }
        }

    }
}
