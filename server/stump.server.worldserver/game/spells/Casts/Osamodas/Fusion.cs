using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Extensions;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.ANIMAL_LINK_26)]
    public class Fusion : DefaultSpellCastHandler
    {
        public Fusion(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            var m_buff = Handlers.OfType<StatsBuff>();

            foreach (var handler in Handlers)
            {
                if (m_buff.Contains(handler))
                    continue;

                handler.Apply();
            }

        }
    }
}