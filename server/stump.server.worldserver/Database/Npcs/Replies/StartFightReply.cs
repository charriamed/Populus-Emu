using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Stump.Core.IO;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Dopple;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Dopple;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("StartFight", typeof(NpcReply), typeof(NpcReplyRecord))]
    internal class StartFightReply : NpcReply
    {
        public StartFightReply(NpcReplyRecord record) : base(record)
        {
        }

        public string MonstersCSV
        {
            get { return Record.GetParameter<string>(0); }
            set { Record.SetParameter(0, value); }
        }

        public string[] Monsters
        {
            get { return MonstersCSV.Split('|'); }
        }

        public override bool Execute(Npc npc, Character character)
        {
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)3);

            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));

            var group = new MonsterGroup(0, position);

            foreach (var monsterToAdd in Monsters)
            {
                var toAdd = monsterToAdd.Split(',');
                var monsterGradeId = Convert.ToInt32(toAdd[1]);
                var monsterId = Convert.ToInt32(toAdd[0]);

                var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(monsterId, monsterGradeId);

                var monster = new Monster(grade, group);
                
                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
                
            }
            
            fight.StartPlacement();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            return true;
        }
    }
}