using System;
using System.Linq;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Database.Quests;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Exchanges.Craft;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;

namespace Stump.Server.WorldServer.Game.Quests.Objectives
{
    public class CraftItemObjective : QuestObjective
    {
        private int m_completion;

        public CraftItemObjective(Character character, QuestObjectiveTemplate template, bool finished, int itemId, int amount)
            : base(character, template, finished)
        {
            Item = ItemManager.Instance.TryGetTemplate(itemId);
            Amount = amount;
        }

        public CraftItemObjective(Character character, QuestObjectiveTemplate template, QuestObjectiveStatus finished, int itemId, int amount)
          : base(character, template, finished)
        {
            Item = ItemManager.Instance.TryGetTemplate(itemId);
            Amount = amount;
        }

        public ItemTemplate Item
        {
            get;
        }

        public int Amount
        {
            get;
        }

        public int Completions
        {
            get { return ObjectiveRecord.Completion; }
            private set
            {
                m_completion = value;
                ObjectiveRecord.Completion = value;
            }
        }

        public override void EnableObjective()
        {
            Character.CraftItem += OnCraftItem;
        }

        private void OnCraftItem(BasePlayerItem item, int amount)
        {
            if (item.Template.Id != Item.Id)
                return;

            if (Completions + amount >= Amount)
            {
                Completions += amount;
                CompleteObjective();
            }
            else
                Completions += amount;
        }

        public override void DisableObjective()
        {
            Character.CraftItem -= OnCraftItem;
        }

        public override QuestObjectiveInformations GetQuestObjectiveInformations()
        {
            return new QuestObjectiveInformationsWithCompletion((ushort)Template.Id, ObjectiveRecord.Status ? false : true, new string[0], (ushort)Completions, (ushort)Amount);
        }

        public override bool CanSee()
        {
            return true;
        }

        public override int Completion()
        {
            return Completions;
        }
    }
}