namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlaySpellAnimMessage : Message
    {
        public const uint Id = 6114;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong CasterId { get; set; }
        public ushort TargetCellId { get; set; }
        public ushort SpellId { get; set; }
        public short SpellLevel { get; set; }

        public GameRolePlaySpellAnimMessage(ulong casterId, ushort targetCellId, ushort spellId, short spellLevel)
        {
            this.CasterId = casterId;
            this.TargetCellId = targetCellId;
            this.SpellId = spellId;
            this.SpellLevel = spellLevel;
        }

        public GameRolePlaySpellAnimMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(CasterId);
            writer.WriteVarUShort(TargetCellId);
            writer.WriteVarUShort(SpellId);
            writer.WriteShort(SpellLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            CasterId = reader.ReadVarULong();
            TargetCellId = reader.ReadVarUShort();
            SpellId = reader.ReadVarUShort();
            SpellLevel = reader.ReadShort();
        }

    }
}
