using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.ELEMENTAL_SHIELD_6229)]
    public class ElementalShield : DefaultSpellCastHandler
    {
        public ElementalShield(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            foreach (var handler in Handlers.OrderBy(x => x.Priority))
            {
                var newTarget = Caster.OpposedTeam.Fighters.FirstOrDefault(x => Caster.LastAttacker == x);
                handler.SetAffectedActors(new[] { newTarget });
                handler.Apply();
            }
        }
    }
}