using Stump.Core.Threading;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Diverse
{
    public class MonsterNani
    {
        public MonsterNani(MonsterNaniRecord record)
        {
            Record = record;
            Spawned = false;

        }

        public MonsterNaniRecord Record
        {
            get;
            set;
        }
        public bool Spawned
        {
            get;
            set;
        }

        public string ZoneName
        {
            get;
            set;
        }
    }

    public class MonsterNaniManager : DataManager<MonsterNaniManager>
    {
        public static int NaniUpdateInterval = 1000; //ON FIX SUR 3 MINUTES POUR TEST
        public static int NaniMatchmakingInterval = 900; //900 INTERVALE EN SECONDES

        private List<MonsterNani> monsterNaniRecords = new List<MonsterNani>();
        readonly SelfRunningTaskPool m_NaniTaskPool = new SelfRunningTaskPool(NaniUpdateInterval, "MonstersNani");

        [Initialization(InitializationPass.Fifth)]
        public override void Initialize()
        {
            monsterNaniRecords = Database.Query<MonsterNaniRecord>(MonsterNaniRelator.FetchQuery).Select(x => new MonsterNani(x)).ToList();
            m_NaniTaskPool.CallPeriodically(NaniMatchmakingInterval * 1000, UsualCHeck);
            m_NaniTaskPool.Start();
        }

        public void UsualCHeck()
        {
            CheckMonstersSpawns();
        }

        public void CheckMonstersSpawns(bool Silent = false)
        {
            foreach(var nani in monsterNaniRecords)
            {
                var monster = MonsterManager.Instance.GetMonsterGrade(nani.Record.MonsterGradeId);
                if (monster == null) continue;

                if (nani.Spawned)
                {
                    if (!Silent) World.Instance.SendAnnounce("'<b>" + monster.Template.Name + "</b>' est disponible dans la zone : <b>" + nani.ZoneName + "</b>.", Color.OrangeRed);
                    continue;
                }

                var map = PickRandomMapInSUbArea(nani.Record.SubAreas);

                var pos = new ObjectPosition(map, map.GetFirstFreeCellNearMiddle());

                var group = map.SpawnMonsterGroup(monster, pos, true);

                nani.Spawned = true;
                nani.ZoneName = map.SubArea.Record.Name;

               if (!Silent) World.Instance.SendAnnounce("'<b>" + monster.Template.Name + "</b>' est disponible dans la zone : <b>" + map.SubArea.Record.Name + "</b>.", Color.OrangeRed);

            }
        }

        public Map PickRandomMapInSUbArea(SubArea[] areas)
        {
            Random r = new Random();
            int pickedareaIndex = r.Next(0, (areas.Count() - 1));
            var pickedarea = areas[pickedareaIndex];

            var AreaMaps = pickedarea.Maps.Where(x => x.HasPriorityOnWorldmap && x.AllowFightChallenges).ToArray();

            int pickedMap = r.Next(0, (AreaMaps.Count() - 1));

            return AreaMaps[pickedMap];
        }

        public void ResetSpawn(MonsterGrade Nani)
        {
            var mn = monsterNaniRecords.FirstOrDefault(x => x.Record.MonsterGradeId == Nani.Id || x.Record.MonsterGradeId == Nani.GradeId);
            if (mn == null) return;
            mn.Spawned = false;
        }
    }
}