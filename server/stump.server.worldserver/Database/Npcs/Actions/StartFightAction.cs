//using System;
//using System.Drawing;
//using System.Linq;
//using Stump.Core.Reflection;
//using Stump.DofusProtocol.Enums;
//using Stump.DofusProtocol.Messages;
//using Stump.Server.BaseServer.Database;
//using Stump.Server.WorldServer.Database.Dopple;
//using Stump.Server.WorldServer.Database.Npcs.Actions;
//using Stump.Server.WorldServer.Game.Actors.Fight;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
//using Stump.Server.WorldServer.Game.Dopple;
//using Stump.Server.WorldServer.Game.Fights;
//using Stump.Server.WorldServer.Game.Items;
//using Stump.Server.WorldServer.Game.Maps.Cells;
//using Stump.Server.WorldServer.Handlers.Context;

//namespace Stump.Server.WorldServer.Database.Npcs.Actions
//{
//    [Discriminator("StartFight", typeof(NpcActionDatabase), typeof(NpcActionRecord))]
//    public class StartFightAction : NpcActionDatabase
//    {
//        public override NpcActionTypeEnum[] ActionType
//        {
//            get
//            {
//                return new[] { NpcActionTypeEnum.ACTION_FIGHT };
//            }
//        }
//        public StartFightAction(NpcActionRecord record) : base(record)
//        {
//        }

//        public int MonsterId
//        {
//            get { return Record.GetParameter<int>(0u); }
//            set { Record.SetParameter(0u, value); }
//        }

//        public override void Execute(Npc npc, Character character)
//        {

//            var monsterGradeId = 5;

//            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(MonsterId, monsterGradeId);
//            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
//            var monster = new Monster(grade, new MonsterGroup(0, position));

//            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
//            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
//            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
//            fight.StartPlacement();

//            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
//                new GameFightJoinRequestMessage(character.Fighter.Id, fight.Id));
//            character.SaveLater();
//            return;
//        }
//    }
//}