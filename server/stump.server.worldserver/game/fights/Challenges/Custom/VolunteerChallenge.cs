using System.Linq;
using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.DÉSIGNÉ_VOLONTAIRE)]
    [ChallengeIdentifier((int)ChallengeEnum.KARDORIM__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.TOURNESOL_AFFAMÉ__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHAFER_RONIN__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.BWORKETTE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.COFFRE_DES_FORGERONS__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.KANNIBOUL_EBIL__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.CRAQUELEUR_LÉGENDAIRE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.DAÏGORO__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.REINE_NYÉE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.ABRAKNYDE_ANCESTRAL__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.MEULOU__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.SILF_LE_RASBOUL_MAJEUR__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.MAÎTRE_CORBAC__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.RAT_BLANC__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.ROYALMOUTH__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.MAÎTRE_PANDORE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.HAUTE_TRUCHE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHÊNE_MOU__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.KIMBO__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.MINOTOT__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.OBSIDIANTRE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.KANIGROULA__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.USH_GALESH__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.TENGU_GIVREFOUX__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.PÈRE_VER__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.KOLOSSO__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.GLOURSÉLESTE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.OMBRE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.COMTE_RAZOF__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.ROI_NIDAS__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.REINE_DES_VOLEURS__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.ANERICE_LA_SHUSHESS__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.DAZAK_MARTEGEL__CHALLENGE_1_)]
    public class VolunteerChallenge : DefaultChallenge
    {
        public VolunteerChallenge(int id, IFight fight)
            : base(id, fight)
        {
            BonusMin = 30;
            BonusMax = 60;
        }

        public override void Initialize()
        {
            base.Initialize();

            Target = Fight.GetRandomFighter<MonsterFighter>();

            foreach (var fighter in Target.Team.Fighters)
                fighter.Dead += OnDead;
        }

        public override bool IsEligible() => Fight.GetAllFighters<MonsterFighter>().Count() > 1;

        void OnDead(FightActor victim, FightActor killer)
        {
            if (victim == Target)
                UpdateStatus(ChallengeStatusEnum.SUCCESS);

            UpdateStatus(ChallengeStatusEnum.FAILED);
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            base.OnWinnersDetermined(fight, winners, losers, draw);

            foreach (var fighter in Target.Team.Fighters)
                fighter.Dead -= OnDead;
        }
    }
}
