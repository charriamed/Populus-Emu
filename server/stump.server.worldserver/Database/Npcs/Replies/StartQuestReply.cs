using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Quests;
using Stump.Server.WorldServer.Handlers.Context.RolePlay;
using System.Linq;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("Quest", typeof(NpcReply), typeof(NpcReplyRecord))]
    public class StartQuestReply : NpcReply
    {
        public StartQuestReply(NpcReplyRecord record)
            : base(record)
        {
        }

        public int StepId
        {
            get { return Record.GetParameter<int>(0); }
            set { Record.SetParameter(0, value); }
        }

        public override bool CanExecute(Npc npc, Character character)
        {
            return base.CanExecute(npc, character) && !character.Quests.Any(x => x.Template.StepIds.Contains(StepId));
        }

        public override bool Execute(Npc npc, Character character)
        {
            if(base.Execute(npc, character))
            {
                character.StartQuest(StepId);
                ContextRoleplayHandler.SendRefreshMapQuestWithout(character.Client, QuestManager.Instance.GetQuestTemplateWithStepId(StepId).Id);
                return true;
            }

            return false;
        }
    }
}