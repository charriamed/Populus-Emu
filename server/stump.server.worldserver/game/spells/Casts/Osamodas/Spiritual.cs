using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Extensions;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;
using Stump.Server.WorldServer.Game.Actors.Fight;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.SPIRITUAL_LEASH_4840)]
    public class Spiritual : DefaultSpellCastHandler
    {
        public Spiritual(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            var m_affectedactors = (from character in this.Caster.Team.GetAllFighters().OfType<CharacterFighter>()
                                    where !(character.IsRevived)
                                    select character).ToList();

            if (m_affectedactors.Count() > 0)
                Handlers[0].SetAffectedActors(m_affectedactors);

            foreach (var handler in Handlers)
                handler.Apply();

        }
    }
}
