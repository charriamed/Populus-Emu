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
    public class FreeFormObjective : QuestObjective
    {
        public FreeFormObjective(Character character, QuestObjectiveTemplate template, bool finished) : base(character, template, finished)
        {
        }

        public FreeFormObjective(Character character, QuestObjectiveTemplate template, QuestObjectiveStatus finished) : base(character, template, finished)
        {
        }

        public override bool CanSee()
        {
            return true;
        }

        public override void DisableObjective()
        {

        }

        public override void EnableObjective()
        {

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
