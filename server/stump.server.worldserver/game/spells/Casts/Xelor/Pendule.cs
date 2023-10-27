using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Xelor
{
    [SpellCastHandler(SpellIdEnum.PENDULUM_9901)]
    public class Pendule : DefaultSpellCastHandler
    {
        public Pendule(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                Handlers[0].Priority = 0;
                Handlers[3].Priority = 1;
                return true;
            }

            return false;
        }
    }
}
