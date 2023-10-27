using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Extensions;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Handlers.Actions;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Ecaflip
{
    [SpellCastHandler(SpellIdEnum.FATE_OF_ECAFLIP_120)]
    public class FateOfEcaflipCastHandler : DefaultSpellCastHandler
    {
        public FateOfEcaflipCastHandler(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                Handlers[2].Priority = 9997;
                Handlers[0].Priority = 9998;
                Handlers[3].Priority = 9999;
                return true;
            }

            return false;
        }
    }
}