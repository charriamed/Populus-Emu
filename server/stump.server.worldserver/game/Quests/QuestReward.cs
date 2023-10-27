using Stump.Server.WorldServer.Database.Quests;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;

namespace Stump.Server.WorldServer.Game.Quests
{
    public class QuestReward
    {
        public QuestReward(QuestRewardTemplate template)
        {
            Template = template;
        }

        public QuestRewardTemplate Template
        {
            get;
            set;
        }

        public void GiveReward(Character character)
        {
            for (int i = 0; i < Template.ItemsReward.Length; i++)
            {
                character.Inventory.AddItem(ItemManager.Instance.TryGetTemplate(Template.ItemsReward[i]), Template.ItemQuantitiesReward[i]);
            }
        }
    }
}