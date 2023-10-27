using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Quests;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Actions;

namespace Stump.Server.WorldServer.Game.Quests.Objectives
{
    public class TalkToObjective : QuestObjective
    {
        public TalkToObjective(Character character, QuestObjectiveTemplate template, bool finished, int npcId) : base(character, template, finished)
        {
            Npc = NpcManager.Instance.GetNpcTemplate(npcId);
        }

        public TalkToObjective(Character character, QuestObjectiveTemplate template, QuestObjectiveStatus finished, int npcId) : base(character, template, finished)
        {
            Npc = NpcManager.Instance.GetNpcTemplate(npcId);
        }

        public NpcTemplate Npc
        {
            get;
            set;
        }

        public override bool CanSee()
        {
            return Character.Quests.Any(x => x.CurrentStep.Objectives.Any(y => y.Template.Parameter0 == Npc.Id));
        }

        public override void EnableObjective()
        {
            Character.InteractingWith += CharacterInteractingWith;
        }

        public override void DisableObjective()
        {
            Character.InteractingWith -= CharacterInteractingWith;
        }

        private void CharacterInteractingWith(Character character, Npc npc, DofusProtocol.Enums.NpcActionTypeEnum actionType, Database.Npcs.Actions.NpcAction npcAction)
        {
            if (!(npcAction is NpcTalkAction) || npc.Template.Id != Template.Parameter0)
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
