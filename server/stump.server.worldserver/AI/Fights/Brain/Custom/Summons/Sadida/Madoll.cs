using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.LA_FOLLE)]
    public class Madoll : Brain
    {
        public Madoll(AIFighter fighter)
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

                var m_targets = Environment.GetNearestFighters(entry => entry.IsEnnemyWith(Fighter) && entry.IsAlive());

                if(m_targets.Count() == 0 ||m_targets == null)
                {
                    var move = new PrioritySelector();

                    move.AddChild(new PrioritySelector(
                            new Decorator(new FleeAction(Fighter))));

                    foreach (var action in move.Execute(this)) { }
                }

                else if (m_targets.Count() > 0 && spell != null)
                {
                    int i = 0;

                    //Ações de cast
                    foreach (var targets in m_targets)
                    {
                        if (i == 2)
                            break;


                        var selector = new PrioritySelector();

                        selector.AddChild(new PrioritySelector(
                            new Decorator(ctx => Fighter.CanCastSpell(spell, targets.Cell) == SpellCastResult.OK,
                                new Sequence(
                                    new SpellCastAction(Fighter, spell, targets.Cell, true))),
                                    
                            
                            new Sequence(
                                new MoveNearTo(Fighter, targets),
                                new Decorator(
                                    ctx => Fighter.CanCastSpell(spell, targets.Cell) == SpellCastResult.OK,
                                    new Sequence(
                                        new SpellCastAction(Fighter, spell, targets.Cell, true))))));


                        foreach (var action in selector.Execute(this)) { }
                        i++;
                    }

                    //Ações de movimento
                    var move_selector = new PrioritySelector();

                    move_selector.AddChild(new PrioritySelector(
                            new Decorator(new FleeAction(Fighter))));

                    foreach (var action in move_selector.Execute(this)) { }

                }

            }
        }
    }
}