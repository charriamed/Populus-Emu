using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Handlers.Actions;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Steals
{
    [EffectHandler(EffectsEnum.Effect_StealHPWater)]
    [EffectHandler(EffectsEnum.Effect_StealHPEarth)]
    [EffectHandler(EffectsEnum.Effect_StealHPAir)]
    [EffectHandler(EffectsEnum.Effect_StealHPFire)]
    [EffectHandler(EffectsEnum.Effect_StealHPNeutral)]
    [EffectHandler(EffectsEnum.Effect_StealHPDamage)]
    public class HPSteal : SpellEffectHandler
    {
        public HPSteal(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            foreach (var actor in GetAffectedActors())
            {
                if (IsBuff())
                    AddTriggerBuff(actor, StealHpBuffTrigger);
                else
                    StealHp(actor);
            }

            return true;
        }

        private void NotifySpellReflected(FightActor source)
        {
            ActionsHandler.SendGameActionFightReflectSpellMessage(Fight.Clients, Caster, source);
        }

        void StealHp(FightActor target)
        {
            // spell reflected
            var buff = target.GetBestReflectionBuff();
            if (buff != null && buff.ReflectedLevel >= Spell.CurrentLevel && Spell.Template.Id != 0)
            {
                var school = GetEffectSchool(Effect.EffectId);

                if (Dice.EffectId == EffectsEnum.Effect_StealHPDamage)
                    school = FindBestSchool(target, true);

                var damage = new Fights.Damage(Dice, school, Caster, Spell, TargetedCell, EffectZone) { IsCritical = Critical };

                NotifySpellReflected(target);
                damage.Source = Caster;
                damage.ReflectedDamages = true;
                Caster.InflictDamage(damage);

                if (buff.Duration <= 0)
                    target.RemoveBuff(buff);
            }
            else
            {
                var school = GetEffectSchool(Effect.EffectId);

                if (Dice.EffectId == EffectsEnum.Effect_StealHPDamage)
                    school = FindBestSchool(target);
                

                var damage = new Fights.Damage(Dice, school, Caster, Spell, TargetedCell, EffectZone) { IsCritical = Critical };

                target.InflictDamage(damage);
                
                var amount = (short)Math.Floor(damage.Amount / 2.0) ;
                if (amount > 0)
                    Caster.Heal(amount, target, true);
            }
        }

        void StealHpBuffTrigger(TriggerBuff buff, FightActor triggerrer, BuffTriggerType trigger, object token)
        {
            StealHp(buff.Target);
        }

        static EffectSchoolEnum GetEffectSchool(EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_StealHPWater:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_StealHPEarth:
                    return EffectSchoolEnum.Earth;
                case EffectsEnum.Effect_StealHPAir:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_StealHPFire:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_StealHPNeutral:
                    return EffectSchoolEnum.Neutral;
                case EffectsEnum.Effect_StealHPDamage:
                    return EffectSchoolEnum.Unknown;
                default:
                    throw new Exception(string.Format("Effect {0} has not associated School Type", effect));
            }
        }

        private EffectSchoolEnum FindBestSchool(FightActor actor, bool reflect = false)
        {
            //TEST BEST DAMAGE
            var neutre = new Fights.Damage(Dice, EffectSchoolEnum.Neutral, Caster, Spell, TargetedCell, EffectZone)
            {
                MarkTrigger = MarkTrigger,
                IsCritical = Critical
            };
            neutre.GenerateDamages();
            neutre.Amount = (short)(neutre.Amount * Efficiency);

            var terre = new Fights.Damage(Dice, EffectSchoolEnum.Earth, Caster, Spell, TargetedCell, EffectZone)
            {
                MarkTrigger = MarkTrigger,
                IsCritical = Critical
            };
            terre.GenerateDamages();
            terre.Amount = (short)(terre.Amount * Efficiency);

            var air = new Fights.Damage(Dice, EffectSchoolEnum.Air, Caster, Spell, TargetedCell, EffectZone)
            {
                MarkTrigger = MarkTrigger,
                IsCritical = Critical
            };
            air.GenerateDamages();
            air.Amount = (short)(air.Amount * Efficiency);

            var feu = new Fights.Damage(Dice, EffectSchoolEnum.Fire, Caster, Spell, TargetedCell, EffectZone)
            {
                MarkTrigger = MarkTrigger,
                IsCritical = Critical
            };
            feu.GenerateDamages();
            feu.Amount = (short)(feu.Amount * Efficiency);

            var eau = new Fights.Damage(Dice, EffectSchoolEnum.Water, Caster, Spell, TargetedCell, EffectZone)
            {
                MarkTrigger = MarkTrigger,
                IsCritical = Critical
            };
            eau.GenerateDamages();
            eau.Amount = (short)(eau.Amount * Efficiency);

            bool isMelee = neutre.Source.Position.Point.ManhattanDistanceTo(actor.Position.Point) <= 1;

            List<Fights.Damage> Damages = new List<Fights.Damage>();

            Damages.Add(neutre.Source.CalculateDamageBonuses(neutre, isMelee));
            Damages.Add(terre.Source.CalculateDamageBonuses(terre, isMelee));
            Damages.Add(air.Source.CalculateDamageBonuses(air, isMelee));
            Damages.Add(feu.Source.CalculateDamageBonuses(feu, isMelee));
            Damages.Add(eau.Source.CalculateDamageBonuses(eau, isMelee));

            if (reflect)
                return Damages.OrderBy(x => x.Amount).FirstOrDefault().School;

            return Damages.OrderByDescending(x => x.Amount).FirstOrDefault().School;
        }
    }
}