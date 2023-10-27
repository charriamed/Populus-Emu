﻿using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using NLog;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Mounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Mounts;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.CERTIFICAT_DE_DRAGODINDE)]
    [ItemType(ItemTypeEnum.CERTIFICAT_DE_MULDO)]
    [ItemType(ItemTypeEnum.CERTIFICAT_DE_VOLKORNE)]
    public sealed class MountCertificate : BasePlayerItem
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private EffectMount m_mountEffect;
        private EffectString m_nameEffect;
        private EffectString m_belongsToEffect;
        private EffectDuration m_validityEffect;

        public MountCertificate(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            // default template is used to apply mount effects
            if (Template.Id != MountTemplate.DEFAULT_SCROLL_ITEM)
                Initialize();

            if (record.Stack > 1)
            {
                for (int i = 0; i < record.Stack - 1; i++)
                {
                    Owner.Inventory.AddItem(Template);
                }

                record.Stack = 1;
            }

        }

        public override uint Stack
        {
            get { return Math.Min(Record.Stack, 1); }
            set { Record.Stack = Math.Min(value, 1); }
        }

        public Mount Mount
        {
            get;
            private set;
        }

        private void Initialize()
        {
            if (Effects.Count > 0)
            {
                // hack to bypass initialization (see MountManager.StoreMount)
                if (Effects.Any(x => x.Id == -1))
                    return;

                m_mountEffect = Effects.OfType<EffectMount>().FirstOrDefault();
                m_nameEffect = Effects.OfType<EffectString>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_Name);
                m_belongsToEffect = Effects.OfType<EffectString>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_BelongsTo);
                m_validityEffect = Effects.OfType<EffectDuration>().FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_Validity);

                if (m_mountEffect == null)
                {
                    logger.Error($"Invalid certificate mount effect absent");
                    CreateMount();
                    return;
                }

                // invalid certificate
                //if (m_mountEffect.Date < DateTime.Now - MountManager.MountStorageValidity)
                //    return;

                var record = MountManager.Instance.GetMount(m_mountEffect.MountId);

                if (record == null) // mount has been deleted, the certificate isn't valid anymore
                    return;

                Mount = new Mount(Owner, record);

            }
            else
                CreateMount();
        }

        public void InitializeEffects(Mount mount)
        {
            if (Effects.Count > 0)
                Effects.Clear();

            Effects.Add(m_mountEffect = new EffectMount(EffectsEnum.Effect_ViewMountCharacteristics, mount.Id,
                DateTimeOffset.Now.AddDays(MountManager.MountStorageValidityDays).ToUnixTimeMilliseconds(), (uint)mount.Template.Id, mount.Name, mount.Owner.Name, mount.Level,
                mount.Sex, true, false, false, mount.ReproductionCount, (uint)mount.ReproductionCountMax,
                mount.Effects.ToList(), new List<uint>() { }));
            if (mount.Owner != null)
                Effects.Add(m_belongsToEffect = new EffectString(EffectsEnum.Effect_BelongsTo, mount.Owner.Name));

            Mount = mount;
            mount.StoredSince = DateTime.Now;
            Owner.SetOwnedMount(mount);
        }

        private void CreateMount()
        {
            var template = MountManager.Instance.GetTemplateByScrollId(Template.Id);

            if (template == null)
            {
                logger.Error($"Cannot generate mount associated to scroll {Template.Name} ({Template.Id}) there is no matching mount template");
                Owner.Inventory.RemoveItem(this);
                return;
            }

            var mount = MountManager.Instance.CreateMount(Owner, template);
            InitializeEffects(mount);
        }

        public int? MountId => (m_mountEffect ?? (m_mountEffect = Effects.OfType<EffectMount>().FirstOrDefault()))?.MountId;

        public bool CanConvert()
        {
            return Mount != null && m_mountEffect != null /*&& m_mountEffect.Date + MountManager.MountStorageValidity > DateTime.Now*/;
        }

        public override ObjectItem GetObjectItem()
        {
            if (m_validityEffect != null && m_mountEffect != null)
            {
                var validity = m_mountEffect.Date + MountManager.MountStorageValidity - DateTime.Now;
                m_validityEffect.Update(validity > TimeSpan.Zero ? validity : TimeSpan.Zero);
            }

            return base.GetObjectItem();
        }

        public override void OnPersistantItemAdded()
        {
            if (Mount != null)
                MountManager.Instance.SaveMount(Mount.Record);
        }

        public override bool OnRemoveItem()
        {
            if (Mount != null)
                Mount.StoredSince = null;
            return base.OnRemoveItem();
        }
    }
}