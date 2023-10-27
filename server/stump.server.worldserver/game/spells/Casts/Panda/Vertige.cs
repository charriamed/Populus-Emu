using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Panda
{
    [SpellCastHandler(SpellIdEnum.VERTIGO_694)]
    public class Vertige : DefaultSpellCastHandler
    {
        public Vertige(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            var m_affectedactors = Caster.GetCarriedActor();

            if (m_affectedactors != null)
            {
                Handlers[0].AddAffectedActor(m_affectedactors);
                Handlers[0].Priority = 1;
                if(m_affectedactors.IsFriendlyWith(Caster))
                    Handlers[2].AddAffectedActor(m_affectedactors);
                Handlers[2].Priority = 0;
                Handlers[1].Priority = 2;
            }

            foreach (var handler in Handlers.OrderBy(x => x.Priority))
            {
                handler.Apply();
            }
        }
    }
}
