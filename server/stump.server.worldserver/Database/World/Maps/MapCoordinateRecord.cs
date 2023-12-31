﻿using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.DofusProtocol.D2oClasses;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using System.Linq;

namespace Stump.Server.WorldServer.Database.World.Maps
{
    public class MapCoordinateRecordRelator
    {
        public static string FetchQuery = "SELECT * FROM world_maps_coordinates";
    }

    [TableName("world_maps_coordinates")]
    [D2OClass("MapCoordinates", "com.ankamagames.dofus.datacenter.world")]
    public sealed class MapCoordinateRecord : IAssignedByD2O, ISaveIntercepter, IAutoGeneratedRecord
    {
        private int[] m_mapIds;
        private string m_mapIdsCSV;

        [PrimaryKey("CompressedCoords", false)]
        public uint CompressedCoords
        {
            get;
            set;
        }

        public string MapIdsCSV
        {
            get { return m_mapIdsCSV; }
            set
            {
                m_mapIdsCSV = value;
                m_mapIds = value.FromCSV<int>(",");
            }
        }

        [Ignore]
        public int[] MapIds
        {
            get { return m_mapIds; }
            set
            {
                m_mapIds = value;
                m_mapIdsCSV = value.ToCSV(",");
            }
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var map = (MapCoordinates) d2oObject;
            CompressedCoords = map.compressedCoords;
            MapIds = map.mapIds.Select(x => (int)x).ToArray();
        }

        #endregion

        #region ISaveIntercepter Members

        public void BeforeSave(bool insert)
        {
            m_mapIdsCSV = MapIds.ToCSV(",");
        }

        #endregion
    }
}