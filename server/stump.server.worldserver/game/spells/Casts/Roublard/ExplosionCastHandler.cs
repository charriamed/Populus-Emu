using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Targets;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Roublard
{
    [SpellCastHandler(SpellIdEnum.ROGUISH_EXPLOSION)]
    [SpellCastHandler(SpellIdEnum.ROGUISH_DOWNPOUR)]
    [SpellCastHandler(SpellIdEnum.ROGUISH_TORNADO)]
    [SpellCastHandler(SpellIdEnum.ROGUISH_AVALANCHE)]
    public class ExplosionCastHandler : DefaultSpellCastHandler
    {
        public ExplosionCastHandler(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                // FIX BOMBS BOOST
                foreach (var handler in Handlers.Where(x => x.Targets.OfType<StateCriterion>().Any(y => y.State == (int)SpellStatesEnum.KABOOM_92)))
                {
                    handler.Efficiency = 1;
                    handler.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_STRONG_DISPEL;
                }

                // FIX SUB AP/MP
                foreach(var handler in Handlers.Where(x => x.Effect.EffectId == EffectsEnum.Effect_SubMP_Roll || x.Effect.EffectId == EffectsEnum.Effect_SubAP_Roll))
                {
                    handler.Efficiency = 1;
                }

                return true;
            }
            return false;
        }
    }
}