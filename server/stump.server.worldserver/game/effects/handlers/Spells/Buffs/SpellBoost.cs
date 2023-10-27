using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs
{
    [EffectHandler(EffectsEnum.Effect_SpellBoost)]
    [EffectHandler(EffectsEnum.Effect_SpellRangeIncrease)]
    [EffectHandler(EffectsEnum.Effect_SpellObstaclesDisable)]
    [EffectHandler(EffectsEnum.Effect_ApCostReduce)]
    public class SpellBoost : SpellEffectHandler
    {
        public SpellBoost(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            foreach (var actor in GetAffectedActors())
            {
                var integerEffect = GenerateEffect();

                if (Dice.DiceNum != 9835 && (integerEffect == null || !actor.HasSpell(Dice.DiceNum)))
                    return false;

                var boostedSpell = new Spell(9835, 1);

                if (Dice.DiceNum != 9835)
                    boostedSpell = actor.GetSpell(Dice.DiceNum);



                if (Dice.DiceFace == 0 && Dice.DiceNum == 0 && Effect.EffectId == EffectsEnum.Effect_SpellObstaclesDisable)
                {
                    var chara = actor as Actors.Fight.CharacterFighter;
                    if (chara != null && actor is CharacterFighter)
                    {
                        var spells = (actor as Actors.Fight.CharacterFighter).Character.Spells.GetPlayableSpells().Where(x => x.CurrentSpellLevel.CastTestLos == true && x.CurrentSpellLevel.Range > 1 && (actor as Actors.Fight.CharacterFighter).Character.SpellsModifications.All(v => v.SpellId != x.Template.Id)).ToList();
                        var buffd = new SpellBuff(actor.PopNextBuffId(), actor, Caster, this, Spell, boostedSpell, (short)Dice.Value, false, FightDispellableEnum.DISPELLABLE_BY_DEATH, spells);
                        actor.AddBuff(buffd);
                        chara.Character.RefreshStats();
                        return true;
                    }
                }

                if (boostedSpell == null)
                    return false;

                if (Effect.EffectId == EffectsEnum.Effect_ApCostReduce && (boostedSpell.CurrentSpellLevel.ApCost - boostedSpell.ApCostReduction) <= 1) return false;

                var buff = new SpellBuff(actor.PopNextBuffId(), actor, Caster, this, Spell, boostedSpell, (short)Dice.Value, false, FightDispellableEnum.DISPELLABLE_BY_DEATH);

                actor.AddBuff(buff);
            }

            return true;
        }
    }
}