using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.WATERFALL_9747)]
    public class Cascade : DefaultSpellCastHandler
    {
        public Cascade(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                Handlers[3].Priority = 9999;
                Handlers[1].Priority = 9997;
                Handlers[2].Priority = 9998;
                Handlers[0].Priority = 0;
                if(Caster.GetCarriedActor() != null)
                    Handlers[0].AddAffectedActor(Caster.GetCarriedActor());
                return true;
            }

            return false;
        }
    }
}