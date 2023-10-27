using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using System.Collections.Generic;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.LA_GONFLABLE)]
    public class Inflatable : Brain
    {
        public Inflatable(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;
        }

        public override void Play()
        {
            foreach (var spell in Fighter.Spells.Values)
            {
                var target = Environment.GetNearestAlly();

                if (!(target is CharacterFighter))
                    target = Fighter.Summoner;

                var ennemy = Environment.GetNearestEnemy();
                var distance = Fighter.Position.Point.ManhattanDistanceTo(target.Position.Point);

                var selector_move = new PrioritySelector();
                var selector = new PrioritySelector();


                selector.AddChild(new Decorator(ctx => target == null, new DecoratorContinue(new RandomMove(Fighter))));
                selector.AddChild(new Decorator(ctx => spell == null, new DecoratorContinue(new FleeAction(Fighter))));

                if ((target != null && spell != null) && spell.Id == 587)
                {
                    // Caso o alvo não esteja dentro da zona de cura, apenas movimenta
                    selector_move.AddChild(new PrioritySelector(
                        new Decorator(ctx => distance > 3,
                            new Sequence(
                                new MoveNearTo(Fighter, target),
                                    new PrioritySelector(
                                        new Decorator(
                                            ctx => Fighter.Stats.Health.TotalMax / 2 > Fighter.LifePoints,
                                            new FleeAction(Fighter)),
                                        new Decorator(new MoveFarFrom(Fighter, ennemy)))))));

                    foreach (var action in selector_move.Execute(this)) { }

                    // O Seletor sempre fará a inflável dar cast na spell
                    selector.AddChild(new PrioritySelector(
                                 new Sequence(
                                        new Decorator(
                                            ctx => Fighter.CanCastSpell(spell, Fighter.Cell) == SpellCastResult.OK,
                                            new Sequence(
                                                new SpellCastAction(Fighter, spell, Fighter.Cell, true),
                                                    new FleeAction(Fighter))))));
                }

                else
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
