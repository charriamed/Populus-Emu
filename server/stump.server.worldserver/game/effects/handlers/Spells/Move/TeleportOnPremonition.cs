using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Handlers.Actions;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;
using Stump.Server.WorldServer.Game.Fights.Triggers;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move
{
    [EffectHandler(EffectsEnum.Effect_1101)]
    public class TeleportOnPremonition : SpellEffectHandler
    {
        public TeleportOnPremonition(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            var carryingActor = Caster.GetCarryingActor();

            var celltogo = Fight.GetTriggers().Where(x => x is Rune && (x as Rune).CastedSpell.Id == 9900).FirstOrDefault();

            if(celltogo != null)
            {
                var finalCell = celltogo.CenterCell;
                var fighter = Fight.GetOneFighter(finalCell);
                if(fighter != null && fighter != Caster)
                {
                    if (carryingActor != null)
                        return true;
                    else
                    {
                        Caster.Telefrag(Caster, fighter);
                    }
                }
                else
                {
                    if (carryingActor != null)
                        carryingActor.ThrowActor(finalCell);
                    else
                    {
                        Caster.Position.Cell = finalCell;

                        Fight.ForEach(entry => ActionsHandler.SendGameActionFightTeleportOnSameMapMessage(entry.Client, Caster, Caster, finalCell), true);
                    }
                }
            }

            return true;
        }
    }
}