using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Quests;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Actors.RolePlay;

namespace Stump.Server.WorldServer.Game.Quests.Objectives
{
    public class DiscoverMapObjective : QuestObjective
    {
        public DiscoverMapObjective(Character character, QuestObjectiveTemplate template, bool finished, int mapId) : base(character, template, finished)
        {
            Map = World.Instance.GetMap(mapId);
        }

        public DiscoverMapObjective(Character character, QuestObjectiveTemplate template, QuestObjectiveStatus finished, int mapId) : base(character, template, finished)
        {
            Map = World.Instance.GetMap(mapId);
        }

        public Map Map
        {
            get;
            set;
        }

        public override bool CanSee()
        {
            return true;
        }

        public override void DisableObjective()
        {
            Character.EnterMap -= CharacterEnterMap;
        }

        public override void EnableObjective()
        {
            Character.EnterMap += CharacterEnterMap;
        }

        private void CharacterEnterMap(RolePlayActor character, Map map)
        {
            if (map != Map)
                return;

            CompleteObjective();
        }

        public override QuestObjectiveInformations GetQuestObjectiveInformations()
        {
            return new QuestObjectiveInformations((ushort)Template.Id, ObjectiveRecord.Status ? false : true, new string[0]);
        }

        public override int Completion()
        {
            return 0;
        }
    }
}
