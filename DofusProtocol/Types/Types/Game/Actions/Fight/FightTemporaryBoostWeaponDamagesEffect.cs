namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTemporaryBoostWeaponDamagesEffect : FightTemporaryBoostEffect
    {
        public new const short Id = 211;
        public override short TypeId
        {
            get { return Id; }
        }
        public short WeaponTypeId { get; set; }

        public FightTemporaryBoostWeaponDamagesEffect(uint uid, double targetId, short turnDuration, sbyte dispelable, ushort spellId, uint effectId, uint parentBoostUid, short delta, short weaponTypeId)
        {
            this.Uid = uid;
            this.TargetId = targetId;
            this.TurnDuration = turnDuration;
            this.Dispelable = dispelable;
            this.SpellId = spellId;
            this.EffectId = effectId;
            this.ParentBoostUid = parentBoostUid;
            this.Delta = delta;
            this.WeaponTypeId = weaponTypeId;
        }

        public FightTemporaryBoostWeaponDamagesEffect() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(WeaponTypeId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            WeaponTypeId = reader.ReadShort();
        }

    }
}
