using System;
using Stump.Core.Mathematics;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Database.World;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Maps.Cells.Shapes;

namespace Stump.Server.WorldServer.Game.Fights
{
    public class Damage
    {
        private int m_amount;

        public Damage(int amount)
        {
            Amount = amount;
        }

        public Damage(EffectDice effect)
        {
            BaseMaxDamages = Math.Max(effect.DiceFace, effect.DiceNum);
            BaseMinDamages = Math.Min(effect.DiceFace, effect.DiceNum);

            if (BaseMinDamages == 0)
                BaseMinDamages = BaseMaxDamages;
        }

        public Damage(EffectDice effect, EffectSchoolEnum school, FightActor source, Spell spell, Cell targetCell, Zone zone = null)
            : this(effect)
        {
            School = school;
            Source = source;
            Spell = spell;
            TargetCell = targetCell;
            Zone = zone;
            ElementId = effect.EffectElement;
        }

        public EffectSchoolEnum School
        {
            get;
            set;
        }

        public int ElementId
        {
            get;
            set;
        }

        public FightActor Source
        {
            get;
            set;
        }

        public MarkTrigger MarkTrigger
        {
            get;
            set;
        }

        public Buff Buff
        {
            get;
            set;
        }

        public int BaseMinDamages
        {
            get;
            set;
        }

        public int BaseMaxDamages
        {
            get;
            set;
        }

        public bool IsCritical
        {
            get;
            set;
        }

        public int Amount
        {
            get { return m_amount; }
            set
            {
                if (value < 0)
                    value = 0;
                m_amount = value;
                Generated = true;
            }
        }

        public bool Generated
        {
            get;
            set;
        }

        public Spell Spell
        {
            get;
            set;
        }

        public bool IgnoreDamageReduction
        {
            get;
            set;
        }

        public bool IgnoreDamageBoost
        {
            get;
            set;
        }

        public EffectGenerationType EffectGenerationType
        {
            get;
            set;
        }

        public bool ReflectedDamages
        {
            get;
            set;
        }

        public Cell TargetCell
        {
            get;
            set;
        }

        public Zone Zone
        {
            get;
            set;
        }

        public bool IsWeaponAttack => Spell == null || Spell.Id == 0;


        public void GenerateDamages()
        {
            uint AdditionalDamage = 0;
            if (Generated)
                return;

            try
            {
                if(Spell != null)
                {
                    if (School == EffectSchoolEnum.Healing)
                    {
                        AdditionalDamage = Spell.AdditionalHeal;
                    }
                    else
                    {
                        AdditionalDamage = Spell.AdditionalDamage;
                    }
                }
            }
            catch { }

            switch (EffectGenerationType)
            {
                case EffectGenerationType.MaxEffects:
                    Amount = (BaseMaxDamages + (int)AdditionalDamage);
                    break;
                case EffectGenerationType.MinEffects:
                    Amount = (BaseMinDamages + (int)AdditionalDamage);
                    break;
                default:
                    {
                        var rand = new CryptoRandom();

                        Amount = rand.Next((BaseMinDamages + (int)AdditionalDamage), (BaseMaxDamages + (int)AdditionalDamage) + 1);
                    }
                    break;
            }
        }
    }
}