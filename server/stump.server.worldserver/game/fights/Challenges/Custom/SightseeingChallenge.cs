using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.Stats;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int) ChallengeEnum.PERDU_DE_VUE)]

    // Liberty criterion
    [ChallengeIdentifier((int) ChallengeEnum.CHÊNE_MOU__CHALLENGE_1_)]
    [ChallengeIdentifier((int) ChallengeEnum.PHOSSILE__CHALLENGE_2_)]
    [ChallengeIdentifier((int) ChallengeEnum.COMTE_RAZOF__CHALLENGE_1_)]
    [ChallengeIdentifier((int) ChallengeEnum.CHALOEIL__CHALLENGE_1_)]
    [ChallengeIdentifier((int) ChallengeEnum.ILYZAELLE__CHALLENGE_1_)]
    [ChallengeIdentifier((int) ChallengeEnum.DAZAK_MARTEGEL__CHALLENGE_1_)]
    public class SightseeingChallenge : DefaultChallenge
    {
        public SightseeingChallenge(int id, IFight fight)
            : base(id, fight)
        {
            BonusMin = 15;
            BonusMax = 15;
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var fighter in Fight.GetAllFighters<MonsterFighter>())
                fighter.Stats[PlayerFields.Range].Modified += OnRangeModified;
        }

        public override bool IsEligible()
            => Fight.GetAllCharacters().Any(x => x.BreedId == PlayableBreedEnum.Enutrof ||
                                                 x.BreedId == PlayableBreedEnum.Cra);

        void OnRangeModified(StatsData stats, int amount)
        {
            if (amount >= 0)
                return;

            stats.Owner.Stats[PlayerFields.Range].Modified -= OnRangeModified;
            UpdateStatus(ChallengeStatusEnum.FAILED);
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            base.OnWinnersDetermined(fight, winners, losers, draw);

            foreach (var fighter in Fight.GetAllFighters<MonsterFighter>())
                fighter.Stats[PlayerFields.Range].Modified -= OnRangeModified;
        }
    }
}
