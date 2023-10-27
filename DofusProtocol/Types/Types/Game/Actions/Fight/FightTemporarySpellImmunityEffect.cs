namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTemporarySpellImmunityEffect : AbstractFightDispellableEffect
    {
        public new const short Id = 366;
        public override short TypeId
        {
            get { return Id; }
        }
        public int ImmuneSpellId { get; set; }

        public FightTemporarySpellImmunityEffect(uint uid, double targetId, short turnDuration, sbyte dispelable, ushort spellId, uint effectId, uint parentBoostUid, int immuneSpellId)
        {
            this.Uid = uid;
            this.TargetId = targetId;
            this.TurnDuration = turnDuration;
            this.Dispelable = dispelable;
            this.SpellId = spellId;
            this.EffectId = effectId;
            this.ParentBoostUid = parentBoostUid;
            this.ImmuneSpellId = immuneSpellId;
        }

        public FightTemporarySpellImmunityEffect() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(ImmuneSpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ImmuneSpellId = reader.ReadInt();
        }

    }
}
