using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.Interfaces;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Targets
{
    public class MonsterCriterion : TargetCriterion
    {
        public MonsterCriterion(int monsterId, bool caster, bool required)
        {
            MonsterId = monsterId;
            Required = required;
            Caster = caster;
        }

        public int MonsterId
        {
            get;
            set;
        }

        public bool Required
        {
            get;
            set;
        }
        public bool Caster
        {
            get;
            set;
        }

        public override bool IsDisjonction => Required;

        public override bool IsTargetValid(FightActor actor, SpellEffectHandler handler)
        {
            if(Caster)
                return Required ? ((handler.Caster is ICreature) && (handler.Caster as ICreature).MonsterGrade.MonsterId == MonsterId) :
                (!(handler.Caster is ICreature) || (handler.Caster as ICreature).MonsterGrade.MonsterId != MonsterId);

            return Required ? ((actor is ICreature) && (actor as ICreature).MonsterGrade.MonsterId == MonsterId) :
                (!(actor is ICreature) || (actor as ICreature).MonsterGrade.MonsterId != MonsterId);
        }
    }
}
