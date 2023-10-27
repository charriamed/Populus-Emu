using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.Game.Spells.Cast.Sacrieur
{
    [SpellCastHandler(SpellIdEnum.FLYING_SWORD_9978)]
    public class Espada : DefaultSpellCastHandler
    {
        public Espada(SpellCastInformations cast)
          : base(cast)
        {
        }

        public override void Execute()
        {
            // No State
            //if (!Caster.HasState(488) && !Caster.HasState(489) && !Caster.HasState(490))
                Handlers[0].Apply();

            // State 488
            //else if (Caster.HasState(488))
                Handlers[1].Apply();

            // State 489
            //else if (Caster.HasState(489))
                Handlers[2].Apply();

            //else{
              //  if (!Caster.HasState(490))
                //    return;
                Handlers[3].Apply();
            }
        }

    }

