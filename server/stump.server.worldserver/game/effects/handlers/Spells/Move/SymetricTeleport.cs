using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Actions;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;
using System.Threading.Tasks;
using Stump.Core.Threading;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move
{
    [EffectHandler(EffectsEnum.Effect_SymetricTargetTeleport)]
    [EffectHandler(EffectsEnum.Effect_SymetricCasterTeleport)]
    [EffectHandler(EffectsEnum.Effect_SymetricPointTeleport)]
    public class SymetricTeleport : SpellEffectHandler
    {
        public SymetricTeleport(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            List<FightActor> TelefraggedActor = new List<FightActor>();
            foreach (var target in GetAffectedActors())
            {
                var casterPoint = Caster.Position.Point;
                var targetPoint = target.Position.Point;

                switch (Effect.EffectId)
                {
                    case EffectsEnum.Effect_SymetricCasterTeleport:
                        casterPoint = target.Position.Point;
                        targetPoint = Caster.Position.Point;
                        break;
                    case EffectsEnum.Effect_SymetricPointTeleport:
                        casterPoint = target.Position.Point;
                        targetPoint = TargetedPoint;
                        break;
                }

                var cell = new MapPoint((2 * targetPoint.X - casterPoint.X), (2 * targetPoint.Y - casterPoint.Y));

                if (!cell.IsInMap())
                    continue;

                var dstCell = Map.GetCell(cell.CellId);

                if (dstCell == null)
                    continue;

                if (!dstCell.Walkable && !dstCell.NonWalkableDuringFight)
                {
                    continue;
                }

                var fighter = Fight.GetOneFighter(dstCell);
                if (fighter != null && fighter != target)
                {
                    var caster = Caster;

                    if (Effect.EffectId == EffectsEnum.Effect_SymetricCasterTeleport || Effect.EffectId == EffectsEnum.Effect_SymetricPointTeleport)
                        caster = target;

                    if (!TelefraggedActor.Contains(caster) && Effect.EffectId == EffectsEnum.Effect_SymetricPointTeleport)
                    {
                        caster.Telefrag(Caster, fighter);
                        TelefraggedActor.Add(fighter);
                    }
                    else if(Effect.EffectId != EffectsEnum.Effect_SymetricPointTeleport)
                    {
                        caster.Telefrag(Caster, fighter);
                    }
                }
                else
                {
                    var caster = Caster;

                    if (Effect.EffectId == EffectsEnum.Effect_SymetricCasterTeleport || Effect.EffectId == EffectsEnum.Effect_SymetricPointTeleport)
                        caster = target;

                    caster.Position.Cell = dstCell;
                    //caster.MovementHistory.RegisterEntry(dstCell);
                    Fight.ForEach(entry => ActionsHandler.SendGameActionFightTeleportOnSameMapMessage(entry.Client, caster, caster, dstCell), true);
                }
            }

            return true;
        }
    }

    [EffectHandler(EffectsEnum.Effect_2017)]
    public class Recurse : SpellEffectHandler
    {
        public Recurse(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            DoIt();
            return true;
        }

        public async void DoIt()
        {
            foreach (var target in GetAffectedActors())
            {
                try
                {
                    var custtarget = target;

                    var casterPoint = Caster.Position.Point;
                    var targetPoint = custtarget.Position.Point;

                    var direction = casterPoint.OrientationToAdjacent(targetPoint);
                    var Fighter = Fight.GetOneFighter(Map.GetCell(targetPoint.GetCellInDirection(direction, 1).CellId));
                    if (Fighter != null && Fighter is SummonedTurret)
                    {
                        casterPoint = custtarget.Position.Point;
                        custtarget = Fighter;
                        targetPoint = custtarget.Position.Point;
                        direction = casterPoint.OrientationToAdjacent(targetPoint);
                        Fighter = Fight.GetOneFighter(Map.GetCell(targetPoint.GetCellInDirection(direction, 1).CellId));
                    }
                    if (Fighter != null && Fighter is SummonedTurret)
                    {
                        casterPoint = custtarget.Position.Point;
                        custtarget = Fighter;
                        targetPoint = custtarget.Position.Point;
                        direction = casterPoint.OrientationToAdjacent(targetPoint);
                        Fighter = Fight.GetOneFighter(Map.GetCell(targetPoint.GetCellInDirection(direction, 1).CellId));
                    }
                    if (Fighter != null && Fighter is SummonedTurret)
                    {
                        casterPoint = custtarget.Position.Point;
                        custtarget = Fighter;
                        targetPoint = custtarget.Position.Point;
                        direction = casterPoint.OrientationToAdjacent(targetPoint);
                    }
                    bool newtowr = false;
                    Cell cdstcell = null;
                    var nextturret = target.Team.GetAllFighters().FirstOrDefault(x => x is SummonedTurret && x != target && x.IsAlive() && x.Position.Point.IsOnSameLine(target.Position.Point) && x.Position.Point.ManhattanDistanceTo(target.Position.Point) > 1 && !(x as SummonedTurret).AlreadyRecursive);
                    if (nextturret != null)
                    {
                        var direc2 = target.Position.Point.OrientationTo(nextturret.Position.Point);

                        var cell1 = Map.GetCell(nextturret.Position.Point.GetCellInDirection(direc2, 1).CellId);
                        if (cell1.Walkable)
                        {
                            if (target.Position.Point.GetCellsOnLineBetween(nextturret.Position.Point).Where(v => v.CellId != target.Position.Point.CellId && v.CellId != nextturret.Position.Point.CellId).Any(x => !Map.GetCell(x.CellId).Walkable))
                            {

                            }
                            else
                            {
                                if(target is SummonedTurret)
                                {
                                    (target as SummonedTurret).AlreadyRecursive = true;
                                }
                                cdstcell = Map.GetCell(target.Position.Point.GetAdjacentCells().FirstOrDefault(x => target.Position.Point.OrientationTo(x) == target.Position.Point.OrientationTo(nextturret.Position.Point)).CellId);
                                newtowr = true;
                            }
                        }
                    }

                    var cell = new MapPoint((2 * targetPoint.X - casterPoint.X), (2 * targetPoint.Y - casterPoint.Y));

                    if (!cell.IsInMap())
                        continue;

                    var dstCell = newtowr ? cdstcell : Map.GetCell(cell.CellId);

                    if (dstCell == null)
                        continue;

                    if (!dstCell.Walkable && !dstCell.NonWalkableDuringFight)
                        continue;

                    var fighter = Fight.GetOneFighter(dstCell);
                    if (fighter != null && fighter != custtarget)
                    {
                        Caster.Telefrag(Caster, fighter);
                    }
                    else
                    {
                        Caster.Position.Cell = dstCell;
                        Fight.ForEach(entry => ActionsHandler.SendGameActionFightTeleportOnSameMapMessage(entry.Client, Caster, Caster, dstCell), true);
                    }

                    EffectDice d = new EffectDice(EffectsEnum.Effect_PushBack, 4, 0, 0);
                    var hand = EffectManager.Instance.GetSpellEffectHandler(d, custtarget, CastHandler, Caster.Cell, false);
                    if (newtowr) (hand as Move.Push).PushDirection = target.Position.Point.OrientationTo(nextturret.Position.Point);
                    hand.SetAffectedActors(new FightActor[] { Caster });
                    hand.Apply();

                    if (newtowr) await Task.Factory.StartNewDelayed(1000, () =>
                    {
                        EffectDice d2 = new EffectDice(EffectsEnum.Effect_2017, 0, 0, 0);
                        var hand2 = EffectManager.Instance.GetSpellEffectHandler(d2, Caster, CastHandler, nextturret.Cell, false);
                        hand2.SetAffectedActors(new FightActor[] { nextturret });
                        using (Fight.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL)) hand2.Apply();
                    });
                }
                finally { }
            }
        }
    }
}
