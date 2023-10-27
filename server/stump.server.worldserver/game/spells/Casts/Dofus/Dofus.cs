using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Spells.Casts
{
    [SpellCastHandler(9089)]
    public class DofusIvoire2 : DefaultSpellCastHandler
    {
        public DofusIvoire2(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {

            foreach (var handler in Handlers)
            {
                handler.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                handler.Apply();
            }
        }
    }

    [SpellCastHandler(6828)]
    public class Aby : DefaultSpellCastHandler
    {
        public Aby(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (Fight.TimeLine.RoundNumber == 1 && !Caster.Abyssal)
            {
                Caster.Abyssal = true;
                EffectDice dice = new EffectDice(EffectsEnum.Effect_AddMP, 1, 0, 0);
                var cells = Caster.Position.Point.GetAdjacentCells();
                foreach (var cell in cells)
                {
                    var f = Fight.GetOneFighter(Map.GetCell(cell.CellId));
                    if (f != null)
                    {
                        if (f.IsEnnemyWith(Caster)) dice = new EffectDice(EffectsEnum.Effect_AddAP_111, 1, 0, 0);
                    }
                }
                var hand = EffectManager.Instance.GetSpellEffectHandler(dice, Caster, this, Caster.Cell, false);
                hand.DefaultDispellableStatus = FightDispellableEnum.REALLY_NOT_DISPELLABLE; // tocheck
                hand.Apply();
            }
            foreach (var handler in Handlers)
            {
                handler.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                handler.Apply();
            }
        }
    }
}