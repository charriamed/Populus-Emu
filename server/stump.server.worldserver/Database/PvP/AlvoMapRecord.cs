﻿using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Game.Maps;

namespace Stump.Server.WorldServer.Database.Arena
{
    public class AlvoMapRelator
    {
        public const string FetchQuery = "SELECT * FROM alvos_maps";
    }

    [TableName("alvos_maps")]
    public class AlvoMapRecord : IAutoGeneratedRecord
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