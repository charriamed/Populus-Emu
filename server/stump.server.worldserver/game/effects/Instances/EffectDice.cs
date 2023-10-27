using System;
using System.IO;
using Stump.Core.Threading;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;

namespace Stump.Server.WorldServer.Game.Effects.Instances
{
    [Serializable]
    public class EffectDice : EffectInteger
    {
        protected int m_diceface;
        protected int m_dicenum;

        public EffectDice()
        {
        }

        public EffectDice(EffectDice copy)
            : this(copy.Id, copy.Value, copy.DiceNum, copy.DiceFace, copy)
        {

        }

        public EffectDice(short id, int value, int dicenum, int diceface, EffectBase effect)
            : base(id, value, effect)
        {
            m_dicenum = dicenum;
            m_diceface = diceface;
        }

        public EffectDice(EffectInstanceDice effect)
            : base(effect)
        {
            m_dicenum = (int)effect.diceNum;
            m_diceface = (int)effect.diceSide;
        }

        public EffectDice(EffectsEnum effect, int value, int dicenum, int diceface)
            : this((short)effect, value, dicenum, diceface, new EffectBase())
        {

        }

        public override int ProtocoleId
        {
            get { return 73; }
        }

        public override byte SerializationIdenfitier
        {
            get { return 4; }
        }

        public int DiceNum
        {
            get { return m_dicenum; }
            set
            {
                m_dicenum = value; IsDirty = true;
            }
        }

        public int DiceFace
        {
            get { return m_diceface; }
            set
            {
                m_diceface = value; IsDirty = true;
            }
        }


        public int Min
        {
            get
            {
                var min = m_dicenum <= m_diceface ? m_dicenum : m_diceface;
                return min == 0 ? Max : min;
            }
            set
            {
                m_diceface = value;
            }
        }

        public int Max => m_dicenum >= m_diceface ? m_dicenum : m_diceface;

        public override object[] GetValues()
        {
            if (Value == 0 && DiceFace == 0)
            {
                return new object[] { Value, DiceFace, DiceNum };
            }

            return new object[] { DiceNum, DiceFace, Value };
        }

        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectDice((ushort)Id, (uint)DiceNum, (uint)DiceFace, (uint)Value);
        }

        public override EffectInstance GetEffectInstance()
        {
            return new EffectInstanceDice
            {
                effectId = (uint)Id,
                targetMask = TargetMask,
                delay = Delay,
                duration = Duration,
                group = Group,
                random = Random,
                modificator = Modificator,
                trigger = Trigger,
                triggers = Triggers,
                zoneMinSize = ZoneMinSize,
                zoneSize = ZoneSize,
                zoneShape = (uint)ZoneShape,
                zoneEfficiencyPercent = ZoneEfficiencyPercent,
                zoneMaxEfficiency = ZoneMaxEfficiency,
                value = Value,
                diceNum = (uint)DiceNum,
                diceSide = (uint)DiceFace
            };
        }

        public override EffectBase GenerateEffect(EffectGenerationContext context,
                                                  EffectGenerationType type = EffectGenerationType.Normal)
        {
            var rand = new AsyncRandom();

            var max = m_dicenum >= m_diceface ? m_dicenum : m_diceface;
            var min = m_dicenum <= m_diceface ? m_dicenum : m_diceface;

            if (min == 0)
                min = max;

            if (type == EffectGenerationType.MaxEffects && Template.Operator == "+")
                return new EffectInteger(Id, Template.Operator != "-" ? max : min, this);
            if (type == EffectGenerationType.MinEffects)
                return new EffectInteger(Id, Template.Operator != "-" ? min : max, this);

            if (min != 0)
                return new EffectInteger(Id, (short)rand.Next(min, max + 1), this);

            return max == 0 ? new EffectInteger(Id, m_value, this) : new EffectInteger(Id, max, this);
        }

        protected override void InternalSerialize(ref BinaryWriter writer)
        {
            base.InternalSerialize(ref writer);

            writer.Write(DiceNum);
            writer.Write(DiceFace);
        }

        protected override void InternalDeserialize(ref BinaryReader reader)
        {
            base.InternalDeserialize(ref reader);

            m_dicenum = reader.ReadInt32();
            m_diceface = reader.ReadInt32();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EffectDice))
                return false;
            var b = obj as EffectDice;
            return base.Equals(obj) && m_diceface == b.m_diceface && m_dicenum == b.m_dicenum;
        }

        public static bool operator ==(EffectDice a, EffectDice b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EffectDice a, EffectDice b)
        {
            return !(a == b);
        }

        public bool Equals(EffectDice other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && other.m_diceface == m_diceface && other.m_dicenum == m_dicenum;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = base.GetHashCode();
                result = (result * 397) ^ m_diceface;
                result = (result * 397) ^ m_dicenum;
                return result;
            }
        }
    }
}