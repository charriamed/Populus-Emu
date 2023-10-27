using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.HARDI)]
    [ChallengeIdentifier((int)ChallengeEnum.CORAILLEUR_MAGISTRAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.WA_WABBIT__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.REINE_NYÉE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHOUDINI__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.DRAGON_COCHON__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.MEULOU__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.RAT_BLANC__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.RAT_NOIR__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_MULTICOLORE_ROYAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.TANUKOUÏ_SAN__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.PÉKI_PÉKI__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.SPHINCTER_CELL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.PHOSSILE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.KANIGROULA__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.BWORKER__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.VORTEX__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.LARVE_DE_KOUTOULOU__CHALLENGE_2_)]


    [ChallengeIdentifier((int)ChallengeEnum.COLLANT)]
    [ChallengeIdentifier((int)ChallengeEnum.TOURNESOL_AFFAMÉ__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.KANKREBLATH__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_BLEUE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_CITRON__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_FRAISE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_MENTHE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHOUDINI__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.MAÎTRE_CORBAC__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.MAÎTRE_PANDORE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.CROCABULIA__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.OUGAH__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.GLOURSÉLESTE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.OMBRE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.MISSIZ_FRIZZ___CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.REINE_DES_VOLEURS__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.XLII__CHALLENGE_2_)]
    public class ImpertinenceChallenge : DefaultChallenge
    {
        private readonly FightTeam m_team;

        static readonly int[] Collant =
        {
            (int) ChallengeEnum.COLLANT,
            (int) ChallengeEnum.TOURNESOL_AFFAMÉ__CHALLENGE_1_,
            (int) ChallengeEnum.KANKREBLATH__CHALLENGE_2_,
            (int) ChallengeEnum.GELÉE_ROYALE_BLEUE__CHALLENGE_2_,
            (int) ChallengeEnum.GELÉE_ROYALE_CITRON__CHALLENGE_2_,
            (int) ChallengeEnum.GELÉE_ROYALE_FRAISE__CHALLENGE_2_,
            (int) ChallengeEnum.GELÉE_ROYALE_MENTHE__CHALLENGE_2_,
            (int) ChallengeEnum.CHOUDINI__CHALLENGE_1_,
            (int) ChallengeEnum.MAÎTRE_CORBAC__CHALLENGE_1_,
            (int) ChallengeEnum.MAÎTRE_PANDORE__CHALLENGE_2_,
            (int) ChallengeEnum.CROCABULIA__CHALLENGE_1_,
            (int) ChallengeEnum.OUGAH__CHALLENGE_1_,
            (int) ChallengeEnum.GLOURSÉLESTE__CHALLENGE_1_,
            (int) ChallengeEnum.OMBRE__CHALLENGE_2_,
            (int) ChallengeEnum.MISSIZ_FRIZZ___CHALLENGE_2_,
            (int) ChallengeEnum.REINE_DES_VOLEURS__CHALLENGE_2_,
            (int) ChallengeEnum.XLII__CHALLENGE_2_,
        };

        public ImpertinenceChallenge(int id, IFight fight)
            : base(id, fight)
        {
            if (id == (int)ChallengeEnum.HARDI)
            {
                BonusMin = 25;
                BonusMax = 25;
            }
            else
            {
                BonusMin = 40;
                BonusMax = 40;
            }

            m_team = Fight.DefendersTeam is FightMonsterTeam ? Fight.DefendersTeam : Fight.ChallengersTeam;
            if (Collant.Contains(id))
                m_team = m_team.OpposedTeam;
        }

        public override void Initialize()
        {
            base.Initialize();

            Fight.BeforeTurnStopped += OnBeforeTurnStopped;
        }

        public override bool IsEligible() => m_team.GetAllFighters().Count() > 1;

        void OnBeforeTurnStopped(IFight fight, FightActor fighter)
        {
            if (!(fighter is CharacterFighter))
                return;

            if (fighter.Position.Point.GetAdjacentCells(x => m_team.GetOneFighter(Fight.Map.GetCell(x)) != null).Any())
                return;

            UpdateStatus(ChallengeStatusEnum.FAILED);
            Fight.BeforeTurnStopped -= OnBeforeTurnStopped;
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            OnBeforeTurnStopped(fight, fight.FighterPlaying);

            base.OnWinnersDetermined(fight, winners, losers, draw);

            Fight.BeforeTurnStopped -= OnBeforeTurnStopped;
        }
    }
}
