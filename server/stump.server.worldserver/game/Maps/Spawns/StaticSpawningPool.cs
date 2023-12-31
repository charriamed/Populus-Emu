﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NLog;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Game.Maps.Spawns
{
    public class StaticSpawningPool : SpawningPoolBase
    {
        [Variable(true)]
        public static int StaticSpawnsInterval = 3;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly List<MonsterStaticSpawn> m_spawns = new List<MonsterStaticSpawn>();
        private Queue<MonsterStaticSpawn> m_spawnsQueue = new Queue<MonsterStaticSpawn>();
        private readonly Dictionary<MonsterGroup, MonsterStaticSpawn> m_groupsSpawn = new Dictionary<MonsterGroup, MonsterStaticSpawn>();

        private readonly object m_locker = new object();

        public StaticSpawningPool(Map map)
            : this(map, StaticSpawnsInterval)
        {
        }

        public StaticSpawningPool(Map map, int interval)
            : base(map, interval)
        {
        }

        public ReadOnlyCollection<MonsterStaticSpawn> Spawns
        {
            get { return m_spawns.AsReadOnly(); }
        }

        public void AddSpawn(MonsterStaticSpawn spawn)
        {
            lock (m_locker)
            {
                m_spawns.Add(spawn);
                m_spawnsQueue.Enqueue(spawn);
            }
        }

        public void RemoveSpawn(MonsterStaticSpawn spawn)
        {
            lock (m_locker)
            {
                m_spawns.Remove(spawn);

                var asList = m_spawnsQueue.ToList();
                if (asList.Remove(spawn))
                    m_spawnsQueue = new Queue<MonsterStaticSpawn>(asList);
            }
        }

        protected override void OnGroupSpawned(MonsterGroup group)
        {
            group.EnterFight += OnGroupEnterFight;

            base.OnGroupSpawned(group);
        }

        private void OnGroupEnterFight(MonsterGroup group, Character character)
        {
            group.EnterFight -= OnGroupEnterFight;
            group.Fight.WinnersDetermined += OnWinnersDetermined;
        }

        private void OnWinnersDetermined(IFight fight, FightTeam winners, FightTeam losers, bool draw)
        {
            fight.WinnersDetermined -= OnWinnersDetermined;

            if (draw)
                return;

            // if players didn't win they don't get teleported
            if (!(winners is FightPlayerTeam) || !(losers is FightMonsterTeam))
                return;

            var group = ((MonsterFighter)losers.Leader).Monster.Group;

            if (!m_groupsSpawn.ContainsKey(@group))
            {
                logger.Error("Group {0} (Map {1}) has ended his fight but is not register in the pool", @group.Id, Map.Id);
                return;
            }

            var spawn = m_groupsSpawn[@group];

            if (spawn.TpMap.Id == 0)
                return;

            foreach (var fighter in winners.GetAllFighters<CharacterFighter>())
            {
                var celltotp = spawn.TpCell.Id != 0 ? spawn.TpCell : fighter.Cell;
                var directiontotp = spawn.TpDirection != null ? (DirectionsEnum)spawn.TpDirection : fighter.Direction;
                fighter.Character.NextMap = spawn.TpMap;
                fighter.Character.Cell = celltotp;
                fighter.Character.Direction = directiontotp;


                m_groupsSpawn.Remove(group);
            }
        }

        protected override bool IsLimitReached()
        {
            return m_spawnsQueue.Count == 0;
        }

        protected override int GetNextSpawnInterval()
        {
            return Interval*1000;
        }

        protected override MonsterGroup DequeueNextGroupToSpawn()
        {
            if (!Map.CanSpawnMonsters())
            {
                StopAutoSpawn();
                return null;
            }

            lock (m_locker)
            {
                if (m_spawnsQueue.Count == 0)
                {
                    return null;
                }

                var spawn = m_spawnsQueue.Dequeue();

                var group = new MonsterGroup(Map.GetNextContextualId(), new ObjectPosition(Map, spawn.Cell, (DirectionsEnum)spawn.Direction), this);
                foreach (var monsterGrade in spawn.GroupMonsters)
                {
                    group.AddMonster(new Monster(monsterGrade, group));
                }

                m_groupsSpawn.Add(group, spawn);

                return group;
            }
        }

        protected override void OnGroupUnSpawned(MonsterGroup monster)
        {
            lock (m_locker)
            {
                if (!m_groupsSpawn.ContainsKey(monster))
                {
                    logger.Error("Group {0} (Map {1}) was not bind to a dungeon spawn", monster.Id, Map.Id);
                }
                else
                {
                    var spawn = m_groupsSpawn[monster];

                    if (m_spawns.Contains(spawn))
                        m_spawnsQueue.Enqueue(spawn);
                }
            }

            base.OnGroupUnSpawned(monster);
        }
    }
}
