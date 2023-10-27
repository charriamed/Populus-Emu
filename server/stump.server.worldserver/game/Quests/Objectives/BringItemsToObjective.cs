using System;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Actions;
using Stump.Server.WorldServer.Database.Quests;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Quests.Objectives
{
    public class BringItemsToObjective : QuestObjective
    {
        public BringItemsToObjective(Character character, QuestObjectiveTemplate template, bool finished, int npcId, int amount, int itemId, bool delete)
            : base(character, template, finished)
        {
            Npc = NpcManager.Instance.GetNpcTemplate(npcId);
            Amount = amount;
            Item = ItemManager.Instance.TryGetTemplate(itemId);
            Delete = delete;
        }

        public BringItemsToObjective(Character character, QuestObjectiveTemplate template, QuestObjectiveStatus status, int npcId, int amount, int itemId, bool delete)
           : base(character, template, status)
        {
            Npc = NpcManager.Instance.GetNpcTemplate(npcId);
            Amount = amount;
            Item = ItemManager.Instance.TryGetTemplate(itemId);
            Delete = delete;
        }

        public NpcTemplate Npc
        {
            get;
            set;
        }

        public int Amount
        {
            get;
            set;
        }

        public ItemTemplate Item
        {
            get;
            set;
        }

        public bool Delete
        {
            get;
            set;
        }

        public override void EnableObjective()
        {
            Character.InteractingWith += CharacterOnInteractingWith;
        }

        public override void DisableObjective()
        {
            Character.InteractingWith -= CharacterOnInteractingWith;
        }

        private void CharacterOnInteractingWith(Character character, Npc npc, NpcActionTypeEnum actionType, NpcAction action)
        {
            if (!(action is NpcTalkAction) || npc.Template.Id != Template.Parameter0)
                return;
            var item = character.Inventory.GetItems().FirstOrDefault(x => x.Template == Item && x.Stack >= Amount);
            if (item != null)
            {
                if(Delete)
                    character.Inventory.RemoveItem(item, Amount);

                CompleteObjective();
            }
        }

        public override QuestObjectiveInformations GetQuestObjectiveInformations()
        {
            return new QuestObjectiveInformations((ushort)Template.Id, ObjectiveRecord.Status ? false : true, new string[0]);
        }

        public override bool CanSee()
        {
            return Character.Inventory.GetItems().Any(x => x.Template == Item && x.Stack >= Amount);
        }

        public override int Completion()
        {
            return 0;
        }
    }
}