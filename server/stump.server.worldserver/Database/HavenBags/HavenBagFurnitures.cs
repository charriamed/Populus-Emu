﻿using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Database.HavenBags
{
    public class HavenBagFurnituresRelator
    {
        public static string FetchQuery = "SELECT * FROM havenbags_furnitures_source";
    }
    [TableName("havenbags_furnitures_source")]
    [D2OClass("HavenbagFurniture", "com.ankamagames.dofus.datacenter.houses")]
    public class HavenBagFurnitures : IAssignedByD2O, IAutoGeneratedRecord
    {
        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }
        
        public int TypeId
        {
            get;
            set;
        }

        public int ThemeId
        {
            get;
            set;
        }

        public int ElementId
        {
            get;
            set;
        }

        public int Color
        {
            get;
            set;
        }

        public int SkillId
        {
            get;
            set;
        }

        public int LayerId
        {
            get;
            set;
        }

        public bool BlocksMovement
        {
            get;
            set;
        }

        public bool IsStackable
        {
            get;
            set;
        }

        public uint CellsWidth
        {
            get;
            set;
        }

        public uint CellsHeight
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
            var havenbagfurniture = (Stump.DofusProtocol.D2oClasses.HavenbagFurniture)d2oObject;

            TypeId = havenbagfurniture.TypeId;
            ThemeId = havenbagfurniture.ThemeId;
            ElementId = havenbagfurniture.ElementId;
            Color = havenbagfurniture.Color;
            SkillId = havenbagfurniture.SkillId;
            LayerId = havenbagfurniture.LayerId;
            BlocksMovement = havenbagfurniture.BlocksMovement;
            IsStackable = havenbagfurniture.IsStackable;
            CellsHeight = havenbagfurniture.CellsHeight;
            CellsWidth = havenbagfurniture.CellsWidth;
            Order = havenbagfurniture.Order;
        }
    }
}
