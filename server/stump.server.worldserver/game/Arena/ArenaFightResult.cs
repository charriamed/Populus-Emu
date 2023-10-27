using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.PvP;
using System;
using System.Linq;
using FightLoot = Stump.Server.WorldServer.Game.Fights.Results.FightLoot;

namespace Stump.Server.WorldServer.Game.Arena
{
    public class ArenaFightResult : FightResult<CharacterFighter>
    {
        public ArenaFightResult(CharacterFighter fighter, FightOutcomeEnum outcome, FightLoot loot, int rank, bool showLoot = true)
            : base(fighter, outcome, loot)
        {
            Rank = rank;
            ShowLoot = showLoot;
        }

        public override bool CanLoot(FightTeam team) => Outcome == FightOutcomeEnum.RESULT_VICTORY && !Fighter.HasLeft() && ShowLoot;

        public int Rank
        {
            get;
        }

        public bool ShowLoot
        {
            get;
        }

        public override FightResultListEntry GetFightResultListEntry()
        {
            var amount = 0;
            ulong kamas = 0;

            if (CanLoot(Fighter.Team))
            {
                amount = Fighter.Character.ComputeWonArenaCaliston();
                kamas = Fighter.Character.ComputeWonArenaKamas();

            }

            var items = amount > 0 ? new[] { (short)12736, (short)amount } // Caliston
                                             : new short[0];

            var loot = new DofusProtocol.Types.FightLoot(items.Select(x => (uint)x).ToArray(), (ulong)kamas);

            return new FightResultPlayerListEntry((ushort)Outcome, 0, loot, Id, Alive, (ushort)Level,
                new FightResultAdditionalData[0]);
        }

        public override void Apply()
        {
            Fighter.Character.UpdateArenaProperties(Rank, Outcome == FightOutcomeEnum.RESULT_VICTORY, Fighter.Character.ArenaMode);
        }
    }
}