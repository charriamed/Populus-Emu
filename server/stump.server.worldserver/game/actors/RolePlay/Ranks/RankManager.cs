using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Characters;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    class RankManager : DataManager<RankManager>
    {
        readonly Dictionary<byte, RankTableEntry> m_records = new Dictionary<byte, RankTableEntry>();

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            foreach (
                var record in Database.Query<RankTableEntry>(RankTableRelator.FetchQuery))
            {
                m_records.Add((byte)record.RankId, record);
            }
            
        }

        public RankTableEntry getRecordById(int rankId)
        {
            foreach (var record in m_records)
            {
                if (record.Value.RankId == rankId)
                    return record.Value;
            }
            return null;
        }

        public Dictionary<byte, RankTableEntry> getRanks()
        {
            return m_records;
        }
    }
}
