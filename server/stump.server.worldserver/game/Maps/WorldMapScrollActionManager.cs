using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Database.World;

namespace Stump.Server.WorldServer.Game.Maps
{
    class WorldMapScrollActionManager : DataManager<WorldMapScrollActionManager>
    {
        readonly Dictionary<int, WorldMapScrollActionRecord> m_records = new Dictionary<int, WorldMapScrollActionRecord>();

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            foreach (
                var record in Database.Query<WorldMapScrollActionRecord>(WorldMapScrollActionRelator.FetchQuery))
            {
                m_records.Add((int)record.Id, record);
            }

        }

        public WorldMapScrollActionRecord GetWorldMapScroll(Map map)
        {
            foreach (var record in m_records)
            {
                if (record.Key == map.Id)
                    return record.Value;
            }
            return null;
        }
    }
}