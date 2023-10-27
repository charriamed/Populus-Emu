using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Cast.Sacrieur
{
    [SpellCastHandler(SpellIdEnum.PUNISHMENT_8143)]
    public class GestaoDosEstados : DefaultSpellCastHandler
    {
        public GestaoDosEstados(SpellCastInformations cast)
          : base(cast)
        {
        }

        public override void Execute()
        {
            // If life < 50%
            if (Caster.Stats.Health.Total <= Caster.Stats.Health.TotalMax / 2){
                IEnumerable<Buff> buffs = Caster.GetBuffs();
                Func<Buff, bool> func = x => x.Spell.Id == 8143;
                if (buffs.Any(func))
                    return;
                Handlers[0].AddAffectedActor(Caster);
                //Handlers[0].Apply();
            }

            // If life > 50%
            else{
                Handlers[1].AddAffectedActor(Caster);
               // Handlers[1].Apply();
            }
        }

    }
}
