﻿using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Game.Maps;

namespace Stump.Server.WorldServer.Database.Arena
{
    public class ArenaRelator_3vs3
    {
        public const string FetchQuery = "SELECT * FROM arenas WHERE Id < 13 ";
    }

    public class ArenaRelator_1vs1
    {
        public const string FetchQuery = "SELECT * FROM arenas WHERE Id >= 13 ";
    }

    [TableName("arenas")]
    public class ArenaRecord : IAutoGeneratedRecord
    {
        private Map m_map;

        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        public int MapId
        {
            get;
            set;
        }

        [Ignore]
        public Map Map
        {
            get { return m_map ?? (m_map = Game.World.Instance.GetMap(MapId)); }
        }
    }
}