using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Exchanges.Craft.Runes
{
    public class MultiRuneCustomer : CraftCustomer
    {
        public MultiRuneCustomer(BaseCraftDialog trade, Character character)
            : base(trade, character)
        {
        }

        public override bool SetKamas(long amount)
        {
            if (amount < 0 || Character.Inventory.Kamas < (ulong)amount)
                return false;

            Kamas = (ulong)amount;

            OnKamasChanged(Kamas);

            return true;
        }
    }
}