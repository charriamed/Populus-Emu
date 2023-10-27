using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Exchanges.MountsExchange
{
    public class MountCustomer : Exchanger
    {
        public MountCustomer(Character character, MountDialog dialog)
            : base(dialog)
        {
            Character = character;
        }

        public Character Character
        {
            get;
        }

        public override bool MoveItem(uint id, int quantity)
        {
            if (quantity > 0)
            {
                var item = Character.Inventory.TryGetItem((int)id);

                return item != null && Character.EquippedMount.Inventory.StoreItem(item, quantity, true) != null;
            }

            if (quantity >= 0)
                return false;

            var deleteItem = Character.EquippedMount.Inventory.TryGetItem((int)id);

            return Character.EquippedMount.Inventory.TakeItemBack(deleteItem, -quantity, true) != null;
        }

        public override bool SetKamas(long amount)
        {
            if (amount > 0)
                return Character.Bank.StoreKamas((ulong)amount);

            return amount < 0 && Character.Bank.TakeKamas(-amount);
        }

        public void MoveItems(bool store, IEnumerable<uint> guids, bool all, bool existing)
        {
            var guids_ = new List<int>();
            foreach(var id in guids)
            {
                guids_.Add((int)id);
            }
            if (store)
                Character.EquippedMount.Inventory.StoreItems(guids_, all, existing);
            else
                Character.EquippedMount.Inventory.TakeItemsBack(guids_, all, existing);
        }
    }
}