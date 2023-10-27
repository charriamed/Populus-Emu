using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Dungeon;
using Stump.Server.WorldServer.Database.Items.Templates;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Dungeon
{
    public class DungeonItemsManager : DataManager<DungeonItemsManager>
    {
        private Dictionary<int, DungeonItemsRecord> m_dungeonItemsTemplate;

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            m_dungeonItemsTemplate = Database.Query<DungeonItemsRecord>(DungeonItemsRelator.FetchQuery).ToDictionary(entry => entry.ItemId);
        }

        public List<ItemTemplate> GetItems()
        {
            List<ItemTemplate> m_dungeonItemsTemplates = new List<ItemTemplate>();

            foreach (var item in m_dungeonItemsTemplate.Values)
                m_dungeonItemsTemplates.Add(item.ItemTemplate);

            return m_dungeonItemsTemplates;
        }

    }
}
