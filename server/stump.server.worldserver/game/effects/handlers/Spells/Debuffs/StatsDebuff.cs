using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

using Stump.Server.WorldServer.Game.Spells.Casts;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs
{
    [EffectHandler(EffectsEnum.Effect_SubAgility)]
    [EffectHandler(EffectsEnum.Effect_SubChance)]
    [EffectHandler(EffectsEnum.Effect_SubIntelligence)]
    [EffectHandler(EffectsEnum.Effect_SubStrength)]
    [EffectHandler(EffectsEnum.Effect_SubWisdom)]
    [EffectHandler(EffectsEnum.Effect_SubVitality)]
    [EffectHandler(EffectsEnum.Effect_SubRange)]
    [EffectHandler(EffectsEnum.Effect_SubRange_135)]
    [EffectHandler(EffectsEnum.Effect_SubCriticalHit)]
    [EffectHandler(EffectsEnum.Effect_SubDamageBonus)]
    [EffectHandler(EffectsEnum.Effect_SubDamageBonusPercent)]
    [EffectHandler(EffectsEnum.Effect_SubDodge)]
    [EffectHandler(EffectsEnum.Effect_SubLock)]
    [EffectHandler(EffectsEnum.Effect_SubDodgeAPProbability)]
    [EffectHandler(EffectsEnum.Effect_SubDodgeMPProbability)]
    [EffectHandler(EffectsEnum.Effect_SubAPAttack)]
    [EffectHandler(EffectsEnum.Effect_SubMPAttack)]
    [EffectHandler(EffectsEnum.Effect_SubHealBonus)]
    [EffectHandler(EffectsEnum.Effect_SubNeutralResistPercent)]
    [EffectHandler(EffectsEnum.Effect_SubEarthResistPercent)]
    [EffectHandler(EffectsEnum.Effect_SubWaterResistPercent)]
    [EffectHandler(EffectsEnum.Effect_SubAirResistPercent)]
    [EffectHandler(EffectsEnum.Effect_SubFireResistPercent)]
    [EffectHandler(EffectsEnum.Effect_ReduceFinalDamages)]
    [EffectHandler(EffectsEnum.Effect_SubMeeleDamageDonePercent)]
    [EffectHandler(EffectsEnum.Effect_SubMeeleResistence)]
    [EffectHandler(EffectsEnum.Effect_SubRangedDamageDonePercent)]
    [EffectHandler(EffectsEnum.Effect_SubRangedResistence)]
    [EffectHandler(EffectsEnum.Effect_SubWeaponDamageDonePercent)]
    [EffectHandler(EffectsEnum.Effect_SubWeaponResistence)]
    [EffectHandler(EffectsEnum.Effect_SubSpellDamageBonusPercent)]
    [EffectHandler(EffectsEnum.Effect_SubSpellResistence)]
    [EffectHandler(EffectsEnum.Effect_SubPushDamageReduction)]
    [EffectHandler(EffectsEnum.Effect_SubPushDamageBonus)]

    public class StatsDebuff : SpellEffectHandler
    {
        public StatsDebuff(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            foreach (var actor in GetAffectedActors())
            {
                var integerEffect = GenerateEffect();

                if (integerEffect == null)
                    return false;

                if (Effect.Duration == 0)
                    continue;

                if (Effect.EffectId == EffectsEnum.Effect_SubRange)
                {
                    actor.OnFightPointsVariation(ActionsEnum.ACTION_CHARACTER_DEBOOST_RANGE, Caster, actor, (short)(-integerEffect.Value));
                    actor.TriggerBuffs(actor, BuffTriggerType.OnRangeLost);
                }

                AddStatBuff(actor, (short) (-integerEffect.Value), GetEffectCaracteristic(Effect.EffectId));
            }

            return true;
        }

        public static PlayerFields GetEffectCaracteristic(EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_SubAgility:
                    return PlayerFields.Agility;
                case EffectsEnum.Effect_SubChance:
                    return PlayerFields.Chance;
                case EffectsEnum.Effect_SubIntelligence:
                    return PlayerFields.Intelligence;
                case EffectsEnum.Effect_SubStrength:
                    return PlayerFields.Strength;
                case EffectsEnum.Effect_SubWisdom:
                    return PlayerFields.Wisdom;
                case EffectsEnum.Effect_SubRange:
                case EffectsEnum.Effect_SubRange_135:
                    return PlayerFields.Range;
                case EffectsEnum.Effect_SubCriticalHit:
                    return PlayerFields.CriticalHit;
                case EffectsEnum.Effect_SubDamageBonus:
                    return PlayerFields.DamageBonus;
                case EffectsEnum.Effect_SubDamageBonusPercent:
                    return PlayerFields.DamageBonusPercent;
                case EffectsEnum.Effect_SubDodge:
                    return PlayerFields.TackleEvade;
                case EffectsEnum.Effect_SubLock:
                    return PlayerFields.TackleBlock;
                case EffectsEnum.Effect_SubDodgeAPProbability:
                    return PlayerFields.DodgeAPProbability;
                case EffectsEnum.Effect_SubDodgeMPProbability:
                    return PlayerFields.DodgeMPProbability;
                case EffectsEnum.Effect_SubVitality:
                    return PlayerFields.Vitality;
                case EffectsEnum.Effect_SubAPAttack:
                    return PlayerFields.APAttack;
                case EffectsEnum.Effect_SubMPAttack:
                    return PlayerFields.MPAttack;
                case EffectsEnum.Effect_SubHealBonus:
                    return PlayerFields.HealBonus;
                case EffectsEnum.Effect_SubNeutralResistPercent:
                    return PlayerFields.NeutralResistPercent;
                case EffectsEnum.Effect_SubEarthResistPercent:
                    return PlayerFields.EarthResistPercent;
                case EffectsEnum.Effect_SubWaterResistPercent:
                    return PlayerFields.WaterResistPercent;
                case EffectsEnum.Effect_SubAirResistPercent:
                    return PlayerFields.AirResistPercent;
                case EffectsEnum.Effect_SubFireResistPercent:
                    return PlayerFields.FireResistPercent;
                case EffectsEnum.Effect_ReduceFinalDamages:
                    return PlayerFields.DamageMultiplicator;
                case EffectsEnum.Effect_SubMeeleDamageDonePercent:
                    return PlayerFields.MeleeDamageDonePercent;
                case EffectsEnum.Effect_SubMeeleResistence:
                    return PlayerFields.MeleeDamageReceivedPercent;
                case EffectsEnum.Effect_SubRangedDamageDonePercent:
                    return PlayerFields.RangedDamageDonePercent;
                case EffectsEnum.Effect_SubRangedResistence:
                    return PlayerFields.RangedDamageReceivedPercent;
                case EffectsEnum.Effect_SubWeaponDamageDonePercent:
                    return PlayerFields.WeaponDamageDonePercent;
                case EffectsEnum.Effect_SubWeaponResistence:
                    return PlayerFields.WeaponDamageReceivedPercent;
                case EffectsEnum.Effect_SubSpellResistence:
                    return PlayerFields.SpellDamageReceivedPercent;
                case EffectsEnum.Effect_SubSpellDamageBonusPercent:
                    return PlayerFields.SpellDamageDonePercent;
                case EffectsEnum.Effect_SubPushDamageReduction:
                    return PlayerFields.PushDamageReduction;
                case EffectsEnum.Effect_SubPushDamageBonus:
                    return PlayerFields.PushDamageBonus;
                default:
                    throw new Exception(string.Format("'{0}' has no binded caracteristic", effect));
            }
        }
    }
}