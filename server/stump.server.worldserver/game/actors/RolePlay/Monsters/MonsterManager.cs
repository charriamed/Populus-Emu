using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Monsters;
using MonsterGrade = Stump.Server.WorldServer.Database.Monsters.MonsterGrade;
using MonsterSpawn = Stump.Server.WorldServer.Database.Monsters.MonsterSpawn;
using MonsterSpell = Stump.Server.WorldServer.Database.Monsters.MonsterSpell;
using MonsterSuperRace = Stump.Server.WorldServer.Database.Monsters.MonsterSuperRace;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters
{
    public class MonsterManager : DataManager<MonsterManager>
    {
        private Dictionary<int, MonsterTemplate> m_monsterTemplates;
        private Dictionary<int, List<MonsterSpell>> m_monsterSpells;
        private Dictionary<int, MonsterSpawn> m_monsterSpawns;
        private Dictionary<int, MonsterDungeonSpawn> m_monsterDungeonsSpawns;
        private Dictionary<int, DroppableItem> m_droppableItems;
        private Dictionary<int, MonsterGrade> m_monsterGrades;
        public Dictionary<int, MonsterAgressionRecord> m_monsterAgression;
        private Dictionary<int, MonsterRace> m_monsterRaces;
        private Dictionary<int, MonsterSuperRace> m_monsterSuperRaces;
        private Dictionary<int, MonsterStaticSpawn> m_monsterStaticSpawns;
        private List<MonsterDungeonWaveSpawnEntity> m_dungeonswaves;

        [Initialization(InitializationPass.Sixth)]
        public override void Initialize()
        {
            m_monsterTemplates = Database.Query<MonsterTemplate>(MonsterTemplateRelator.FetchQuery).ToDictionary(entry => entry.Id);
            m_monsterGrades = Database.Query<MonsterGrade>(MonsterGradeRelator.FetchQuery).ToDictionary(entry => entry.Id);
            m_monsterSpells = new Dictionary<int, List<MonsterSpell>>();
            foreach (var spell in Database.Query<MonsterSpell>(MonsterSpellRelator.FetchQuery))
            {
                List<MonsterSpell> list;
                if (!m_monsterSpells.TryGetValue(spell.MonsterGradeId, out list))
                    m_monsterSpells.Add(spell.MonsterGradeId, list = new List<MonsterSpell>());

                list.Add(spell);
            }
            m_monsterSpawns = Database.Query<MonsterSpawn>(MonsterSpawnRelator.FetchQuery).ToDictionary(entry => entry.Id);
            m_monsterDungeonsSpawns = Database
                .Query<MonsterDungeonSpawn, MonsterDungeonSpawnEntity, MonsterGrade, MonsterDungeonSpawn>
                (new MonsterDungeonSpawnRelator().Map, MonsterDungeonSpawnRelator.FetchQuery)
                .ToDictionary(entry => entry.Id);
            m_monsterAgression = Database.Query<MonsterAgressionRecord>(MonsterAgressionRelator.FetchQuery, new object[0]).ToDictionary((MonsterAgressionRecord entry) => entry.Id);
            m_droppableItems = Database.Query<DroppableItem>(DroppableItemRelator.FetchQuery).ToDictionary(entry => entry.Id);
            m_monsterRaces = Database.Query<MonsterRace>(MonsterRaceRelator.FetchQuery).ToDictionary(entry => entry.Id);
            m_monsterSuperRaces = Database.Query<MonsterSuperRace>(MonsterSuperRaceRelator.FetchQuery).ToDictionary(entry => entry.Id);
            m_monsterStaticSpawns = Database
                .Query<MonsterStaticSpawn, MonsterStaticSpawnEntity, MonsterGrade, MonsterStaticSpawn>
                (new MonsterStaticSpawnRelator().Map, MonsterStaticSpawnRelator.FetchQuery)
                .ToDictionary(entry => entry.Id);

            m_dungeonswaves = Database.Query<MonsterDungeonWaveSpawnEntity>(MonsterDungeonWaveSpawnRelator.FetchQuery).ToList();
        }

        public MonsterGrade[] GetMonsterGrades()
        {
            return m_monsterGrades.Values.ToArray();
        }

        public MonsterGrade GetMonsterGrade(int id)
        {
            MonsterGrade result;
            return !m_monsterGrades.TryGetValue(id, out result) ? null : result;
        }

        public MonsterGrade GetMonsterGrade(int monsterId, int grade)
        {
            var template = GetTemplate(monsterId);
            if (template == null)
                return null;

            return template.Grades.Count <= grade - 1 ? null : template.Grades[grade - 1];
        }

        public List<MonsterGrade> GetMonsterGrades(int monsterId)
        {
            return m_monsterGrades.Where(entry => entry.Value.MonsterId == monsterId).Select(entry => entry.Value).ToList();
        }

        public List<MonsterSpell> GetMonsterGradeSpells(int id)
        {
            List<MonsterSpell> list;
            return m_monsterSpells.TryGetValue(id, out list) ? list : new List<MonsterSpell>();
        }

        public List<DroppableItem> GetMonsterDroppableItems(int id)
        {
            return m_droppableItems.Where(entry => entry.Value.MonsterOwnerId == id).Select(entry => entry.Value).ToList();
        }

        public MonsterRace GetMonsterRace(int id)
        {
            MonsterRace result;
            return !m_monsterRaces.TryGetValue(id, out result) ? null : result;
        }

        public MonsterSuperRace GetMonsterSuperRace(int id)
        {
            MonsterSuperRace result;
            return !m_monsterSuperRaces.TryGetValue(id, out result) ? null : result;
        }

        public List<int> GetMonsterIdAgressableBySubArea(int subAreaId)
        {
            return m_monsterAgression.Values.Where(x => x.SubAreaId == subAreaId).Select(x => x.MonsterId).ToList();
        }

        public List<MonsterAgressionRecord> GetMonsterAgressionRecordBySubAreaId(int subAreaId)
        {
            return m_monsterAgression.Values.Where(x => x.SubAreaId == subAreaId).ToList();
        }

        public MonsterAgressionRecord GetMonsterAgressionByMonsterId(int monsterId)
        {
            return m_monsterAgression.Values.Where(x => x.MonsterId == monsterId).FirstOrDefault();
        }

        public MonsterTemplate GetTemplate(int id)
        {
            MonsterTemplate result;
            return !m_monsterTemplates.TryGetValue(id, out result) ? null : result;
        }

        public MonsterTemplate[] GetTemplates()
        {
            return m_monsterTemplates.Values.ToArray();
        }

        public MonsterTemplate GetTemplate(string name, bool ignoreCommandCase)
        {
            return
                m_monsterTemplates.Values.FirstOrDefault(entry => entry.Name.Equals(name,
                                                                                    ignoreCommandCase
                                                                                        ? StringComparison.InvariantCultureIgnoreCase
                                                                                        : StringComparison.InvariantCulture));
        }

        public void AddMonsterSpell(MonsterSpell spell)
        {
            Database.Insert(spell);
            List<MonsterSpell> list;
            if (!m_monsterSpells.TryGetValue(spell.MonsterGradeId, out list))
                m_monsterSpells.Add(spell.MonsterGradeId, list = new List<MonsterSpell>());

            list.Add(spell);
        }

        public void RemoveMonsterSpell(MonsterSpell spell)
        {
            Database.Delete(spell);
            m_monsterSpells.Remove(spell.Id);
        }

        public MonsterSpawn[] GetMonsterSpawns()
        {
            return m_monsterSpawns.Values.ToArray();
        }

        public MonsterDungeonSpawn[] GetMonsterDungeonsSpawns()
        {
            return m_monsterDungeonsSpawns.Values.ToArray();
        }

        public List<MonsterDungeonWaveSpawnEntity> GetMonsterDungeonsWaveSpawnsByMapId(int MapId)
        {
            var djsSpawns = m_monsterDungeonsSpawns.Values.Where(x => x.MapId == MapId).Select(x => x.Id);
            var toreturn = m_dungeonswaves.Where(x => djsSpawns.Contains(x.DungeonSpawnId)).ToList();
            return toreturn;
        }

        public MonsterStaticSpawn[] GetMonsterStaticSpawns()
        {
            return m_monsterStaticSpawns.Values.ToArray();
        }

        public MonsterSpawn GetOneMonsterSpawn(Predicate<MonsterSpawn> predicate)
        {
            return m_monsterSpawns.Values.SingleOrDefault(entry => predicate(entry));
        }

        public void AddMonsterSpawn(MonsterSpawn spawn)
        {
            Database.Insert(spawn);
            m_monsterSpawns.Add(spawn.Id, spawn);
        }

        public void RemoveMonsterSpawn(MonsterSpawn spawn)
        {
            Database.Delete(spawn);
            m_monsterSpawns.Remove(spawn.Id);
        }

        public void AddMonsterDrop(DroppableItem drop)
        {
            Database.Insert(drop);
            m_droppableItems.Add(drop.Id, drop);
        }

        public void RemoveMonsterDrop(DroppableItem drop)
        {
            Database.Delete(drop);
            m_droppableItems.Remove(drop.Id);
        }
    }
}