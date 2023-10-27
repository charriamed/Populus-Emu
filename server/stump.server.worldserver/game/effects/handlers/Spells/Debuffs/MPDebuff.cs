using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Handlers.Actions;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

using Stump.Server.WorldServer.Game.Spells.Casts;
namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs
{
    [EffectHandler(EffectsEnum.Effect_SubMP)]
    [EffectHandler(EffectsEnum.Effect_LostMP)]
    [EffectHandler(EffectsEnum.Effect_SubMP_Roll)]
    public class MPDebuff : SpellEffectHandler
    {
        public MPDebuff(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            foreach (var actor in GetAffectedActors())
            {
                if (IsTriggerBuff())
                {
                    AddTriggerBuff(actor, TriggerBuff);
                }
                else
                {
                    var integerEffect = GenerateEffect();

                    if (integerEffect == null)
                        return false;

                    var value = Effect.EffectId == EffectsEnum.Effect_SubMP ? integerEffect.Value : RollMP(actor, integerEffect.Value);

                    var dodged = (short) (integerEffect.Value - value);

                    if (dodged > 0)
                    {
                        ActionsHandler.SendGameActionFightDodgePointLossMessage(Fight.Clients,
                            ActionsEnum.ACTION_FIGHT_SPELL_DODGED_PM, Caster, actor, dodged);
                    }

                    actor.TriggerBuffs(actor, BuffTriggerType.OnMPAttack);

                    if (value <= 0)
                        continue;

                    actor.TriggerBuffs(actor, BuffTriggerType.OnMPLost);

                    if (Effect.Duration != 0 || Effect.Delay != 0 && Effect.EffectId != EffectsEnum.Effect_LostMP)
                    {
                        AddStatBuff(actor, (short) -value, PlayerFields.MP, (short) EffectsEnum.Effect_SubMP);
                    }
                    else
                    {
                        actor.LostMP((short)value, Caster);
                    }
                }
            }

            return true;
        }

        private void TriggerBuff(TriggerBuff buff, FightActor triggerrer, BuffTriggerType trigger, object token)
        {
            var integerEffect = GenerateEffect();

            if (integerEffect == null)
                return;

            var value = Effect.EffectId == EffectsEnum.Effect_SubMP ? integerEffect.Value : RollMP(buff.Target, integerEffect.Value);

            var dodged = (short)(integerEffect.Value - value);

            if (dodged > 0)
            {
                ActionsHandler.SendGameActionFightDodgePointLossMessage(Fight.Clients,
                    ActionsEnum.ACTION_FIGHT_SPELL_DODGED_PM, Caster, buff.Target, dodged);
            }

            buff.Target.TriggerBuffs(buff.Target, BuffTriggerType.OnMPAttack);

            if (value <= 0)
                return;

            buff.Target.TriggerBuffs(buff.Target, BuffTriggerType.OnMPLost);

            if (Effect.Duration != 0 || Effect.Delay != 0 && Effect.EffectId != EffectsEnum.Effect_LostMP)
            {
                var newBuff = AddStatBuffDirectly(buff.Target, (short)-value, PlayerFields.MP, (short)EffectsEnum.Effect_SubMP, triggerrer: triggerrer);
                if (TriggeredBuffDuration > 0)
                    newBuff.Duration = (short) TriggeredBuffDuration;
            }
            else
            {
                buff.Target.LostMP((short)value, Caster);
            }
        }

        short RollMP(FightActor actor, int maxValue)
        {
            short value = 0;

            for (var i = 0; i < maxValue && value < actor.MP; i++)
            {
                if (actor.RollMPLose(Caster, value))
                {
                    value++;
                }
            }

            return value;
        }
    }
}