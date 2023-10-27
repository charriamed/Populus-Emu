﻿using System;
using System.Collections;
using System.Collections.Generic;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Jobs;
using System.Linq;
using Stump.Core.Collections;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Database.Items.Craft;
using Stump.Server.WorldServer.Database;
using Stump.DofusProtocol.D2oClasses;

namespace Stump.Server.WorldServer.Game.Jobs
{
    public class JobManager : DataManager<JobManager>, ISaveable
    {
        public const int MAX_JOB_LEVEL_GAP = 100;
        public const int WEIGHT_BONUS_PER_LEVEL = 12;
        public const double WEIGHT_BONUS_DECREASE = 1/200d;

        private Dictionary<int, JobTemplate> m_jobTemplates;
        private Dictionary<int, RecipeRecord> m_recipeRecords;
        private List<CraftItemRecord> m_historyRecords = new List<CraftItemRecord>();

        [Initialization(InitializationPass.Fifth)]
        public void Initialize()
        {
            m_jobTemplates = Database.Query<JobTemplate>(JobTemplateRelator.FetchQuery).ToDictionary(x => x.Id);

            m_recipeRecords = Database.Query<RecipeRecord>(RecipeRelator.FetchQuery).ToDictionary(x => x.Id);

            m_historyRecords = Database.Query<CraftItemRecord>(CraftItemRelator.FetchQuery).ToList();

            var secretRecipes = Database.Query<SecretRecipeRecord>(SecretRecipeRelator.FetchQuery).ToDictionary(x => x.Id);

            foreach(var recipe in secretRecipes)
            {
                if (m_recipeRecords.ContainsKey(recipe.Key))
                    m_recipeRecords[recipe.Key] = recipe.Value;
                else
                    m_recipeRecords.Add(recipe.Key, recipe.Value);
            }

            World.Instance.RegisterSaveableInstance(this);
        }

        public CraftItemRecord GetCraftItemById(int id, int ownerId)
        {
            WorldServer.Instance.IOTaskPool.EnsureContext();
            return Database.Query<CraftItemRecord>(string.Format(CraftItemRelator.FetchById, id, ownerId)).FirstOrDefault();
        }

        public void RegisterCraft(int itemId, int amount, int ownerId)
        {
            CraftItemRecord record;
            var existant = m_historyRecords.FirstOrDefault(x => x.ItemId == itemId && x.OwnerId == ownerId);
            if (existant != null)
            {
                if(existant.Amount + amount > 1000000)
                    m_historyRecords.FirstOrDefault(x => x.ItemId == itemId && x.OwnerId == ownerId).Amount = 1000000;
                else
                    m_historyRecords.FirstOrDefault(x => x.ItemId == itemId && x.OwnerId == ownerId).Amount += amount;
            }
            else
            {
                record = new CraftItemRecord()
                {
                    ItemId = itemId,
                    Amount = amount,
                    OwnerId = ownerId,
                };
                m_historyRecords.Add(record);
            }
        }

        public int GetAmountOfCraft(int ownerId, int itemId)
        {
            int amount = 0;
            if(itemId == 0)
            {
                foreach(var item in m_historyRecords.Where(x => x.OwnerId == ownerId))
                {
                    amount += item.Amount;
                }
            }
            else if(m_historyRecords.FirstOrDefault(x => x.ItemId == itemId && x.OwnerId == ownerId) != null)
            {
                amount = m_historyRecords.FirstOrDefault(x => x.ItemId == itemId && x.OwnerId == ownerId).Amount;
            }

            return amount;
        }

        public IReadOnlyDictionary<int, RecipeRecord> Recipes => m_recipeRecords;

        public JobTemplate GetJobTemplate(int id)
        {
            return m_jobTemplates.TryGetValue(id, out var job) ? job : null;
        }

        public JobRecord[] GetCharacterJobs(int characterId)
        {
            return Database.Query<JobRecord>(string.Format(JobRecordRelator.FetchByOwner, characterId)).ToArray();
        }

        public InteractiveSkillTemplate[] GetJobSkills(int jobId)
        {
            return InteractiveManager.Instance.SkillsTemplates.Values.Where(x => x.ParentJobId == jobId).ToArray();
        }

        public JobTemplate[] GetJobTemplates() => m_jobTemplates.Values.ToArray();
        public IEnumerable<JobTemplate> EnumerateJobTemplates() => m_jobTemplates.Values;
        

        public int GetHarvestJobXp(int minLevel)
        {
            return (int)Math.Floor(5 + minLevel / 10d);
        }

        public Pair<int, int> GetHarvestItemMinMax(JobTemplate job, int jobLevel, InteractiveSkillTemplate skillTemplate)
        {
            if (skillTemplate.LevelMin > jobLevel)
                return new Pair<int, int>(0, 0);

            if (skillTemplate.LevelMin == 200 || job.HarvestedCountMax == 0)
                return new Pair<int, int>(1, 1);

            return new Pair<int, int>(Math.Max(1, jobLevel / 20),
                (int) (job.HarvestedCountMax + ((jobLevel - skillTemplate.LevelMin)/10)));
        }

        public int GetWeightBonus(int lastLevel, int newLevel)
        {
            // sum(WEIGHT_BONUS_PER_LEVEL - WEIGHT_BONUS_DECREASE*newLevel) from lastLevel + 1 to newLevel
            // approx WEIGHT_BONUS_PER_LEVEL*diff - WEIGHT_BONUS_DECREASE * (diff*(diff+1) / 2 + lastLevel*diff) + diff/2

            var diff = newLevel - lastLevel;
            int sum = 0;
            for (int i = lastLevel + 1; i < newLevel+1; i++)
            {
                sum += Math.Max(1, (int)Math.Ceiling(WEIGHT_BONUS_PER_LEVEL - WEIGHT_BONUS_DECREASE * i));
            }
            return sum;

            // return (int)(WEIGHT_BONUS_PER_LEVEL * diff - WEIGHT_BONUS_DECREASE * (diff * (diff + 1) / 2 + lastLevel * diff));
        }

        public void Save()
        {
            Database.BeginTransaction();
            var dbIds = m_historyRecords;

            foreach (var id in dbIds.Distinct())
            {
                CraftItemRecord record = GetCraftItemById(id.ItemId, id.OwnerId);
                if(record != null)
                {
                    record.Amount = id.Amount;
                    Database.Save(record);
                }
                else
                {
                    record = new CraftItemRecord()
                    {
                        ItemId = id.ItemId,
                        Amount = id.Amount,
                        OwnerId = id.OwnerId,
                    };
                    Database.Insert(record);
                }
            }

            Database.CompleteTransaction();
        }
    }
}