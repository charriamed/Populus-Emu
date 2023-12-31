﻿using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Database.Spells
{
    public class SpellVariantTemplateRelator
    {
        public static string FetchQuery = "SELECT * FROM spells_variants";
    }
    [TableName("spells_variants")]
    [D2OClass("SpellVariant", "com.ankamagames.dofus.datacenter.spells")]
    public class SpellVariant : IAssignedByD2O, IAutoGeneratedRecord
    {
        private string m_spellidsCSV;
        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        public uint BreedId
        {
            get;
            set;
        }

        public string SpellsIdsCSV
        {
            get
            { return m_spellidsCSV; }
            set
            {
                m_spellidsCSV = value;
            }
        }

        [Ignore]
        public List<uint> SpellIds
        {
            get
            {
                return m_spellidsCSV.FromCSV<uint>(",").ToList();
            }
            set
            {
                m_spellidsCSV = value.ToCSV(",");
            }
        }


        public void AssignFields(object d2oObject)
        {
            var variant = (Stump.DofusProtocol.D2oClasses.SpellVariant)d2oObject;
            Id = variant.Id;
            BreedId = variant.BreedId;
            SpellIds = variant.SpellIds;
        }
    }
}
