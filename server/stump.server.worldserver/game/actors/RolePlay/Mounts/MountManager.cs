﻿using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Attributes;
using Stump.Core.Mathematics;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Mounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Items.Player.Custom;


namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Mounts
{
    public class MountManager : DataManager<MountManager>
    {
        [Variable]
        public static int MountStorageValidityDays = 40;

        public static TimeSpan MountStorageValidity => TimeSpan.FromDays(MountStorageValidityDays);

        private Dictionary<int, MountTemplate> m_mountTemplates;
        private Dictionary<int, MountRecord> m_mounts;
        private Dictionary<int, HarnessRecord> m_harness;

        [Initialization(InitializationPass.Sixth)]
        public override void Initialize()
        {
            m_mountTemplates = Database.Query<MountTemplate, MountBonus, MountTemplate>(new MountTemplateRelator().Map, MountTemplateRelator.FetchQuery).ToDictionary(entry => entry.Id);
            Database.Execute(string.Format(MountRecordRelator.DeleteStoredSince, (DateTime.Now - MountStorageValidity).ToString("yyyy-MM-dd HH:mm:ss.fff")));
            m_mounts = Database.Query<MountRecord>(MountRecordRelator.FetchQuery).ToDictionary(x => x.Id);
            m_harness = Database.Query<HarnessRecord>(HarnessRelator.FetchQuery).ToDictionary(x => x.ItemId);
        }

        public MountTemplate[] GetTemplates()
        {
            return m_mountTemplates.Values.ToArray();
        }

        public HarnessRecord GetHarness(int id)
        {
            return !m_harness.TryGetValue(id, out HarnessRecord record) ? null : record;
        }

        public MountTemplate GetTemplate(int id)
        {
            return !m_mountTemplates.TryGetValue(id, out var result) ? null : result;
        }

        public MountTemplate GetTemplateByScrollId(int scrollId)
        {
            return m_mountTemplates.FirstOrDefault(x => x.Value.ScrollId == scrollId).Value;
        }

        public void AddMount(MountRecord record)
        {
            if (!m_mounts.ContainsKey(record.Id))
                m_mounts.Add(record.Id, record);
        }

        public void RemoveMount(MountRecord record)
        {
            m_mounts.Remove(record.Id);
        }

        public void SaveMount(MountRecord record)
        {
            if (record.IsNew)
                Database.Insert(record);
            else
                Database.Update(record);

            record.IsDirty = false;
            record.IsNew = false;
        }

        public void DeleteMount(MountRecord record)
        {
            RemoveMount(record);
            Database.Delete(record);
        }

        public MountRecord GetMount(int mountId)
        {
            if (!m_mounts.TryGetValue(mountId, out var record))
                return null;

            return record;
        }

        private static short GetBonusByLevel(int finalBonus, int level)
        {
            return (short)Math.Floor(finalBonus * level / 100d);
        }

        public List<EffectInteger> GetMountEffects(Mount mount)
        {
            return mount.Template.Bonuses.Select(x => new EffectInteger((EffectsEnum)x.EffectId, GetBonusByLevel(x.Amount, mount.Level))).ToList();
        }

        public Mount CreateMount(Character owner, MountTemplate template)
        {
            var rand = new CryptoRandom();
            return CreateMount(owner, template, rand.Next(2) == 1);
        }

        public Mount CreateMount(Character owner, MountTemplate template, bool sex)
        {

            var record = new MountRecord()
            {
                IsNew = true,
                TemplateId = template.Id,
                OwnerId = owner.Id,
                OwnerName = owner.Name,
                Name = "Anonyme",
                Sex = sex,
                Energy = template.EnergyBase + template.EnergyPerLevel - 42,
            };
            record.AssignIdentifier();

            AddMount(record);

            return new Mount(owner, record);
        }

        public BasePlayerItem StoreMount(Character character, Mount mount)
        {
            // null effect bypass initialization
            var item = ItemManager.Instance.CreatePlayerItem(character, mount.ScrollItem, 1, new List<EffectBase> { new EffectBase(-1, new EffectBase()) }) as MountCertificate;

            if (item == null)
                throw new Exception($"Item {mount.ScrollItem} type isn't MountCertificate");

            item.InitializeEffects(mount);
            return character.Inventory.AddItem(item);
        }


    }
}
