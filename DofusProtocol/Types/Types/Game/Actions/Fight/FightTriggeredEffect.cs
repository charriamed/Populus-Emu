namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTriggeredEffect : AbstractFightDispellableEffect
    {
        public new const short Id = 210;
        public override short TypeId
        {
            get { return Id; }
        }
        public int Param1 { get; set; }
        public int Param2 { get; set; }
        public int Param3 { get; set; }
        public short Delay { get; set; }

        public FightTriggeredEffect(uint uid, double targetId, short turnDuration, sbyte dispelable, ushort spellId, uint effectId, uint parentBoostUid, int param1, int param2, int param3, short delay)
        {
            this.Uid = uid;
            this.TargetId = targetId;
            this.TurnDuration = turnDuration;
            this.Dispelable = dispelable;
            this.SpellId = spellId;
            this.EffectId = effectId;
            this.ParentBoostUid = parentBoostUid;
            this.Param1 = param1;
            this.Param2 = param2;
            this.Param3 = param3;
            this.Delay = delay;
        }

        public FightTriggeredEffect() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(Param1);
            writer.WriteInt(Param2);
            writer.WriteInt(Param3);
            writer.WriteShort(Delay);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Param1 = reader.ReadInt();
            Param2 = reader.ReadInt();
            Param3 = reader.ReadInt();
            Delay = reader.ReadShort();
        }

    }
}
