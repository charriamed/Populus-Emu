using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Stats;
using Stump.Server.WorldServer.Game.Maps.Cells;
using System.Collections.Generic;
using System.Linq;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

namespace Stump.Server.WorldServer.Game.Actors.Fight
{
    public class SummonedClone : SummonedFighter
    {
        protected readonly StatsFields m_stats;

        public SummonedClone(int id, FightActor caster, Cell cell)
            : base(id, caster.Team, new List<Spell>(), caster, cell)
        {
            Caster = caster;
            Look = caster.Look.Clone();
            m_stats = new StatsFields(this);
            m_stats.InitializeFromStats(caster.Stats);
            if (Caster is CharacterFighter && (Caster as CharacterFighter).Character.BreedId == PlayableBreedEnum.Sram) Stats.Health.DamageTaken = 0;
            ResetUsedPoints();
        }

        public FightActor Caster
        {
            get;
        }

        public override ObjectPosition MapPosition => Position;

        public override string GetMapRunningFighterName() => Name;

        public override ushort Level => Caster.Level;

        public override string Name => (Caster is NamedFighter) ? ((NamedFighter)Caster).Name : "(no name)";

        public override StatsFields Stats => m_stats;

        public override GameFightFighterInformations GetGameFightFighterInformations(WorldClient client = null)
        {
            var casterInfos = Caster.GetGameFightFighterInformations();

            if (casterInfos is GameFightCharacterInformations)
            {
                var characterInfos = casterInfos as GameFightCharacterInformations;

                return new GameFightCharacterInformations(Id, casterInfos.Look, GetEntityDispositionInformations(), casterInfos.TeamId,
                    0, IsAlive(), GetGameFightMinimalStats(), MovementHistory.GetEntries(2).Select(x => x.Cell.Id).Select(x => (ushort)x).ToArray(),
                    characterInfos.Name, characterInfos.Status, characterInfos.LeagueId, characterInfos.LadderPosition, false, characterInfos.Level, characterInfos.AlignmentInfos, characterInfos.Breed, characterInfos.Sex);
            }

            return new GameFightFighterInformations(Id, casterInfos.Look, GetEntityDispositionInformations(), casterInfos.TeamId,
                0, IsAlive(), GetGameFightMinimalStats(), MovementHistory.GetEntries(2).Select(x => x.Cell.Id).Select(x => (ushort)x).ToArray());
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations() => new FightTeamMemberInformations(Id);
    }
}