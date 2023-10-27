using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Buffs.Customs;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom
{
    [BrainIdentifier((int)MonsterIdEnum.HAMRACK)]
    public class HamrackBrain : Brain
    {
        public HamrackBrain(AIFighter fighter) : base(fighter)
        {
            Fighter.GetAlive += OnGetAlive;
        }

        public override void Play()
        {
            var target = Environment.GetNearestFighter(x => x.IsFriendlyWith(Fighter) && x != Fighter && Fighter.Position.Point.SquareDistanceTo(x.Position.Point) < 3);
            var selector = new PrioritySelector();
            var spellinvu = Fighter.Spells.Where(x => x.Value.Id == 2618).FirstOrDefault().Value;
            var spell = Fighter.Spells.Where(x => x.Value.Id == 682).FirstOrDefault().Value;
            var enemy = Environment.GetNearestEnemy();

            if (target != null && spellinvu != null && (target as MonsterFighter).Monster.Template.Id == 2877)
            {
                selector.AddChild(new PrioritySelector(
                    new Decorator(ctx => Fighter.CanCastSpell(spellinvu, target.Cell) == SpellCastResult.OK,
                        new Sequence(new SpellCastAction(Fighter, spellinvu, target.Cell, false))),
                    new Sequence(new MoveNearTo(Fighter, target),
                        new Decorator(ctx => Fighter.CanCastSpell(spellinvu, target.Cell) == SpellCastResult.OK,
                        new Sequence(new SpellCastAction(Fighter, spellinvu, target.Cell, false))))
                    ));
            }
            else
            {
                selector.AddChild(new PrioritySelector(
                    new Decorator(ctx => Fighter.Position.Point.GetAdjacentCells().Contains(enemy.Position.Point),
                        new Sequence(new SpellCastAction(Fighter, spell, Fighter.Cell, false))),
                    new Sequence(new MoveNearTo(Fighter, enemy),
                        new Decorator(ctx => Fighter.Position.Point.GetAdjacentCells().Contains(enemy.Position.Point),
                        new Sequence(new SpellCastAction(Fighter, spell, Fighter.Cell, false))))
                    ));
            }

            foreach (var action in selector.Execute(this))
            {

            }
        }

        private void OnGetAlive(FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            if (fighter is SummonedMonster && (fighter as SummonedMonster).Monster.MonsterId == 2955) fighter.CastAutoSpell(new Spell(2617, 1), fighter.Cell);
        }
    }
}
