using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Sadida
{
    [SpellCastHandler(SpellIdEnum.TREE_6718)]
    public class SacrifiedDeadSpellCastHandler : DefaultSpellCastHandler
    {
        public SacrifiedDeadSpellCastHandler(SpellCastInformations cast)
            : base(cast)
        {
            
        }

        public override bool Initialize()
        {
            if (base.Initialize() && CastedByEffect != null)
            {
                Handlers[0].SetAffectedActors(CastedByEffect.GetAffectedActors());
                return true;
            }

            return false;
        }
        
    }
}