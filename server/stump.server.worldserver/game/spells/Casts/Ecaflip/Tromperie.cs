using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.TRICKERY)]
    public class Tromperie : DefaultSpellCastHandler
    {
        public Tromperie(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                Caster.RemoveSpellBuffs(9446);
                return true;
            }

            return false;
        }
    }
}