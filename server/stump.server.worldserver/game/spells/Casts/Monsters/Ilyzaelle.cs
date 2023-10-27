using System.Linq;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Extensions;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Handlers.Actions;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Ecaflip
{
    [SpellCastHandler(SpellIdEnum.KINGSGUARD_9048)]
    public class KingsGuard : DefaultSpellCastHandler
    {
        public KingsGuard(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                var cell = Caster.Map.GetCell(Caster.OpposedTeam.Fighters.RandomElementOrDefault().Position.Point.GetAdjacentCells().RandomElementOrDefault().CellId);
                foreach (var handler in Handlers)
                {
                    handler.TargetedCell = cell;
                }
                return true;
            }

            return false;
        }
    }
}