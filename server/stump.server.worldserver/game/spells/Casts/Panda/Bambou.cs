using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.BAMBOO_SHERPA)]
    public class Bambou : DefaultSpellCastHandler
    {
        public Bambou(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override bool Initialize()
        {
            if (base.Initialize())
            {
                if(Spell.CurrentLevel == 2)
                {
                    Handlers[0].AddAffectedActor(Caster);
                    Handlers[1].AddAffectedActor(Caster);
                    Handlers[2].AddAffectedActor(Caster);
                }
                return true;
            }

            return false;
        }
    }
}