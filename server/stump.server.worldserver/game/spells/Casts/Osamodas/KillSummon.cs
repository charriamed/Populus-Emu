using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Extensions;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Osamodas
{
    [SpellCastHandler(SpellIdEnum.CALL_TO_ORDER_37)]
    public class KillSummon : DefaultSpellCastHandler
    {
        public KillSummon(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            foreach(var handler in Handlers.Reverse())
                handler.Apply();
        }
    }
}