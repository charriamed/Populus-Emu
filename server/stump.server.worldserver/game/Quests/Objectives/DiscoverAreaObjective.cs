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
    public class DiscoverAreaObjective : QuestObjective
    {
        public DiscoverAreaObjective(Character character, QuestObjectiveTemplate template, bool finished, int subAreaId) : base(character, template, finished)
        {
            SubArea = World.Instance.GetSubArea(subAreaId);
        }

        public DiscoverAreaObjective(Character character, QuestObjectiveTemplate template, QuestObjectiveStatus finished, int subAreaId) : base(character, template, finished)
        {
            SubArea = World.Instance.GetSubArea(subAreaId);
        }

        public SubArea SubArea
        {
            get;
            set;
        }

        public override bool CanSee()
        {
            return true;
        }


        public override void EnableObjective()
        {
            Character.EnterMap += CharacterEnterMap;
        }


        public override void DisableObjective()
        {
            Character.EnterMap -= CharacterEnterMap;
        }

        private void CharacterEnterMap(RolePlayActor actor, Map map)
        {
            if (map.SubArea != SubArea)
                return;
            CompleteObjective();
        }

        public override QuestObjectiveInformations GetQuestObjectiveInformations()
        {
            return new QuestObjectiveInformations((ushort)Template.Id, ObjectiveRecord.Status == true ? false : true, new string[0]);
        }

        public override int Completion()
        {
            return 0;
        }
    }
}
