using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Companion;

namespace Stump.Server.WorldServer.Game.Companions
{
    public class CompanionsManager : DataManager<CompanionsManager>
    {
        public IEnumerable<CompanionRecord> companion;

        public IEnumerable<CompanionRecord> GetCompanionById(int ItemId)
        {
            companion = Database.Fetch<CompanionRecord>(string.Format(CompanionRelator.FetchQuery))
                .Where(x => x.ItemId == ItemId);
            return companion;
        }
    }
}