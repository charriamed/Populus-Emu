using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.ABRAKNYDE_ANCESTRAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BATOFU__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BEN_LE_RIPATE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_COCO_ROYAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_GRIOTTE_ROYAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_INDIGO_ROYAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_MULTICOLORE_ROYAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_REINETTE_ROYAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BOOSTACHE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BOUFTOU_ROYAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BULBIG_BROZEUR__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BWORKER__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BWORKETTE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHAFER_RONIN__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CAPITAINE_EKARLATTE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHÊNE_MOU__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.COFFRE_DES_FORGERONS__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CORAILLEUR_MAGISTRAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CRAQUELEUR_LÉGENDAIRE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CROCABULIA__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.DAÏGORO__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.DRAGON_COCHON__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.FRAKTALE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.FUJI_GIVREFOUX_NOURRICIÈRE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_BLEUE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_CITRON__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_FRAISE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_MENTHE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GLOURSÉLESTE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GOURLO_LE_TERRIBLE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GROLLOUM__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GROZILLA_ET_GRASMERA_FATIGUÉS__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GROZILLA_ET_GRASMERA_SOMNAMBULES__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GROZILLA_ET_GRASMERA__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.GROZILLA_ET_GRASMERA_ÉPUISÉS__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.HALOUINE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.HAREBOURG__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.HAUTE_TRUCHE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KANIGROULA__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KANKREBLATH__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KANNIBOUL_EBIL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KARDORIM__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KIMBO__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KLIME__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KOLOSSO__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KORRIANDRE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KOULOSSE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KRALAMOUR_GÉANT__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.KWAKWA__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MALLÉFISK__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MANSOT_ROYAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MAÎTRE_CORBAC__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MAÎTRE_DES_PANTINS__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MAÎTRE_PANDORE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MERKATOR__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MEULOU__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MINOTOROR__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MINOTOT__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MISSIZ_FRIZZ__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MOB_L_EPONGE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MOON__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.NELWEEN__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.NILEZA__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.OBSIDIANTRE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.OMBRE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.OUGAH__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.PAPA_NOWEL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.PHOSSILE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.PROTOZORREUR__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.PÈRE_FWETAR__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.PÉKI_PÉKI__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.RAT_BLANC__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.RAT_NOIR__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.REINE_NYÉE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.ROYALMOUTH__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.SAPIK__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.SCARABOSSE_DORÉ__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.SHIN_LARVE__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.SILF_LE_RASBOUL_MAJEUR__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.SKEUNK__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.SPHINCTER_CELL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.SYLARGH__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.TANUKOUÏ_SAN__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.TENGU_GIVREFOUX__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.TOFU_ROYAL__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.TOURNESOL_AFFAMÉ__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.TOXOLIATH__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.TYNRIL_AHURI__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.WA_WABBIT__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.WA_WOBOT__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.XLII__CHALLENGE_DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.DAZAK_MARTEGEL__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.BETHEL_AKARNA__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.SOLAR__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.ILYZAELLE__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.ANERICE_LA_SHUSHESS__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHOUDINI__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.COMTE_RAZOF__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.TAL_KASHA__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.PÈRE_VER__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.EL_PIKO__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.MANTISCORE__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.DANTINEA__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.LARVE_DE_KOUTOULOU__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CAPITAINE_MENO__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.USH_GALESH__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.POUNICHEUR__DUO_)]
    [ChallengeIdentifier((int)ChallengeEnum.CHOUQUE__DUO_)]

    [ChallengeIdentifier((int)ChallengeEnum.CHALOEIL__TRIO_)]
    [ChallengeIdentifier((int)ChallengeEnum.REINE_DES_VOLEURS__CHALLENGE_TRIO_)]
    [ChallengeIdentifier((int)ChallengeEnum.ROI_NIDAS__CHALLENGE_TRIO_)]
    [ChallengeIdentifier((int)ChallengeEnum.VORTEX__CHALLENGE_TRIO_)]
    public class DuoChallenge : DefaultChallenge
    {
        private static int[] Trio =
        {
            (int)ChallengeEnum.CHALOEIL__TRIO_,
            (int)ChallengeEnum.REINE_DES_VOLEURS__CHALLENGE_TRIO_,
            (int)ChallengeEnum.ROI_NIDAS__CHALLENGE_TRIO_,
            (int)ChallengeEnum.VORTEX__CHALLENGE_TRIO_,
        };

        public DuoChallenge(int id, IFight fight)
            : base(id, fight)
        {
            BonusMin = 0;
            BonusMax = 0;
        }

        public override void Initialize()
        {
            base.Initialize();

            Fight.FightStarted += OnFightStarted;
        }

        void OnFightStarted(IFight fight)
        {
            if (Trio.Contains(Id))
            {
                if (fight.Fighters.OfType<CharacterFighter>().Count() > 3)
                    UpdateStatus(ChallengeStatusEnum.FAILED);
            }
            else
            {
                if (fight.Fighters.OfType<CharacterFighter>().Count() > 2)
                    UpdateStatus(ChallengeStatusEnum.FAILED);
            }
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            base.OnWinnersDetermined(fight, winners, losers, draw);

            Fight.FightStarted -= OnFightStarted;
        }
    }
}
