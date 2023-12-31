﻿using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Core.IO;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Game.Achievements;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.Server.WorldServer.Database.Achievements
{
    [D2OClass("AchievementCategory", "com.ankamagames.dofus.datacenter.quest", true), TableName("achievements_categories")]
    public class AchievementCategoryRecord : IAutoGeneratedRecord, ISaveIntercepter, IAssignedByD2O
    {
        // FIELDS
        private uint[] m_achievementsIds;
        private string m_achievementsIdsCSV;
        private AchievementCategoryRecord m_parent;
        private AchievementTemplate[] m_achievements;

        // PROPERTIES
        [PrimaryKey("Id", false)]
        public uint Id
        {
            get;
            set;
        }
        public uint NameId
        {
            get;
            set;
        }
        public uint ParentId
        {
            get;
            set;
        }
        [Ignore]
        public AchievementCategoryRecord Parent
        {
            get
            {
                if (this.ParentId > 0)
                {
                    if (this.m_parent == null)
                    {
                        this.m_parent = Singleton<AchievementManager>.Instance.TryGetAchievementCategory(this.ParentId);
                    }
                }

                return this.m_parent;
            }
        }
        [NullString]
        public string Icon
        {
            get;
            set;
        }
        public uint Order
        {
            get;
            set;
        }
        [NullString]
        public string Color
        {
            get;
            set;
        }
        [Ignore]
        public uint[] AchievementsIds
        {
            get
            {
                return this.m_achievementsIds;
            }
            set
            {
                this.m_achievementsIds = value;
                this.m_achievementsIdsCSV = value.ToCSV(",");
            }
        }
        [Ignore]
        public AchievementTemplate[] Achievements
        {
            get
            {
                if (this.m_achievements == null)
                {
                    this.m_achievements = new AchievementTemplate[this.m_achievementsIds.Length];
                    for (int i = 0; i < this.m_achievementsIds.Length; i++)
                    {
                        this.m_achievements[i] = Singleton<AchievementManager>.Instance.TryGetAchievement(this.m_achievementsIds[i]);
                    }
                }

                return this.m_achievements;
            }
        }
        public string AchievementsIdsCSV
        {
            get
            {
                return this.m_achievementsIdsCSV;
            }
            set
            {
                this.m_achievementsIdsCSV = value;
                this.m_achievementsIds = value.FromCSV<uint>(",");
            }
        }

        // CONSTRUCTORS

        // METHODS
        public void BeforeSave(bool insert)
        {

        }

        public virtual void AssignFields(object d2oObject)
        {
            var item = (Stump.DofusProtocol.D2oClasses.AchievementCategory)d2oObject;
            this.Id = item.id;
            this.NameId = item.nameId;
            this.ParentId = item.parentId;
            this.Icon = item.icon;
            this.Order = item.order;
            this.Color = item.color;
            this.AchievementsIds = item.achievementIds.ToArray();
        }
    }
}
