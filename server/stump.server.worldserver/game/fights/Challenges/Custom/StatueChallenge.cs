using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.STATUE)]
    [ChallengeIdentifier((int)ChallengeEnum.BATOFU__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BWORKETTE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.MOON__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.MAÎTRE_DES_PANTINS__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.POUNICHEUR__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.TOFU_ROYAL__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.SKEUNK__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.CAPITAINE_EKARLATTE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.PÉKI_PÉKI__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BEN_LE_RIPATE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.KIMBO__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.OBSIDIANTRE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.TENGU_GIVREFOUX__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.FUJI_GIVREFOUX_NOURRICIÈRE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.HAREBOURG__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.ROI_NIDAS__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.PROTOZORREUR__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.DANTINEA__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.TAL_KASHA__CHALLENGE_2_)]
    public class StatueChallenge : DefaultChallenge
    {
        public StatueChallenge(int id, IFight fight)
            : base(id, fight)
        {
            BonusMin = 25;
            BonusMax = 55;
        }

        public override void Initialize()
        {
            base.Initialize();

            Fight.TurnStopped += OnTurnStopped;
        }

        void OnTurnStopped(IFight fight, FightActor fighter)
        {
            if (!(fighter is CharacterFighter))
                return;

            if (fighter.Position?.Cell.Id == fighter.TurnStartPosition?.Cell.Id)
                return;

            UpdateStatus(ChallengeStatusEnum.FAILED);

            Fight.TurnStopped -= OnTurnStopped;
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            OnTurnStopped(fight, fight.FighterPlaying);

            base.OnWinnersDetermined(fight, winners, losers, draw);

            Fight.TurnStopped -= OnTurnStopped;
        }
    }
}
