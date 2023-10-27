namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTemporarySpellBoostEffect : FightTemporaryBoostEffect
    {
        public new const short Id = 207;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort BoostedSpellId { get; set; }

        public FightTemporarySpellBoostEffect(uint uid, double targetId, short turnDuration, sbyte dispelable, ushort spellId, uint effectId, uint parentBoostUid, short delta, ushort boostedSpellId)
        {
            this.Uid = uid;
            this.TargetId = targetId;
            this.TurnDuration = turnDuration;
            this.Dispelable = dispelable;
            this.SpellId = spellId;
            this.EffectId = effectId;
            this.ParentBoostUid = parentBoostUid;
            this.Delta = delta;
            this.BoostedSpellId = boostedSpellId;
        }

        public FightTemporarySpellBoostEffect() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(BoostedSpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            BoostedSpellId = reader.ReadVarUShort();
        }

    }
}
