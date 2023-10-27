﻿using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.DUEL)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_COCO_ROYAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_GRIOTTE_ROYAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_INDIGO_ROYAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_REINETTE_ROYAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_BLEUE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_CITRON__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_FRAISE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.GELÉE_ROYALE_MENTHE__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.BLOP_MULTICOLORE_ROYAL__CHALLENGE_1_)]
    [ChallengeIdentifier((int)ChallengeEnum.EL_PIKO__CHALLENGE_1_)]
    public class DuelChallenge : DefaultChallenge
    {
        private readonly Dictionary<MonsterFighter, CharacterFighter> m_history = new Dictionary<MonsterFighter, CharacterFighter>();

        public DuelChallenge(int id, IFight fight)
            : base(id, fight)
        {
            BonusMin = 40;
            BonusMax = 40;
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var fighter in Fight.GetAllFighters<MonsterFighter>())
                fighter.DamageInflicted += OnDamageInflicted;
        }

        public override bool IsEligible() => Fight.GetAllFighters<MonsterFighter>().Count() > 1;

        void OnDamageInflicted(FightActor fighter, Damage damage)
        {
            var source = (damage.Source is SummonedFighter) ? ((SummonedFighter) damage.Source).Summoner : damage.Source;

            if (!(source is CharacterFighter))
                return;

            CharacterFighter caster;
            m_history.TryGetValue((MonsterFighter) fighter, out caster);

            if (caster == null)
            {
                m_history.Add((MonsterFighter)fighter, (CharacterFighter)source);
                return;
            }

            if (caster == source)
                return;

            UpdateStatus(ChallengeStatusEnum.FAILED, source);
        }

        protected override void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            base.OnWinnersDetermined(fight, winners, losers, draw);

            foreach (var fighter in Fight.GetAllFighters<MonsterFighter>())
                fighter.DamageInflicted -= OnDamageInflicted;
        }
    }
}
