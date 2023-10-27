using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Arena;
using Stump.Server.WorldServer.Database.Idols;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Mounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.PvP
{
    public class AlvoMapManager : DataManager<AlvoMapManager>
    {
        private Dictionary<int, AlvoMapRecord> m_alvomaps;

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            m_alvomaps = Database.Query<AlvoMapRecord>(AlvoMapRelator.FetchQuery).ToDictionary(entry => entry.Id);
        }

        public List<Map> GetAllMaps()
        {
            List<Map> m_allmaps = new List<Map>();
            foreach (var maps in m_alvomaps.Values)
            {
                m_allmaps.Add(maps.Map);
            }
            return m_allmaps;
        }

    }
}
