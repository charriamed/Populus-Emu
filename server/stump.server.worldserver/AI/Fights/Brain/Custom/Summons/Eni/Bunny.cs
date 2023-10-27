using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons.Ani
{
    [BrainIdentifier((int)MonsterIdEnum.LAPINO)]
    [BrainIdentifier((int)MonsterIdEnum.LAPINO_PROTECTEUR)]
    class Bunny : Brain
    {
        public Bunny(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
            fighter.BeforeDead += OnBeforeDead;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == 39)
            {
                fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STIMULATING_WORD_126, (short)fighter.Level), fighter.Cell);
            }
            if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == (int)MonsterIdEnum.LAPINO_PROTECTEUR)
            {
                fighter.CastAutoSpell(new Spell((int)SpellIdEnum.STIMULATING_WORD, (short)fighter.Level), fighter.Cell);
            }
        }

        public void OnBeforeDead(FightActor fighter, FightActor killedBy)
        {
            if (fighter != Fighter)
                return;

            if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == 39)
            {
                (fighter as SummonedMonster).Summoner.CastAutoSpell(new Spell(6638, 1), fighter.Cell);
            }
            if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == (int)MonsterIdEnum.LAPINO_PROTECTEUR)
            {
                (fighter as SummonedMonster).Summoner.CastAutoSpell(new Spell(9598, 1), fighter.Cell);
            }
        }

        public override void Play()
        {
            foreach (var spell in Fighter.Spells.Values)
            {
                FightActor target = null;
                if (Fighter.Id == (int)MonsterIdEnum.LAPINO_PROTECTEUR)
                {
                    target = Environment.GetNearestFighter(x => x.IsFriendlyWith(Fighter) && x != Fighter);
                }
                else
                {
                    target = Environment.GetNearestFighter(x => x.IsFriendlyWith(Fighter) && x != Fighter && x.LifePoints < x.MaxLifePoints);

                }
                var ennemy = Environment.GetNearestEnemy();

                var selector = new PrioritySelector();
                selector.AddChild(new Decorator(ctx => target == null, new DecoratorContinue(new RandomMove(Fighter))));
                selector.AddChild(new Decorator(ctx => spell == null, new DecoratorContinue(new FleeAction(Fighter))));

                if (target != null && spell != null)
                {
                    selector.AddChild(new PrioritySelector(
                        new Decorator(ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
                            new Sequence(
                                new SpellCastAction(Fighter, spell, target.Cell, true),
                                new PrioritySelector(
                                    new Decorator(
                                        ctx => Fighter.Stats.Health.TotalMax / 2 > Fighter.LifePoints,
                                        new FleeAction(Fighter)),
                                    new Decorator(new MoveFarFrom(Fighter, ennemy))))),
                        new Sequence(
                            new MoveNearTo(Fighter, target),
                            new Decorator(
                                ctx => Fighter.CanCastSpell(spell, target.Cell) == SpellCastResult.OK,
                                new Sequence(
                                    new SpellCastAction(Fighter, spell, target.Cell, true))))));
                }

                foreach (var action in selector.Execute(this))
                {

                }
            }
        }
    }
}