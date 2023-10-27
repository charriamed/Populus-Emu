using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier((int)MonsterIdEnum.KORRIANDRE)]
    public class KorriandreBrain : Brain
    {
        public KorriandreBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted; 
        }

        private void Fight_FightStarted(IFight obj)
        {
            Fighter.Stats.Fields.FirstOrDefault(x => x.Key == PlayerFields.SummonLimit).Value.Base = 3;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.KWEST, 1), Fighter.Cell);
        }

        [BrainIdentifier((int)MonsterIdEnum.SPORAKNE)]
        public class SporakneBrain : Brain
        {
            public SporakneBrain(AIFighter fighter)
                : base(fighter)
            {
                fighter.Fight.FightStarted += Fight_FightStarted;
            }

            private void Fight_FightStarted(IFight obj)
            {
                Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.JIEM, 1), Fighter.Cell);
        }

        [BrainIdentifier((int)MonsterIdEnum.MERULETTE)]
        public class MeruletteBrain : Brain
        {
            public MeruletteBrain(AIFighter fighter)
                : base(fighter)
            {
                    fighter.Fight.FightStarted += Fight_FightStarted;
            }

                private void Fight_FightStarted(IFight obj)
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SERPULVERISE, 1), Fighter.Cell);
                }
            }
        }
    }
}

