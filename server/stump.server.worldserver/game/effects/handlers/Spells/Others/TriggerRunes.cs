using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using System.Linq;
using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Others
{
    [EffectHandler(EffectsEnum.Effect_TriggerRunes)]
    public class TriggerRunes : SpellEffectHandler
    {
        public TriggerRunes(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical) : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            foreach (var trigger in Fight.GetTriggers().OfType<Rune>().Where(x => x.Caster == Caster && AffectedCells.Contains(x.CenterCell)).ToArray())
            {
                var target = Fight.GetOneFighter(trigger.CenterCell);

                if (Spell.Id == (int)SpellIdEnum.RUNIC_TREATMENT && target == null)
                    target = Caster;

                if (Spell.Id == (int)SpellIdEnum.RUNIC_REPULSION_9597)
                    target = Fight.GetOneFighter(TargetedCell);

                if (Spell.Id == (int)SpellIdEnum.RUNIC_OVERCHARGE && target == null)
                    target = Fight.GetOneFighter(TargetedCell);

                if (Spell.Id == (int)SpellIdEnum.MANIFESTATION_9579 && target == null)
                    target = Caster;

                if (Spell.Id == (int)SpellIdEnum.INVASION_9084 && target == null)
                    target = Caster.OpposedTeam.Fighters.FirstOrDefault();

                if (target == null)
                    continue;

                using (Fight.StartSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP))
                    trigger.Trigger(target);
            }

            return true;
        }
    }
}
