using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Game.Exchanges.MountsExchange
{
    public class MountDialog : IExchange
    {
        public MountDialog(Character character)
        {
            Customer = new MountCustomer(character, this);
        }

        public Character Character => Customer.Character;

        public MountCustomer Customer
        {
            get;
        }

        public ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.MOUNT;

        public DialogTypeEnum DialogType => DialogTypeEnum.DIALOG_EXCHANGE;

        public void Open()
        {
            InventoryHandler.SendExchangeStartedMountStockMessage(Character.Client, Character.EquippedMount.Inventory);
            Character.SetDialoger(Customer);
        }

        public void Close()
        {
            InventoryHandler.SendExchangeLeaveMessage(Character.Client, DialogType, false);
            Character.ResetDialog();
        }
    }
}