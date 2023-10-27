using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.History;
using Stump.Server.WorldServer.Game.Spells.Casts;
using Stump.Server.WorldServer.Handlers.Actions;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs
{
    [EffectHandler(EffectsEnum.Effect_1036)]
    public class ReduceSpellCooldown : SpellEffectHandler
    {
        public ReduceSpellCooldown(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            var spellId = Dice.DiceNum;

            foreach (var actor in GetAffectedActors())
            {
                var spell = actor.GetSpell(spellId);
                if (spell == null)
                    continue;

                var cd = actor.SpellHistory.GetSpellCooldown(spell.CurrentSpellLevel) - Dice.Value;

                if (IsTriggerBuff())
                {
                    AddTriggerBuff(actor, TriggerBuff);
                }
                else
                {
                    if (cd >= 0)
                    {
                        actor.SpellHistory.RegisterCastedSpell(new SpellHistoryEntry(actor.SpellHistory, spell.CurrentSpellLevel,
                        Caster, actor, Fight.TimeLine.RoundNumber, cd));
                        ActionsHandler.SendGameActionFightSpellCooldownVariationMessage(actor.Fight.Clients, Caster, actor, spell, (short)cd);
                    }
                }
            }

            return true;
        }

        private void TriggerBuff(TriggerBuff buff, FightActor triggerrer, BuffTriggerType trigger, object token)
        {
            var spellId = Dice.DiceNum;

            var spell = buff.Target.GetSpell(spellId);
            if (spell == null)
                return;

            var cd = buff.Target.SpellHistory.GetSpellCooldown(spell.CurrentSpellLevel) - Dice.Value;

            if(cd >= 0)
            {
                buff.Target.SpellHistory.RegisterCastedSpell(new SpellHistoryEntry(buff.Target.SpellHistory, spell.CurrentSpellLevel,
                Caster, buff.Target, Fight.TimeLine.RoundNumber, cd));
                ActionsHandler.SendGameActionFightSpellCooldownVariationMessage(buff.Target.Fight.Clients, Caster, buff.Target, spell, (short)cd);
            }
        }
    }
}