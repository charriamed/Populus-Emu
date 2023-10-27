using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Xelor
{
    [SpellCastHandler(SpellIdEnum.TEMPORAL_DUST_96)]
    public class TemporalDustCastHandler : DefaultSpellCastHandler
    {
        public TemporalDustCastHandler(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {

            Handlers[2].SetAffectedActors(Fight.Fighters.Where(x => Handlers[2].AffectedCells.Contains(x.Cell) && (x.HasState(251) || x.HasState(244))));
            Handlers[3].SetAffectedActors(new List<FightActor>());

            base.Execute();
        }
    }
}
