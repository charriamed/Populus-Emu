using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int) ChallengeEnum.BARBARE)]
    [ChallengeIdentifier((int) ChallengeEnum.HAUTE_TRUCHE__CHALLENGE_2_)]
    [ChallengeIdentifier((int) ChallengeEnum.XLII__CHALLENGE_1_)]
    [ChallengeIdentifier((int) ChallengeEnum.SYLARGH__CHALLENGE_2_)]
    [ChallengeIdentifier((int) ChallengeEnum.PROTOZORREUR__CHALLENGE_1_)]
    public class BarbaricChallenge : DefaultChallenge
    {
        public BarbaricChallenge(int id, IFight fight)
            : base(id, fight)
        {
            BonusMin = 60;
            BonusMax = 75;
        }

        bool WeaponAttack;

        public override void Initialize()
        {
            base.Initialize();

            Fight.TurnStopped += OnTurnStopped;

            foreach (var fighter in Fight.GetAllFighters<MonsterFighter>())
                fighter.DamageInflicted += OnDamageInflicted;
        }

        private void OnTurnStopped(IFight fight, FightActor fighter)
        {
            if (fighter is CharacterFighter && !WeaponAttack)
                UpdateStatus(ChallengeStatusEnum.FAILED, fighter);

            WeaponAttack = false;
        }

        private void OnDamageInflicted(FightActor fighter, Damage damage)
        {
            if (!damage.IsWeaponAttack)
                return;

            WeaponAttack = true;
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            base.OnWinnersDetermined(fight, winners, losers, draw);

            Fight.TurnStopped -= OnTurnStopped;

            foreach (var fighter in Fight.GetAllFighters<MonsterFighter>())
                fighter.DamageInflicted -= OnDamageInflicted;
        }
    }
}
