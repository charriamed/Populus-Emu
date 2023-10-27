using System.Linq;
using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.ANACHORÈTE)]
    [ChallengeIdentifier((int)ChallengeEnum.MOB_L_EPONGE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BOOSTACHE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BWORKETTE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BULBIG_BROZEUR__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.NELWEEN__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.CRAQUELEUR_LÉGENDAIRE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.DAÏGORO__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.MINOTOROR__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.FRAKTALE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.TOXOLIATH__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.SYLARGH__CHALLENGE_1_)]

    [ChallengeIdentifier((int)ChallengeEnum.PUSILLANIME)]
    [ChallengeIdentifier((int)ChallengeEnum.BOUFTOU_ROYAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.SHIN_LARVE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_COCO_ROYAL__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_GRIOTTE_ROYAL__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_INDIGO_ROYAL__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_REINETTE_ROYAL__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.DAÏGORO__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.KLIME__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.NILEZA__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHALOEIL__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.CAPITAINE_MENO__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.LARVE_DE_KOUTOULOU__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.ANERICE_LA_SHUSHESS__CHALLENGE_1_)]
    public class HermitChallenge : DefaultChallenge
    {
        readonly FightTeam m_team;

        static readonly int[] Anacho =
        {
            (int)ChallengeEnum.ANACHORÈTE,
            (int)ChallengeEnum.MOB_L_EPONGE__CHALLENGE_1_,
            (int)ChallengeEnum.BOOSTACHE__CHALLENGE_1_,
            (int)ChallengeEnum.BWORKETTE__CHALLENGE_1_,
            (int)ChallengeEnum.BULBIG_BROZEUR__CHALLENGE_1_,
            (int)ChallengeEnum.NELWEEN__CHALLENGE_2_,
            (int)ChallengeEnum.CRAQUELEUR_LÉGENDAIRE__CHALLENGE_1_,
            (int)ChallengeEnum.DAÏGORO__CHALLENGE_1_,
            (int)ChallengeEnum.MINOTOROR__CHALLENGE_1_,
            (int)ChallengeEnum.FRAKTALE__CHALLENGE_1_,
            (int)ChallengeEnum.TOXOLIATH__CHALLENGE_2_,
            (int)ChallengeEnum.SYLARGH__CHALLENGE_1_,
        };

        public HermitChallenge(int id, IFight fight)
            : base(id, fight)
        {
            if (id == (int)ChallengeEnum.ANACHORÈTE)
            {
                BonusMin = 20;
                BonusMax = 30;
            }
            else
            {
                BonusMin = 30;
                BonusMax = 30;  
            }

            m_team = Fight.DefendersTeam is FightMonsterTeam ? Fight.DefendersTeam : Fight.ChallengersTeam;
            if (Anacho.Contains(id))
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
            if (!(fighter is CharacterFighter) || fighter.IsDead())
                return;

            if (!fighter.Position.Point.GetAdjacentCells(x => m_team.GetOneFighter(y => y.IsAlive() && y.Cell == Fight.Map.GetCell(x)) != null).Any())
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
