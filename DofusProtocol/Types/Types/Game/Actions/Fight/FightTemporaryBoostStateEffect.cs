namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTemporaryBoostStateEffect : FightTemporaryBoostEffect
    {
        public new const short Id = 214;
        public override short TypeId
        {
            get { return Id; }
        }
        public short StateId { get; set; }

        public FightTemporaryBoostStateEffect(uint uid, double targetId, short turnDuration, sbyte dispelable, ushort spellId, uint effectId, uint parentBoostUid, short delta, short stateId)
        {
            this.Uid = uid;
            this.TargetId = targetId;
            this.TurnDuration = turnDuration;
            this.Dispelable = dispelable;
            this.SpellId = spellId;
            this.EffectId = effectId;
            this.ParentBoostUid = parentBoostUid;
            this.Delta = delta;
            this.StateId = stateId;
        }

        public FightTemporaryBoostStateEffect() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(StateId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            StateId = reader.ReadShort();
        }

    }
}
