using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Mounts;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.PvP
{
    public class CranioManager : DataManager<CranioManager>
    {
        private Dictionary<int, CranioRecord> m_cranioTemplate;

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            m_cranioTemplate = Database.Query<CranioRecord>(CranioRelator.FetchQuery).ToDictionary(entry => entry.BreedId);
        }

        public int GetCranioByBreed(int id)
        {
            return !m_cranioTemplate.TryGetValue(id, out var result) ? 0 : result.ItemId;
        }

        public List<ItemTemplate> GetCranios()
        {
           List<ItemTemplate> m_craniosTemplates = new List<ItemTemplate>();
           foreach(var cranio in m_cranioTemplate.Values){
                m_craniosTemplates.Add(cranio.ItemTemplate);
            }
            return m_craniosTemplates;
        }

    }
}
