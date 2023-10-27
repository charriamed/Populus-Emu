using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Attributes;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Handlers.Inventory;
using Stump.DofusProtocol.Types;
using Stump.DofusProtocol.Enums;

namespace Stump.Server.WorldServer.Game.Items.Player
{
    public class MountInventory : ItemsStorage<MountItem>
    {
        public MountInventory(Character character)
        {
            Owner = character;
        }

        public void LoadRecord()
        {
            Items = WorldServer.Instance.DBAccessor.Database.Query<MountItemRecord>(string.Format(MountItemRelator.FetchByOwner,
                    Owner.EquippedMount.Id)).ToDictionary(x => x.Id, x => new MountItem(Owner, x));
        }

        public Character Owner
        {
            get;
        }

        public override ulong Kamas
        {
            get { return Owner.Client.WorldAccount.BankKamas; }
            protected set { Owner.Client.WorldAccount.BankKamas = value; }
        }

        public MountItem StoreItem(BasePlayerItem item, int amount, bool sendMessage)
        {
            if (!Owner.Inventory.HasItem(item) || amount <= 0)
                return null;

            if (item.IsLinkedToPlayer())
                return null;

            if (item.IsEquiped())
                return null;

            if (amount > item.Stack)
                amount = (int)item.Stack;

            Owner.Inventory.RemoveItem(item, amount, sendMessage: sendMessage);

            var mountItem = ItemManager.Instance.CreateMountItem(Owner, item, amount);
            mountItem = AddItem(mountItem, sendMessage);

            return mountItem;
        }

        public bool StoreItems(IEnumerable<int> guids, bool all, bool existing)
        {
            var mountItems = new List<MountItem>();
            var deletedItems = new List<BasePlayerItem>();

            foreach (var item in Owner.Inventory.Where(x => guids.Contains(x.Guid) || (existing && Items.Values.Any(y => y.Template.Id == x.Template.Id)) || all).ToArray())
            {
                var mountItem = StoreItem(item, (int)item.Stack, false);
                if (mountItem == null)
                    continue;

                mountItems.Add(mountItem);
                deletedItems.Add(item);
            }

            InventoryHandler.SendObjectsDeletedMessage(Owner.Client, deletedItems.Select(x => x.Guid));
            InventoryHandler.SendStorageObjectsUpdateMessage(Owner.Client, mountItems);

            InventoryHandler.SendInventoryWeightMessage(Owner.Client);

            return true;
        }

        public bool StoreKamas(ulong kamas)
        {
            if (kamas < 0)
                return false;

            if (Owner.Inventory.Kamas < kamas)
                kamas = Owner.Inventory.Kamas;

            Owner.Inventory.SetKamas(Owner.Inventory.Kamas - kamas);
            AddKamas(kamas);
            return true;
        }

        public BasePlayerItem TakeItemBack(MountItem item, int amount, bool sendMessage)
        {
            if (amount < 0)
                throw new ArgumentException("amount < 0", "amount");

            if (item == null)
                return null;

            if (!HasItem(item))
                return null;

            if (amount > item.Stack)
                amount = (int)item.Stack;

            RemoveItem(item, amount, sendMessage: sendMessage);

            var playerItem = ItemManager.Instance.CreatePlayerItem(Owner, item.Template, amount, new List<EffectBase>(item.Effects));
            playerItem = Owner.Inventory.AddItem(playerItem, sendMessage);

            return playerItem;
        }

        public bool TakeItemsBack(IEnumerable<int> guids, bool all, bool existing)
        {
            var newItems = new List<BasePlayerItem>();
            var deletedItems = new List<MountItem>();

            foreach (var item in Items.Values.Where(x => guids.Contains(x.Guid) || (existing && Owner.Inventory.Any(y => y.Template.Id == x.Template.Id)) || all).ToArray())
            {
                var newItem = TakeItemBack(item, (int)item.Stack, false);
                if (newItem == null)
                    continue;

                deletedItems.Add(item);
                newItems.Add(newItem);
            }

            InventoryHandler.SendStorageObjectsRemoveMessage(Owner.Client, deletedItems.Select(x => x.Guid));
            InventoryHandler.SendObjectsAddedMessage(Owner.Client, newItems.Select(x => x.GetObjectItem()));
            InventoryHandler.SendObjectsQuantityMessage(Owner.Client, newItems.Select(x => new ObjectItemQuantity((uint)x.Guid, (uint)x.Stack)));

            InventoryHandler.SendInventoryWeightMessage(Owner.Client);

            return true;
        }

        public bool TakeKamas(ulong kamas)
        {
            if (kamas < 0)
                return false;

            if (kamas > Kamas)
                kamas = Kamas;

            SubKamas(kamas);
            Owner.Inventory.AddKamas(kamas);

            return true;
        }

        protected override void OnItemAdded(MountItem item, bool sendMessage = true)
        {
            if (sendMessage)
                InventoryHandler.SendStorageObjectUpdateMessage(Owner.Client, item);

            base.OnItemAdded(item, sendMessage);
        }

        protected override void OnItemRemoved(MountItem item, bool sendMessage = true)
        {
            if (sendMessage)
                InventoryHandler.SendStorageObjectRemoveMessage(Owner.Client, item);

            base.OnItemRemoved(item, sendMessage);
        }

        protected override void OnItemStackChanged(MountItem item, int difference)
        {
            InventoryHandler.SendStorageObjectUpdateMessage(Owner.Client, item);

            base.OnItemStackChanged(item, difference);
        }

        protected override void OnKamasAmountChanged(ulong amount)
        {
            InventoryHandler.SendStorageKamasUpdateMessage(Owner.Client, Kamas);

            base.OnKamasAmountChanged(amount);
        }
    }
}