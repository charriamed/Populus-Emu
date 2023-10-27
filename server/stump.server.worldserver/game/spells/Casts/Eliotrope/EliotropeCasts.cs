using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Ecaflip
{

    [SpellCastHandler(SpellIdEnum.STUPOR_5386)]
    public class StuporSpawn : DefaultSpellCastHandler
    {
        public StuporSpawn(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            var portalout = Fight.GetTriggers().FirstOrDefault(x => x is Portal && x.ContainsCell(TargetedCell)) as Portal;
            //break;
            if (!m_initialized)
                Initialize();
            EffectDice subap = new EffectDice((short)EffectsEnum.Effect_Trap, 0, 5359, 1, new EffectBase());
            SpellEffectHandler spellEffectHandler = Singleton<EffectManager>.Instance.GetSpellEffectHandler(subap, base.Caster, this, base.TargetedCell, false);
            spellEffectHandler.Dice.Value = 1;
            spellEffectHandler.Apply();
        }
    }

    [SpellCastHandler(5371)]
    public class Etire : DefaultSpellCastHandler
    {
        public Etire(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();
            var boostedSpell = Caster.GetSpell(5359);
            Handlers[0].Apply();
            if (boostedSpell != null)
            {
                var targer = Handlers[0].GetAffectedActors().First();
                var buff = new SpellBuff(targer.PopNextBuffId(), targer, Caster, Handlers[1], Spell, boostedSpell, 3, false, FightDispellableEnum.DISPELLABLE_BY_DEATH);
                ContextHandler.ModifySpellRange(Fight.Clients, buff);
                boostedSpell.CurrentSpellLevel.Range = 7;
            }
            foreach (var handler in Handlers)
            {
                if (handler != Handlers[0])
                    handler.Apply();
            }
        }
    }
}