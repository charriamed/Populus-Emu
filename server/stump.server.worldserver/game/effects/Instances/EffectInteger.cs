﻿using System;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;

namespace Stump.Server.WorldServer.Game.Effects.Instances
{
    [Serializable]
    public class EffectInteger : EffectBase
    {
        protected int m_value;

        public EffectInteger()
        {

        }

        public EffectInteger(EffectInteger copy)
            : this(copy.Id, copy.Value, copy)
        {

        }

        public EffectInteger(short id, int value, EffectBase effect)
            : base(id, effect)
        {
            m_value = value;
        }

        public EffectInteger(EffectsEnum id, int value)
            : this((short)id, value, new EffectBase())
        {
        }

        public EffectInteger(EffectInstanceInteger effect)
            : base(effect)
        {
            m_value = effect.value;
        }

        public override int ProtocoleId
        {
            get { return 70; }
        }

        public override byte SerializationIdenfitier
        {
            get
            {
                return 6;
            }
        }

        public int Value
        {
            get { return m_value; }
            set
            {
                m_value = value; IsDirty = true;
            }
        }

        public override object[] GetValues()
        {
            return new object[] { Value };
        }

        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectInteger((ushort)Id, (uint)Value);
        }
        public override EffectBase GenerateEffect(EffectGenerationContext context, EffectGenerationType type = EffectGenerationType.Normal)
        {
            return new EffectInteger(this);
        }
        protected override void InternalSerialize(ref System.IO.BinaryWriter writer)
        {
            base.InternalSerialize(ref writer);

            writer.Write(m_value);
        }

        protected override void InternalDeserialize(ref System.IO.BinaryReader reader)
        {
            base.InternalDeserialize(ref reader);

            m_value = reader.ReadInt32();
        }

        public bool Equals(EffectInteger other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && other.m_value == m_value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as EffectInteger);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ m_value.GetHashCode();
            }
        }

        public static bool operator ==(EffectInteger left, EffectInteger right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EffectInteger left, EffectInteger right)
        {
            return !Equals(left, right);
        }
    }
}