namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractFightDispellableEffect
    {
        public const short Id  = 206;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint Uid { get; set; }
        public double TargetId { get; set; }
        public short TurnDuration { get; set; }
        public sbyte Dispelable { get; set; }
        public ushort SpellId { get; set; }
        public uint EffectId { get; set; }
        public uint ParentBoostUid { get; set; }

        public AbstractFightDispellableEffect(uint uid, double targetId, short turnDuration, sbyte dispelable, ushort spellId, uint effectId, uint parentBoostUid)
        {
            this.Uid = uid;
            this.TargetId = targetId;
            this.TurnDuration = turnDuration;
            this.Dispelable = dispelable;
            this.SpellId = spellId;
            this.EffectId = effectId;
            this.ParentBoostUid = parentBoostUid;
        }

        public AbstractFightDispellableEffect() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(Uid);
            writer.WriteDouble(TargetId);
            writer.WriteShort(TurnDuration);
            writer.WriteSByte(Dispelable);
            writer.WriteVarUShort(SpellId);
            writer.WriteVarUInt(EffectId);
            writer.WriteVarUInt(ParentBoostUid);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Uid = reader.ReadVarUInt();
            TargetId = reader.ReadDouble();
            TurnDuration = reader.ReadShort();
            Dispelable = reader.ReadSByte();
            SpellId = reader.ReadVarUShort();
            EffectId = reader.ReadVarUInt();
            ParentBoostUid = reader.ReadVarUInt();
        }

    }
}
