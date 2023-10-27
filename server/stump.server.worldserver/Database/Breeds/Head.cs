﻿#region License GNU GPL
// Head.cs
// 
// Copyright (C) 2013 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion

using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database.Breeds
{
    public class HeadRelator
    {
        public static string FetchQuery = "SELECT * FROM breeds_heads";
    }

    [TableName("breeds_heads")]
    [D2OClass("Head")]
    public class Head : IAssignedByD2O, IAutoGeneratedRecord, ISaveIntercepter
    {
        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        private short[] m_skins;

        [Ignore]
        public short[] Skins
        {
            get { return m_skins; }
            set { m_skins = value;
            m_skinsCSV = value.ToCSV(",");
            }
        }

        private string m_skinsCSV;

        public string SkinsCSV
        {
            get { return m_skinsCSV; }
            set { m_skinsCSV = value;
            m_skins = value.FromCSV<short>(",");
            }
        }

        public string AssetId
        {
            get;
            set;
        }

        public uint Breed
        {
            get;
            set;
        }

        public uint Gender
        {
            get;
            set;
        }

        public uint Order
        {
            get;
            set;
        }

        public void AssignFields(object d2oObject)
        {
            var head = (Stump.DofusProtocol.D2oClasses.Head)d2oObject;
            Id = head.id;
            SkinsCSV = head.skins;
            AssetId = head.assetId;
            Breed = head.breed;
            Gender = head.gender;
            Order = head.order;
        }

        public void BeforeSave(bool insert)
        {
            m_skinsCSV = m_skins.ToCSV(",");
        }
    }
}