using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Exchanges.Merchant
{
    public class CharacterMerchant : Exchanger
    {
        public CharacterMerchant(Character character, MerchantExchange merchantTrade)
            : base(merchantTrade)
        {
            Character = character;
        }

        public Character Character
        {
            get;
        }

        public override bool MoveItem(uint id, int quantity)
        {
            if (quantity >= 0)
                return false;

            var deleteItem = Character.MerchantBag.TryGetItem((int)id);

            return deleteItem != null && Character.MerchantBag.TakeBack(deleteItem, -quantity);
        }

        public bool MovePricedItem(uint id, int quantity, long price)
        {
            if (quantity <= 0)
                return false;

            var item = Character.Inventory.TryGetItem((int)id);

            return item != null && Character.MerchantBag.StoreItem(item, quantity, price);
        }

        public bool ModifyItem(uint id, int quantity, long price)
        {
            var item = Character.MerchantBag.TryGetItem((int)id);

            return item != null && Character.MerchantBag.ModifyItem(item, quantity, price);
        }

        public override bool SetKamas(long amount) => false;
    }
}