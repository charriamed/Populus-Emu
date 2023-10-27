using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.BAMBOU)]
    public class Bambou : Brain
    {
        public Bambou(AIFighter fighter)
            : base(fighter)
        {
            fighter.DamageInflicted += OnDamageInflicted;
        }

        private void OnDamageInflicted(FightActor fighter, Damage dmg)
        {
            if (fighter != Fighter)
                return;

            fighter.CastAutoSpell(new Spell(10099, 1), fighter.Cell);
        }
    }
}
