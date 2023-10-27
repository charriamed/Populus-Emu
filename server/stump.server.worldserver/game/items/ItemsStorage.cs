using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System;

namespace Stump.Server.WorldServer.Game.Items
{
    public class ItemsStorage<T> : PersistantItemsCollection<T>
        where T : IPersistantItem
    {
        public event Action<ItemsStorage<T>, ulong> KamasAmountChanged;

        private void NotifyKamasAmountChanged(ulong kamas)
        {
            OnKamasAmountChanged(kamas);
            KamasAmountChanged?.Invoke(this, kamas);
        }

        protected virtual void OnKamasAmountChanged(ulong amount)
        {
        }

        public ulong AddKamas(ulong amount, Character c = null)
        {
            if (amount == 0)
                return 0;

            return SetKamas(Kamas + amount);
        }

        public ulong SubKamas(ulong amount)
        {
            if (amount == 0)
                return 0;

            return SetKamas(Kamas - (ulong)amount);
        }

        public virtual ulong SetKamas(ulong amount)
        {
            var oldKamas = Kamas;

            if (amount < 0)
                amount = 0;

            Kamas = amount;
            NotifyKamasAmountChanged(amount - oldKamas);
            return amount - oldKamas;
        }

        public virtual ulong Kamas
        {
            get;
            protected set;
        }
    }
}