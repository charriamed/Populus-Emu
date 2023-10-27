using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Roublard
{
    [SpellCastHandler(SpellIdEnum.MIMESIS_10043)]
    public class Mimesis : DefaultSpellCastHandler
    {
        public Mimesis(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if(Spell.CurrentLevel == 2)
            {
                if (Caster.HasState(637))
                {
                    Handlers[4].Apply();
                    Handlers[0].Apply();
                }
                else if (Caster.HasState(638))
                {
                    Handlers[5].Apply();
                    Handlers[1].Apply();
                }
                else if (Caster.HasState(639))
                {
                    Handlers[6].Apply();
                    Handlers[2].Apply();
                }
                else if (Caster.HasState(640))
                {
                    Handlers[7].Apply();
                    Handlers[3].Apply();
                }
            }
            else
            {
                base.Execute();
            }
        }
    }
}