namespace Stump.Server.WorldServer.Game.Dialogs
{
    public interface IShopDialog : IDialog
    {
        bool BuyItem(uint id, uint quantity);
        bool SellItem(uint id, uint quantity);
    }
}