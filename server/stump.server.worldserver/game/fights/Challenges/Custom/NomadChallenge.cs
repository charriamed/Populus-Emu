using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.NOMADE)]
    [ChallengeIdentifier((int)ChallengeEnum.CHAFER_RONIN__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.COFFRE_DES_FORGERONS__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.ABRAKNYDE_ANCESTRAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHOUQUE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.KRALAMOUR_GÉANT__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.MISSIZ_FRIZZ__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.KLIME__CHALLENGE_2_)]

    [ChallengeIdentifier((int)ChallengeEnum.PÉTULANT)]
    public class NomadChallenge : DefaultChallenge
    {

        static readonly int[] Nomade =
        {
            (int)ChallengeEnum.NOMADE,
            (int)ChallengeEnum.CHAFER_RONIN__CHALLENGE_1_,
            (int)ChallengeEnum.COFFRE_DES_FORGERONS__CHALLENGE_1_,
            (int)ChallengeEnum.ABRAKNYDE_ANCESTRAL__CHALLENGE_1_,
            (int)ChallengeEnum.CHOUQUE__CHALLENGE_2_,
            (int)ChallengeEnum.KRALAMOUR_GÉANT__CHALLENGE_1_,
            (int)ChallengeEnum.MISSIZ_FRIZZ__CHALLENGE_1_,
            (int)ChallengeEnum.KLIME__CHALLENGE_2_,
        };

        public NomadChallenge(int id, IFight fight)
            : base(id, fight)
        {
            if (Nomade.Contains(id))
            {
                BonusMin = 20;
                BonusMax = 55;
            }
            else
            {
                BonusMin = 10;
                BonusMax = 10;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            Fight.BeforeTurnStopped += OnTurnStopped;
            Fight.Tackled += OnTackled;
        }

        private void OnTackled(FightActor fighter, int apTackled, int mpTackled)
        {
            if (!(fighter is CharacterFighter))
                return;

            Fight.Tackled -= OnTackled;
            UpdateStatus(ChallengeStatusEnum.FAILED, fighter);
        }

        private void OnTurnStopped(IFight fight, FightActor fighter)
        {
            if (fighter.IsDead())
                return;

            if (!(fighter is CharacterFighter))
                return;

            if (Nomade.Contains(Id) && fighter.MP <= 0)
                return;

            if (Id == (int)ChallengeEnum.PÉTULANT && fighter.AP <= 0)
                return;

            UpdateStatus(ChallengeStatusEnum.FAILED);
            Fight.BeforeTurnStopped -= OnTurnStopped;
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            OnTurnStopped(fight, fight.FighterPlaying);

            base.OnWinnersDetermined(fight, winners, losers, draw);

            Fight.BeforeTurnStopped -= OnTurnStopped;
            Fight.Tackled -= OnTackled;
        }
    }
}
