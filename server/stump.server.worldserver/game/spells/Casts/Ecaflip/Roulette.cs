using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.SPELL_STRIKE_9434)]
    public class Roulette : DefaultSpellCastHandler
    {
        public Roulette(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                Handlers[0].Priority = 0;
                return true;
            }

            return false;
        }
    }
}