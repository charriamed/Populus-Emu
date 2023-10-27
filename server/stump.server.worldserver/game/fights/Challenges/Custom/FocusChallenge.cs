using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.FOCUS)]
    [ChallengeIdentifier((int)ChallengeEnum.TANUKOUÏ_SAN__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.CAPITAINE_EKARLATTE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.VORTEX__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.TAL_KASHA__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.ILYZAELLE__CHALLENGE_2_)]
    [ChallengeIdentifier((int)ChallengeEnum.SOLAR__CHALLENGE_2_)]
    public class FocusChallenge : DefaultChallenge
    {
        public FocusChallenge(int id, IFight fight)
            : base(id, fight)
        {
            BonusMin = 30;
            BonusMax = 50;
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var fighter in Fight.GetAllFighters<MonsterFighter>())
            {
                fighter.BeforeDamageInflicted += OnBeforeDamageInflicted;
                fighter.Dead += OnDead;
            }
        }

        void OnDead(FightActor fighter, FightActor killer)
        {
            if (fighter == Target)
                Target = null;
        }

        void OnBeforeDamageInflicted(FightActor fighter, Damage damage)
        {
            if (!(damage.Source is CharacterFighter))
                return;

            if (damage.ReflectedDamages)
                return;

            if (Target == null || Target == fighter)
                Target = fighter;
            else
                UpdateStatus(ChallengeStatusEnum.FAILED, damage.Source);
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            base.OnWinnersDetermined(fight, winners, losers, draw);

            foreach (var fighter in Fight.GetAllFighters<MonsterFighter>())
            {
                fighter.BeforeDamageInflicted -= OnBeforeDamageInflicted;
                fighter.Dead -= OnDead;
            }
        }
    }
}
