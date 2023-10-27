namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightCloseCombatMessage : AbstractGameActionFightTargetedAbilityMessage
    {
        public new const uint Id = 6116;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort WeaponGenericId { get; set; }

        public GameActionFightCloseCombatMessage(ushort actionId, double sourceId, bool silentCast, bool verboseCast, double targetId, short destinationCellId, sbyte critical, ushort weaponGenericId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.SilentCast = silentCast;
            this.VerboseCast = verboseCast;
            this.TargetId = targetId;
            this.DestinationCellId = destinationCellId;
            this.Critical = critical;
            this.WeaponGenericId = weaponGenericId;
        }

        public GameActionFightCloseCombatMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(WeaponGenericId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            WeaponGenericId = reader.ReadVarUShort();
        }

    }
}
