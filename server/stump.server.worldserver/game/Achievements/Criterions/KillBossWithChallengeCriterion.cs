using System;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Achievements.Criterions.Data;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Conditions;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions
{
    public class KillBossWithChallengeCriterion :
        AbstractCriterion<KillBossWithChallengeCriterion, DefaultCriterionData>
    {
        // FIELDS
        public const string Identifier = "EH";
        private ushort? m_maxValue;

        // CONSTRUCTORS
        public KillBossWithChallengeCriterion(AchievementObjectiveRecord objective)
            : base(objective)
        {
            var monsterId = GetMonsterIdByChallId(ChallIdentifier);
            if (monsterId != 0)
                Monster = Singleton<MonsterManager>.Instance.GetTemplate(monsterId);
        }

        // PROPERTIES
        public int ChallIdentifier => this[0][0];

        public MonsterTemplate Monster { get; }

        public int Number => this[0][1];

        public override bool IsIncrementable => false;

        public int GetMonsterIdByChallId(int id)
        {
            switch (id)
            {
                case (int)ChallengeEnum.KARDORIM__CHALLENGE_1_:
                case (int)ChallengeEnum.KARDORIM__CHALLENGE_2_:
                case (int)ChallengeEnum.KARDORIM__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KARDORIM;

                case (int)ChallengeEnum.TOURNESOL_AFFAMÉ__CHALLENGE_1_:
                case (int)ChallengeEnum.TOURNESOL_AFFAMÉ__CHALLENGE_2_:
                case (int)ChallengeEnum.TOURNESOL_AFFAMÉ__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.TOURNESOL_AFFAME;

                case (int)ChallengeEnum.CHAFER_RONIN__CHALLENGE_1_:
                case (int)ChallengeEnum.CHAFER_RONIN__CHALLENGE_2_:
                case (int)ChallengeEnum.CHAFER_RONIN__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.CHAFER_RONIN;

                case (int)ChallengeEnum.BWORKETTE__CHALLENGE_1_:
                case (int)ChallengeEnum.BWORKETTE__CHALLENGE_2_:
                case (int)ChallengeEnum.BWORKETTE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BWORKETTE;

                case (int)ChallengeEnum.COFFRE_DES_FORGERONS__CHALLENGE_1_:
                case (int)ChallengeEnum.COFFRE_DES_FORGERONS__CHALLENGE_2_:
                case (int)ChallengeEnum.COFFRE_DES_FORGERONS__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.COFFRE_DES_FORGERONS;

                case (int)ChallengeEnum.KANNIBOUL_EBIL__CHALLENGE_1_:
                case (int)ChallengeEnum.KANNIBOUL_EBIL__CHALLENGE_2_:
                case (int)ChallengeEnum.KANNIBOUL_EBIL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KANNIBOUL_EBIL;

                case (int)ChallengeEnum.CRAQUELEUR_LÉGENDAIRE__CHALLENGE_1_:
                case (int)ChallengeEnum.CRAQUELEUR_LÉGENDAIRE__CHALLENGE_2_:
                case (int)ChallengeEnum.CRAQUELEUR_LÉGENDAIRE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.CRAQUELEUR_LEGENDAIRE;

                case (int)ChallengeEnum.DAÏGORO__CHALLENGE_1_:
                case (int)ChallengeEnum.DAÏGORO__CHALLENGE_2_:
                case (int)ChallengeEnum.DAÏGORO__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.DAIGORO;

                case (int)ChallengeEnum.REINE_NYÉE__CHALLENGE_1_:
                case (int)ChallengeEnum.REINE_NYÉE__CHALLENGE_2_:
                case (int)ChallengeEnum.REINE_NYÉE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.REINE_NYEE;

                case (int)ChallengeEnum.ABRAKNYDE_ANCESTRAL__CHALLENGE_1_:
                case (int)ChallengeEnum.ABRAKNYDE_ANCESTRAL__CHALLENGE_2_:
                case (int)ChallengeEnum.ABRAKNYDE_ANCESTRAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.ABRAKNYDE_ANCESTRAL;

                case (int)ChallengeEnum.MEULOU__CHALLENGE_1_:
                case (int)ChallengeEnum.MEULOU__CHALLENGE_2_:
                case (int)ChallengeEnum.MEULOU__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MEULOU;

                case (int)ChallengeEnum.SILF_LE_RASBOUL_MAJEUR__CHALLENGE_1_:
                case (int)ChallengeEnum.SILF_LE_RASBOUL_MAJEUR__CHALLENGE_2_:
                case (int)ChallengeEnum.SILF_LE_RASBOUL_MAJEUR__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.SILF_LE_RASBOUL_MAJEUR;

                case (int)ChallengeEnum.MAÎTRE_CORBAC__CHALLENGE_1_:
                case (int)ChallengeEnum.MAÎTRE_CORBAC__CHALLENGE_2_:
                case (int)ChallengeEnum.MAÎTRE_CORBAC__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MAITRE_CORBAC;

                case (int)ChallengeEnum.RAT_BLANC__CHALLENGE_1_:
                case (int)ChallengeEnum.RAT_BLANC__CHALLENGE_2_:
                case (int)ChallengeEnum.RAT_BLANC__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.RAT_BLANC;

                case (int)ChallengeEnum.ROYALMOUTH__CHALLENGE_1_:
                case (int)ChallengeEnum.ROYALMOUTH__CHALLENGE_2_:
                case (int)ChallengeEnum.ROYALMOUTH__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.ROYALMOUTH;

                case (int)ChallengeEnum.MAÎTRE_PANDORE__CHALLENGE_1_:
                case (int)ChallengeEnum.MAÎTRE_PANDORE__CHALLENGE_2_:
                case (int)ChallengeEnum.MAÎTRE_PANDORE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MAITRE_PANDORE;

                case (int)ChallengeEnum.HAUTE_TRUCHE__CHALLENGE_1_:
                case (int)ChallengeEnum.HAUTE_TRUCHE__CHALLENGE_2_:
                case (int)ChallengeEnum.HAUTE_TRUCHE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.HAUTE_TRUCHE;

                case (int)ChallengeEnum.CHÊNE_MOU__CHALLENGE_1_:
                case (int)ChallengeEnum.CHÊNE_MOU__CHALLENGE_2_:
                case (int)ChallengeEnum.CHÊNE_MOU__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.CHENE_MOU;

                case (int)ChallengeEnum.KIMBO__CHALLENGE_1_:
                case (int)ChallengeEnum.KIMBO__CHALLENGE_2_:
                case (int)ChallengeEnum.KIMBO__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KIMBO;

                case (int)ChallengeEnum.MINOTOT__CHALLENGE_1_:
                case (int)ChallengeEnum.MINOTOT__CHALLENGE_2_:
                case (int)ChallengeEnum.MINOTOT__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MINOTOT;

                case (int)ChallengeEnum.OBSIDIANTRE__CHALLENGE_1_:
                case (int)ChallengeEnum.OBSIDIANTRE__CHALLENGE_2_:
                case (int)ChallengeEnum.OBSIDIANTRE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.OBSIDIANTRE;

                case (int)ChallengeEnum.KANIGROULA__CHALLENGE_1_:
                case (int)ChallengeEnum.KANIGROULA__CHALLENGE_2_:
                case (int)ChallengeEnum.KANIGROULA__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KANIGROULA;

                case (int)ChallengeEnum.USH_GALESH__CHALLENGE_1_:
                case (int)ChallengeEnum.USH_GALESH__CHALLENGE_2_:
                case (int)ChallengeEnum.USH_GALESH__DUO_:
                    return (int)MonsterIdEnum.USH_GALESH_4264;

                case (int)ChallengeEnum.TENGU_GIVREFOUX__CHALLENGE_1_:
                case (int)ChallengeEnum.TENGU_GIVREFOUX__CHALLENGE_2_:
                case (int)ChallengeEnum.TENGU_GIVREFOUX__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.TENGU_GIVREFOUX;

                case (int)ChallengeEnum.PÈRE_VER__CHALLENGE_1_:
                case (int)ChallengeEnum.PÈRE_VER__CHALLENGE_2_:
                case (int)ChallengeEnum.PÈRE_VER__DUO_:
                    return (int)MonsterIdEnum.PERE_VER;

                case (int)ChallengeEnum.KOLOSSO__CHALLENGE_1_:
                case (int)ChallengeEnum.KOLOSSO__CHALLENGE_2_:
                case (int)ChallengeEnum.KOLOSSO__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KOLOSSO;

                case (int)ChallengeEnum.GLOURSÉLESTE__CHALLENGE_1_:
                case (int)ChallengeEnum.GLOURSÉLESTE__CHALLENGE_2_:
                case (int)ChallengeEnum.GLOURSÉLESTE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.GLOURSELESTE;

                case (int)ChallengeEnum.OMBRE__CHALLENGE_1_:
                case (int)ChallengeEnum.OMBRE__CHALLENGE_2_:
                case (int)ChallengeEnum.OMBRE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.OMBRE_3564;

                case (int)ChallengeEnum.COMTE_RAZOF__CHALLENGE_1_:
                case (int)ChallengeEnum.COMTE_RAZOF__CHALLENGE_2_:
                case (int)ChallengeEnum.COMTE_RAZOF__DUO_:
                    return (int)MonsterIdEnum.COMTE_RAZOF;

                case (int)ChallengeEnum.ROI_NIDAS__CHALLENGE_1_:
                case (int)ChallengeEnum.ROI_NIDAS__CHALLENGE_2_:
                case (int)ChallengeEnum.ROI_NIDAS__CHALLENGE_TRIO_:
                    return (int)MonsterIdEnum.ROI_NIDAS;

                case (int)ChallengeEnum.REINE_DES_VOLEURS__CHALLENGE_1_:
                case (int)ChallengeEnum.REINE_DES_VOLEURS__CHALLENGE_2_:
                case (int)ChallengeEnum.REINE_DES_VOLEURS__CHALLENGE_TRIO_:
                    return (int)MonsterIdEnum.REINE_DES_VOLEURS_3726;

                case (int)ChallengeEnum.ANERICE_LA_SHUSHESS__CHALLENGE_1_:
                case (int)ChallengeEnum.ANERICE_LA_SHUSHESS__CHALLENGE_2_:
                case (int)ChallengeEnum.ANERICE_LA_SHUSHESS__DUO_:
                    return (int)MonsterIdEnum.ANERICE_LA_SHUSHESS;

                case (int)ChallengeEnum.DAZAK_MARTEGEL__CHALLENGE_1_:
                case (int)ChallengeEnum.DAZAK_MARTEGEL__CHALLENGE_2_:
                case (int)ChallengeEnum.DAZAK_MARTEGEL__DUO_:
                    return (int)MonsterIdEnum.DAZAK_MARTEGEL;

                case (int)ChallengeEnum.KANKREBLATH__CHALLENGE_1_:
                case (int)ChallengeEnum.KANKREBLATH__CHALLENGE_2_:
                case (int)ChallengeEnum.KANKREBLATH__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KANKREBLATH;

                case (int)ChallengeEnum.GELÉE_ROYALE_BLEUE__CHALLENGE_1_:
                case (int)ChallengeEnum.GELÉE_ROYALE_BLEUE__CHALLENGE_2_:
                case (int)ChallengeEnum.GELÉE_ROYALE_BLEUE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.GELEE_ROYALE_BLEUE;

                case (int)ChallengeEnum.GELÉE_ROYALE_CITRON__CHALLENGE_1_:
                case (int)ChallengeEnum.GELÉE_ROYALE_CITRON__CHALLENGE_2_:
                case (int)ChallengeEnum.GELÉE_ROYALE_CITRON__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.GELEE_ROYALE_CITRON;

                case (int)ChallengeEnum.GELÉE_ROYALE_FRAISE__CHALLENGE_1_:
                case (int)ChallengeEnum.GELÉE_ROYALE_FRAISE__CHALLENGE_2_:
                case (int)ChallengeEnum.GELÉE_ROYALE_FRAISE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.GELEE_ROYALE_FRAISE;

                case (int)ChallengeEnum.GELÉE_ROYALE_MENTHE__CHALLENGE_1_:
                case (int)ChallengeEnum.GELÉE_ROYALE_MENTHE__CHALLENGE_2_:
                case (int)ChallengeEnum.GELÉE_ROYALE_MENTHE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.GELEE_ROYALE_MENTHE;

                case (int)ChallengeEnum.CROCABULIA__CHALLENGE_1_:
                case (int)ChallengeEnum.CROCABULIA__CHALLENGE_2_:
                case (int)ChallengeEnum.CROCABULIA__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.CROCABULIA;

                case (int)ChallengeEnum.OUGAH__CHALLENGE_1_:
                case (int)ChallengeEnum.OUGAH__CHALLENGE_2_:
                case (int)ChallengeEnum.OUGAH__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.OUGAH;

                case (int)ChallengeEnum.MISSIZ_FRIZZ__CHALLENGE_1_:
                case (int)ChallengeEnum.MISSIZ_FRIZZ___CHALLENGE_2_:
                case (int)ChallengeEnum.MISSIZ_FRIZZ__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MISSIZ_FRIZZ;

                case (int)ChallengeEnum.SCARABOSSE_DORÉ__CHALLENGE_1_:
                case (int)ChallengeEnum.SCARABOSSE_DORÉ__CHALLENGE_2_:
                case (int)ChallengeEnum.SCARABOSSE_DORÉ__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.SCARABOSSE_DORE;

                case (int)ChallengeEnum.KWAKWA__CHALLENGE_1_:
                case (int)ChallengeEnum.KWAKWA__CHALLENGE_2_:
                case (int)ChallengeEnum.KWAKWA__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KWAKWA;

                case (int)ChallengeEnum.MANTISCORE__CHALLENGE_1_:
                case (int)ChallengeEnum.MANTISCORE__CHALLENGE_2_:
                case (int)ChallengeEnum.MANTISCORE__DUO_:
                    return (int)MonsterIdEnum.MANTISCORE;

                case (int)ChallengeEnum.KOULOSSE__CHALLENGE_1_:
                case (int)ChallengeEnum.KOULOSSE__CHALLENGE_2_:
                case (int)ChallengeEnum.KOULOSSE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KOULOSSE;

                case (int)ChallengeEnum.TYNRIL_AHURI__CHALLENGE_1_:
                case (int)ChallengeEnum.TYNRIL_AHURI__CHALLENGE_2_:
                case (int)ChallengeEnum.TYNRIL_AHURI__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.TYNRIL_AHURI;

                case (int)ChallengeEnum.XLII__CHALLENGE_1_:
                case (int)ChallengeEnum.XLII__CHALLENGE_2_:
                case (int)ChallengeEnum.XLII__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.XLII;

                case (int)ChallengeEnum.KORRIANDRE__CHALLENGE_1_:
                case (int)ChallengeEnum.KORRIANDRE__CHALLENGE_2_:
                case (int)ChallengeEnum.KORRIANDRE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.XLII;

                case (int)ChallengeEnum.BETHEL_AKARNA__CHALLENGE_1_:
                case (int)ChallengeEnum.BETHEL_AKARNA__CHALLENGE_2_:
                case (int)ChallengeEnum.BETHEL_AKARNA__DUO_:
                    return (int)MonsterIdEnum.BETHEL_AKARNA;

                case (int)ChallengeEnum.MOB_L_EPONGE__CHALLENGE_1_:
                case (int)ChallengeEnum.MOB_L_EPONGE__CHALLENGE_2_:
                case (int)ChallengeEnum.MOB_L_EPONGE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MOB_L_EPONGE;

                case (int)ChallengeEnum.BOOSTACHE__CHALLENGE_1_:
                case (int)ChallengeEnum.BOOSTACHE__CHALLENGE_2_:
                case (int)ChallengeEnum.BOOSTACHE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BOOSTACHE;

                case (int)ChallengeEnum.BULBIG_BROZEUR__CHALLENGE_1_:
                case (int)ChallengeEnum.BULBIG_BROZEUR__CHALLENGE_2_:
                case (int)ChallengeEnum.BULBIG_BROZEUR__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BULBIG_BROZEUR;

                case (int)ChallengeEnum.MINOTOROR__CHALLENGE_1_:
                case (int)ChallengeEnum.MINOTOROR__CHALLENGE_2_:
                case (int)ChallengeEnum.MINOTOROR__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MINOTOROR;

                case (int)ChallengeEnum.FRAKTALE__CHALLENGE_1_:
                case (int)ChallengeEnum.FRAKTALE__CHALLENGE_2_:
                case (int)ChallengeEnum.FRAKTALE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.FRAKTALE;

                case (int)ChallengeEnum.TOXOLIATH__CHALLENGE_1_:
                case (int)ChallengeEnum.TOXOLIATH__CHALLENGE_2_:
                case (int)ChallengeEnum.TOXOLIATH__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.TOXOLIATH;

                case (int)ChallengeEnum.SYLARGH__CHALLENGE_1_:
                case (int)ChallengeEnum.SYLARGH__CHALLENGE_2_:
                case (int)ChallengeEnum.SYLARGH__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.SYLARGH;

                case (int)ChallengeEnum.BATOFU__CHALLENGE_1_:
                case (int)ChallengeEnum.BATOFU__CHALLENGE_2_:
                case (int)ChallengeEnum.BATOFU__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BATOFU;

                case (int)ChallengeEnum.SHIN_LARVE__CHALLENGE_1_:
                case (int)ChallengeEnum.SHIN_LARVE__CHALLENGE_2_:
                case (int)ChallengeEnum.SHIN_LARVE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.SHIN_LARVE;

                case (int)ChallengeEnum.DRAGON_COCHON__CHALLENGE_1_:
                case (int)ChallengeEnum.DRAGON_COCHON__CHALLENGE_2_:
                case (int)ChallengeEnum.DRAGON_COCHON__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.DRAGON_COCHON;

                case (int)ChallengeEnum.MOON__CHALLENGE_1_:
                case (int)ChallengeEnum.MOON__CHALLENGE_2_:
                case (int)ChallengeEnum.MOON__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MOON;

                case (int)ChallengeEnum.GROLLOUM__CHALLENGE_1_:
                case (int)ChallengeEnum.GROLLOUM__CHALLENGE_2_:
                case (int)ChallengeEnum.GROLLOUM__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.GROLLOUM;

                case (int)ChallengeEnum.HAREBOURG__CHALLENGE_1_:
                case (int)ChallengeEnum.HAREBOURG__CHALLENGE_2_:
                case (int)ChallengeEnum.HAREBOURG__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.COMTE_HAREBOURG;

                case (int)ChallengeEnum.SOLAR__CHALLENGE_1_:
                case (int)ChallengeEnum.SOLAR__CHALLENGE_2_:
                case (int)ChallengeEnum.SOLAR__DUO_:
                    return (int)MonsterIdEnum.SOLAR;

                case (int)ChallengeEnum.BLOP_COCO_ROYAL__CHALLENGE_1_:
                case (int)ChallengeEnum.BLOP_COCO_ROYAL__CHALLENGE_2_:
                case (int)ChallengeEnum.BLOP_COCO_ROYAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BLOP_COCO_ROYAL;

                case (int)ChallengeEnum.BLOP_GRIOTTE_ROYAL__CHALLENGE_1_:
                case (int)ChallengeEnum.BLOP_GRIOTTE_ROYAL__CHALLENGE_2_:
                case (int)ChallengeEnum.BLOP_GRIOTTE_ROYAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BLOP_GRIOTTE_ROYAL;

                case (int)ChallengeEnum.BLOP_INDIGO_ROYAL__CHALLENGE_1_:
                case (int)ChallengeEnum.BLOP_INDIGO_ROYAL__CHALLENGE_2_:
                case (int)ChallengeEnum.BLOP_INDIGO_ROYAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BLOP_INDIGO_ROYAL;

                case (int)ChallengeEnum.BLOP_REINETTE_ROYAL__CHALLENGE_1_:
                case (int)ChallengeEnum.BLOP_REINETTE_ROYAL__CHALLENGE_2_:
                case (int)ChallengeEnum.BLOP_REINETTE_ROYAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BLOP_REINETTE_ROYAL;

                case (int)ChallengeEnum.BOUFTOU_ROYAL__CHALLENGE_1_:
                case (int)ChallengeEnum.BOUFTOU_ROYAL__CHALLENGE_2_:
                case (int)ChallengeEnum.BOUFTOU_ROYAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BOUFTOU_ROYAL;

                case (int)ChallengeEnum.KLIME__CHALLENGE_1_:
                case (int)ChallengeEnum.KLIME__CHALLENGE_2_:
                case (int)ChallengeEnum.KLIME__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KLIME;

                case (int)ChallengeEnum.NILEZA__CHALLENGE_1_:
                case (int)ChallengeEnum.NILEZA__CHALLENGE_2_:
                case (int)ChallengeEnum.NILEZA__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.NILEZA;

                case (int)ChallengeEnum.CHALOEIL__CHALLENGE_1_:
                case (int)ChallengeEnum.CHALOEIL__CHALLENGE_2_:
                case (int)ChallengeEnum.CHALOEIL__TRIO_:
                    return (int)MonsterIdEnum.CHALOEIL;

                case (int)ChallengeEnum.CAPITAINE_MENO__CHALLENGE_1_:
                case (int)ChallengeEnum.CAPITAINE_MENO__CHALLENGE_2_:
                case (int)ChallengeEnum.CAPITAINE_MENO__DUO_:
                    return (int)MonsterIdEnum.CAPITAINE_MENO;

                case (int)ChallengeEnum.LARVE_DE_KOUTOULOU__CHALLENGE_1_:
                case (int)ChallengeEnum.LARVE_DE_KOUTOULOU__CHALLENGE_2_:
                case (int)ChallengeEnum.LARVE_DE_KOUTOULOU__DUO_:
                    return (int)MonsterIdEnum.LARVE_DE_KOUTOULOU;

                case (int)ChallengeEnum.CORAILLEUR_MAGISTRAL__CHALLENGE_1_:
                case (int)ChallengeEnum.CORAILLEUR_MAGISTRAL__CHALLENGE_2_:
                case (int)ChallengeEnum.CORAILLEUR_MAGISTRAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.CORAILLEUR_MAGISTRAL;

                case (int)ChallengeEnum.WA_WABBIT__CHALLENGE_1_:
                case (int)ChallengeEnum.WA_WABBIT__CHALLENGE_2_:
                case (int)ChallengeEnum.WA_WABBIT__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.WA_WABBIT;

                case (int)ChallengeEnum.WA_WOBOT__CHALLENGE_1_:
                case (int)ChallengeEnum.WA_WOBOT__CHALLENGE_2_:
                case (int)ChallengeEnum.WA_WOBOT__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.WA_WOBOT;

                case (int)ChallengeEnum.MALLÉFISK__CHALLENGE_1_:
                case (int)ChallengeEnum.MALLÉFISK__CHALLENGE_2_:
                case (int)ChallengeEnum.MALLÉFISK__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MALLEFISK;

                case (int)ChallengeEnum.RAT_NOIR__CHALLENGE_1_:
                case (int)ChallengeEnum.RAT_NOIR__CHALLENGE_2_:
                case (int)ChallengeEnum.RAT_NOIR__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.RAT_NOIR;

                case (int)ChallengeEnum.POUNICHEUR__CHALLENGE_1_:
                case (int)ChallengeEnum.POUNICHEUR__CHALLENGE_2_:
                case (int)ChallengeEnum.POUNICHEUR__DUO_:
                    return (int)MonsterIdEnum.POUNICHEUR;

                case (int)ChallengeEnum.SKEUNK__CHALLENGE_1_:
                case (int)ChallengeEnum.SKEUNK__CHALLENGE_2_:
                case (int)ChallengeEnum.SKEUNK__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.SKEUNK;

                case (int)ChallengeEnum.EL_PIKO__CHALLENGE_1_:
                case (int)ChallengeEnum.EL_PIKO__CHALLENGE_2_:
                case (int)ChallengeEnum.EL_PIKO__DUO_:
                    return (int)MonsterIdEnum.EL_PIKO;

                case (int)ChallengeEnum.KRALAMOUR_GÉANT__CHALLENGE_1_:
                case (int)ChallengeEnum.KRALAMOUR_GÉANT__CHALLENGE_2_:
                case (int)ChallengeEnum.KRALAMOUR_GÉANT__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.KRALAMOURE_GEANT;

                case (int)ChallengeEnum.BWORKER__CHALLENGE_1_:
                case (int)ChallengeEnum.BWORKER__CHALLENGE_2_:
                case (int)ChallengeEnum.BWORKER__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BWORKER;

                case (int)ChallengeEnum.MAÎTRE_DES_PANTINS__CHALLENGE_1_:
                case (int)ChallengeEnum.MAÎTRE_DES_PANTINS__CHALLENGE_2_:
                case (int)ChallengeEnum.MAÎTRE_DES_PANTINS__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MAITRE_DES_PANTINS;

                case (int)ChallengeEnum.TOFU_ROYAL__CHALLENGE_1_:
                case (int)ChallengeEnum.TOFU_ROYAL__CHALLENGE_2_:
                case (int)ChallengeEnum.TOFU_ROYAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.TOFU_ROYAL;

                case (int)ChallengeEnum.CAPITAINE_EKARLATTE__CHALLENGE_1_:
                case (int)ChallengeEnum.CAPITAINE_EKARLATTE__CHALLENGE_2_:
                case (int)ChallengeEnum.CAPITAINE_EKARLATTE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.CAPITAINE_EKARLATTE;

                case (int)ChallengeEnum.PÉKI_PÉKI__CHALLENGE_1_:
                case (int)ChallengeEnum.PÉKI_PÉKI__CHALLENGE_2_:
                case (int)ChallengeEnum.PÉKI_PÉKI__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.PEKI_PEKI;

                case (int)ChallengeEnum.BEN_LE_RIPATE__CHALLENGE_1_:
                case (int)ChallengeEnum.BEN_LE_RIPATE__CHALLENGE_2_:
                case (int)ChallengeEnum.BEN_LE_RIPATE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BEN_LE_RIPATE;

                case (int)ChallengeEnum.FUJI_GIVREFOUX_NOURRICIÈRE__CHALLENGE_1_:
                case (int)ChallengeEnum.FUJI_GIVREFOUX_NOURRICIÈRE__CHALLENGE_2_:
                case (int)ChallengeEnum.FUJI_GIVREFOUX_NOURRICIÈRE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.FUJI_GIVREFOUX_NOURRICIERE;

                case (int)ChallengeEnum.PROTOZORREUR__CHALLENGE_1_:
                case (int)ChallengeEnum.PROTOZORREUR__CHALLENGE_2_:
                case (int)ChallengeEnum.PROTOZORREUR__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.PROTOZORREUR;

                case (int)ChallengeEnum.DANTINEA__CHALLENGE_1_:
                case (int)ChallengeEnum.DANTINEA__CHALLENGE_2_:
                case (int)ChallengeEnum.DANTINEA__DUO_:
                    return (int)MonsterIdEnum.DANTINEA;

                case (int)ChallengeEnum.TAL_KASHA__CHALLENGE_1_:
                case (int)ChallengeEnum.TAL_KASHA__CHALLENGE_2_:
                case (int)ChallengeEnum.TAL_KASHA__DUO_:
                    return (int)MonsterIdEnum.TAL_KASHA;

                case (int)ChallengeEnum.CHOUDINI__CHALLENGE_1_:
                case (int)ChallengeEnum.CHOUDINI__CHALLENGE_2_:
                case (int)ChallengeEnum.CHOUDINI__DUO_:
                    return (int)MonsterIdEnum.CHOUDINI;

                case (int)ChallengeEnum.BLOP_MULTICOLORE_ROYAL__CHALLENGE_1_:
                case (int)ChallengeEnum.BLOP_MULTICOLORE_ROYAL__CHALLENGE_2_:
                case (int)ChallengeEnum.BLOP_MULTICOLORE_ROYAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.BLOP_MULTICOLORE_ROYAL;

                case (int)ChallengeEnum.TANUKOUÏ_SAN__CHALLENGE_1_:
                case (int)ChallengeEnum.TANUKOUÏ_SAN__CHALLENGE_2_:
                case (int)ChallengeEnum.TANUKOUÏ_SAN__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.TANUKOUI_SAN;

                case (int)ChallengeEnum.SPHINCTER_CELL__CHALLENGE_1_:
                case (int)ChallengeEnum.SPHINCTER_CELL__CHALLENGE_2_:
                case (int)ChallengeEnum.SPHINCTER_CELL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.SPHINCTER_CELL;

                case (int)ChallengeEnum.PHOSSILE__CHALLENGE_1_:
                case (int)ChallengeEnum.PHOSSILE__CHALLENGE_2_:
                case (int)ChallengeEnum.PHOSSILE__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.PHOSSILE;

                case (int)ChallengeEnum.VORTEX__CHALLENGE_1_:
                case (int)ChallengeEnum.VORTEX__CHALLENGE_2_:
                case (int)ChallengeEnum.VORTEX__CHALLENGE_TRIO_:
                    return (int)MonsterIdEnum.VORTEX;

                case (int)ChallengeEnum.NELWEEN__CHALLENGE_1_:
                case (int)ChallengeEnum.NELWEEN__CHALLENGE_2_:
                case (int)ChallengeEnum.NELWEEN__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.NELWEEN;

                case (int)ChallengeEnum.CHOUQUE__CHALLENGE_1_:
                case (int)ChallengeEnum.CHOUQUE__CHALLENGE_2_:
                case (int)ChallengeEnum.CHOUQUE__DUO_:
                    return (int)MonsterIdEnum.LE_CHOUQUE;

                case (int)ChallengeEnum.MANSOT_ROYAL__CHALLENGE_1_:
                case (int)ChallengeEnum.MANSOT_ROYAL__CHALLENGE_2_:
                case (int)ChallengeEnum.MANSOT_ROYAL__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MANSOT_ROYAL;

                case (int)ChallengeEnum.MERKATOR__CHALLENGE_1_:
                case (int)ChallengeEnum.MERKATOR__CHALLENGE_2_:
                case (int)ChallengeEnum.MERKATOR__CHALLENGE_DUO_:
                    return (int)MonsterIdEnum.MERKATOR;

                case (int)ChallengeEnum.ILYZAELLE__CHALLENGE_1_:
                case (int)ChallengeEnum.ILYZAELLE__CHALLENGE_2_:
                case (int)ChallengeEnum.ILYZAELLE__DUO_:
                    return (int)MonsterIdEnum.ILYZAELLE;

                default:
                    return 0;
            }
        }

        public override ushort MaxValue
        {
            get
            {
                if (m_maxValue == null)
                {
                    m_maxValue = (ushort)Number;

                    switch (base[0].Operator)
                    {
                        case ComparaisonOperatorEnum.EQUALS:
                            break;

                        case ComparaisonOperatorEnum.INFERIOR:
                            throw new Exception();

                        case ComparaisonOperatorEnum.SUPERIOR:
                            m_maxValue++;
                            break;
                    }
                }

                return m_maxValue.Value;
            }
        }

        // METHODS
        public override DefaultCriterionData Parse(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            return new DefaultCriterionData(@operator, parameters);
        }

        public override bool Eval(Character character)
        {
            return character.Achievement.GetRunningCriterion(this) >= Number;
        }

        public override bool Lower(AbstractCriterion left)
        {
            return Number < ((KillBossWithChallengeCriterion)left).Number;
        }

        public override bool Greater(AbstractCriterion left)
        {
            return Number > ((KillBossWithChallengeCriterion)left).Number;
        }

        public override ushort GetPlayerValue(PlayerAchievement player)
        {
            return (ushort)Math.Min(MaxValue, player.GetRunningCriterion(this));
        }
    }
}