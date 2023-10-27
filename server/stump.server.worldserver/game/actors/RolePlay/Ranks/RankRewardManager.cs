using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Ranks;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Ranks
{
    class RankRewardManager : DataManager<RankRewardManager>
    {
        readonly Dictionary<byte, RankRewardEntry> m_records = new Dictionary<byte, RankRewardEntry>();

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            foreach (
                var record in Database.Query<RankRewardEntry>(RankRewardRelator.FetchQuery))
            {
                m_records.Add((byte)record.Id, record);
            }
        }

        public List<KeyValuePair<byte, RankRewardEntry>> getRewardsByRank(int rankId)
        {
           return m_records.Where(x => x.Value.RankId == rankId).ToList();
        }
    }
}