using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Threading;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.AI.Fights.Spells;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Cells.Shapes;
using Stump.Server.WorldServer.Game.Maps.Cells.Shapes.Set;
using Stump.Server.WorldServer.Game.Maps.Pathfinding;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

namespace Stump.Server.WorldServer.AI.Fights.Brain
{
    public class EnvironmentAnalyser
    {
        private MapPoint[] m_moveZone;

        public EnvironmentAnalyser(AIFighter fighter)
        {
            Fighter = fighter;
            CellInformationProvider = new AIFightCellsInformationProvider(Fighter.Fight, Fighter);
            Pathfinding = new Pathfinder(CellInformationProvider);
            fighter.PositionChanged += OnPositionChanged;
        }

        public Pathfinder Pathfinding
        {
            get;
            private set;
        }

        public AIFightCellsInformationProvider CellInformationProvider
        {
            get;
            private set;
        }

        public AIFighter Fighter
        {
            get;
            private set;
        }

        public IFight Fight
        {
            get { return Fighter.Fight; }
        }

        public Cell GetFreeAdjacentCell()
        {
            var cell = Fighter.Position.Point.GetAdjacentCells(CellInformationProvider.IsCellWalkable).FirstOrDefault();

            return cell != null ? CellInformationProvider.GetCellInformation(cell.CellId).Cell : null;
        }

        private void OnPositionChanged(ContextActor arg1, ObjectPosition arg2)
        {
            ResetMoveZone();
        }

        public void ResetMoveZone()
        {
            m_moveZone = null;
        }

        public Cell GetCellToCastSpell(TargetCell target, Spell spell, bool LoS, bool nearFirst = true)
        {
            var moveZone = GetMoveZone();
            Set castRange;
            if (spell.CurrentSpellLevel.CastInLine || spell.CurrentSpellLevel.CastInDiagonal)
                castRange = new CrossSet(target.Point, Fighter.GetSpellRange(spell.CurrentSpellLevel),
                    spell.CurrentSpellLevel.MinRange != 0 ? (int) spell.CurrentSpellLevel.MinRange : CellInformationProvider.IsCellWalkable(target.Cell.Id) ? 0 : 1)
                {
                    Diagonal = spell.CurrentSpellLevel.CastInDiagonal,
                    AllDirections = spell.CurrentSpellLevel.CastInLine && spell.CurrentSpellLevel.CastInDiagonal
                };
            else
             castRange = new LozengeSet(target.Point, Fighter.GetSpellRange(spell.CurrentSpellLevel), 
                spell.CurrentSpellLevel.MinRange != 0 ? (int)spell.CurrentSpellLevel.MinRange : CellInformationProvider.IsCellWalkable(target.Cell.Id) ? 0 : 1);

            var intersection = castRange.EnumerateValidPoints().Intersect(moveZone);

            var closestPoint = intersection.Where(x => Fight.Cells[x.CellId].Walkable && 
                (target.Direction == DirectionFlagEnum.ALL_DIRECTIONS || target.Direction == DirectionFlagEnum.NONE || (x.OrientationTo(target.Point).GetFlag() & target.Direction) != 0) &&
                (!LoS || Fight.CanBeSeen(x, MapPoint.GetPoint(target.Cell), false, Fighter))).
                OrderBy(x => (nearFirst ? 1 : -1)*x.ManhattanDistanceTo(Fighter.Position.Point)).FirstOrDefault();

            return closestPoint != null ? Fighter.Fight.Cells[closestPoint.CellId] : null;
        }

        public IEnumerable<Cell> GetCellsWithLoS(Cell target)
        {
            return GetCellsWithLoS(target, new LozengeSet(Fighter.Position.Point, Fighter.MP));
        }

        public IEnumerable<Cell> GetCellsWithLoS(Cell target, Set searchZone)
        {            
            foreach (var cell in searchZone.EnumerateValidPoints())
            {
                if (Fight.CanBeSeen(cell, MapPoint.GetPoint(target)))
                    yield return Fight.Cells[cell.CellId];
            }
        }

        public MapPoint[] GetMoveZone()
        {
            return m_moveZone ?? (m_moveZone = Pathfinding.FindReachableCells(Fighter.Position.Point, Fighter.MP));
        }

        public bool TryToReach(MapPoint point, out Path path)
        {
            var dist = Fighter.Position.Point.ManhattanDistanceTo(point);

            // todo : take in account teleport spells
            if (dist > Fighter.MP)
            {
                path = null;
                return false;
            }

            path = Pathfinding.FindPath(Fighter.Position.Point, point, false, Fighter.MP);
            return path.EndCell.Id == point.CellId;
        }

        public Cell GetCellToFlee()
        {
            var rand = new AsyncRandom();
            var movementsCells = GetMovementCells();
            var fighters = Fight.GetAllFighters(entry => entry.IsEnnemyWith(Fighter));

            var currentCellIndice = fighters.Sum(entry => entry.Position.Point.ManhattanDistanceTo(Fighter.Position.Point)); 
            Cell betterCell = null;
            long betterCellIndice = 0;
            foreach (var c in movementsCells)
            {
                if (!CellInformationProvider.IsCellWalkable(c.Id))
                    continue;

                var indice = fighters.Sum(entry => entry.Position.Point.ManhattanDistanceTo(new MapPoint(c)));

                if (betterCellIndice < indice)
                {
                    betterCellIndice = indice;
                    betterCell = c;
                }
                else if (betterCellIndice == indice && rand.Next(2) == 0)
                    // random factory
                {
                    betterCellIndice = indice;
                    betterCell = c;
                }
            }

            return currentCellIndice == betterCellIndice ? Fighter.Cell : betterCell;
        }

        public Cell[] GetMovementCells()
        {
            return GetMovementCells(Fighter.MP);
        }

        public Cell[] GetMovementCells(int mp)
        {
            if (mp <= 0)
                return new Cell[0];

            if (mp > 63)
                return Fight.Map.Cells;

            var circle = new Lozenge(0, (byte) mp);

            return circle.GetCells(Fighter.Cell, Fight.Map);
        }

        public FightActor GetNearestFighter()
        {
            return GetNearestFighter(entry => true);
        }

        public FightActor GetNearestAlly()
        {
            return GetNearestFighter(entry => entry.IsFriendlyWith(Fighter) && entry != Fighter);
        }

        public FightActor GetNearestEnemy()
        {
            return GetNearestFighter(entry => entry.IsEnnemyWith(Fighter));
        }

        public FightActor GetNearestFighter(Predicate<FightActor> predicate)
        {
            return Fight.GetAllFighters(entry => predicate(entry) && Fighter.CanSee(entry)).
                OrderBy(entry => entry.Position.Point.ManhattanDistanceTo(Fighter.Position.Point)).FirstOrDefault();
        }

        public List<FightActor> GetNearestFighters(Predicate<FightActor> predicate)
        {
            return Fight.GetAllFighters(entry => predicate(entry) && Fighter.CanSee(entry)).
                OrderBy(entry => entry.Position.Point.ManhattanDistanceTo(Fighter.Position.Point)).ToList();
        }

        public IEnumerable<FightActor> GetVisibleEnemies()
        {
            return Fighter.OpposedTeam.GetAllFighters(entry => entry.IsVisibleFor(Fighter));
        }
    }
}