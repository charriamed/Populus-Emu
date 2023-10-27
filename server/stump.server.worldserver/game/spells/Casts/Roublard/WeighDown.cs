using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Targets;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Roublard
{
    [SpellCastHandler(SpellIdEnum.WEIGH_DOWN_10091)]
    public class WeighDown : DefaultSpellCastHandler
    {
        public WeighDown(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                if(Spell.CurrentLevel == 2)
                {
                    Handlers[2].SetAffectedActors(Fight.GetAllFighters().Where(x => Caster.Position.Point.GetCellsInLine(new MapPoint(TargetedCell)).Contains(x.Cell) && !x.IsFriendlyWith(Caster)));
                }
                
                return true;
            }
            return false;
        }

        public override void Execute()
        {
            if (Spell.CurrentLevel == 1)
            {
                if (Caster is SummonedBomb)
                {
                    var actor = Caster as SummonedBomb;
                    if(actor.MonsterBombTemplate.MonsterId == 3113)
                    {
                        Handlers[0].Apply();
                    }
                    else if(actor.MonsterBombTemplate.MonsterId == 3112)
                    {
                        Handlers[1].Apply();
                    }
                    else if(actor.MonsterBombTemplate.MonsterId == 3114)
                    {
                        Handlers[2].Apply();
                    }
                    else if(actor.MonsterBombTemplate.MonsterId == 5161)
                    {
                        Handlers[2].Apply();
                    }
                    Handlers[4].Apply();
                    Handlers[5].Apply();
                }
                else
                {
                    base.Execute();
                }
            }
            else
            {
                base.Execute();
            }
        }
    }
}