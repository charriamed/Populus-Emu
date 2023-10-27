using MongoDB.Bson;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Logging;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.Trades;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Players;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Handlers.Inventory;
using System.Linq;
namespace Stump.Server.WorldServer.Game.Exchanges
{
    public class NpcTradese : Trade<PlayerTrader, NpcTrader>
    {
        public int Kamas
        {
            get;
            set;
        }
        public int ItemIdToReceive
        {
            get;
            set;
        }
        public int ItemIdToGive
        {
            get;
            set;
        }
        public int RateItem
        {
            get;
            set;
        }

        public override ExchangeTypeEnum ExchangeType
        {
            get
            {
                return ExchangeTypeEnum.NPC_TRADE;
            }
        }
        public NpcTradese(Character character, Npc npc, int kamas, int itemToGive/*, int itemToReceive, int rateItem*/)
        {
            Kamas = kamas;
            ItemIdToGive = itemToGive;
            base.FirstTrader = new PlayerTrader(character, this);
            base.SecondTrader = new NpcTrader(npc, this);
        }
        public override void Open()
        {
            base.Open();
            base.FirstTrader.Character.SetDialoger(base.FirstTrader);
            InventoryHandler.SendExchangeStartOkNpcTradeMessage(base.FirstTrader.Character.Client, this);
        }
        public override void Close()
        {
            base.Close();
            InventoryHandler.SendExchangeLeaveMessage(base.FirstTrader.Character.Client, DialogTypeEnum.DIALOG_EXCHANGE, base.FirstTrader.ReadyToApply);
            base.FirstTrader.Character.CloseDialog(this);
        }
        protected override void Apply()
        {
            if (base.FirstTrader.Items.All(delegate (TradeItem x)
            {
                BasePlayerItem basePlayerItem = base.FirstTrader.Character.Inventory.TryGetItem(x.Guid);
                return basePlayerItem != null && basePlayerItem.Stack >= x.Stack;
            }))
            {
                base.FirstTrader.Character.Inventory.SetKamas((ulong)(base.FirstTrader.Character.Inventory.Kamas + (base.SecondTrader.Kamas - base.FirstTrader.Kamas)));
                foreach (TradeItem current in base.FirstTrader.Items.Where(x => x.Template.Id == ItemIdToGive))
                {
                    BasePlayerItem item = base.FirstTrader.Character.Inventory.TryGetItem(current.Guid);
                    base.FirstTrader.Character.Inventory.RemoveItem(item, amount: (int)current.Stack, delete: true);
                }
                foreach (TradeItem current in base.SecondTrader.Items)
                {
                    base.FirstTrader.Character.Inventory.AddItem(current.Template, amount: (int)current.Stack);
                }
                InventoryHandler.SendInventoryWeightMessage(base.FirstTrader.Character.Client);
                BsonDocument document = new BsonDocument
                {

                    {
                        "NpcId",
                        base.SecondTrader.Npc.TemplateId
                    },

                    {
                        "PlayerId",
                        base.FirstTrader.Id
                    },

                    {
                        "NpcKamas",
                        (long)((ulong)base.SecondTrader.Kamas)
                    },

                    {
                        "PlayerItems",
                        base.FirstTrader.ItemsString
                    },

                    {
                        "NpcItems",
                        base.SecondTrader.ItemsString
                    },

                    {
                        "Date",
                        System.DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    }
                };
                Singleton<MongoLogger>.Instance.Insert("NpcTrade", document);
            }
        }
        protected override void OnTraderReadyStatusChanged(Trader trader, bool status)
        {
            base.OnTraderReadyStatusChanged(trader, status);
            InventoryHandler.SendExchangeIsReadyMessage(base.FirstTrader.Character.Client, trader, status);
            if (trader is PlayerTrader && status)
            {
                base.SecondTrader.ToggleReady(true);
            }
        }
        protected override void OnTraderItemMoved(Trader trader, TradeItem item, bool modified, int difference)
        {
            if (item.Template.Id != ItemIdToGive)
                return;

            base.OnTraderItemMoved(trader, item, modified, difference);
            if (item.Stack == 0u)
            {
                if (trader is PlayerTrader && ItemIdToReceive != 0 && RateItem != 0 && item.Template.Id == ItemIdToGive)
                {
                    foreach (var @object in SecondTrader.Items)
                    {
                        SecondTrader.RemoveItem(@object.Template, @object.Stack);
                    }
                }
                if (trader is PlayerTrader)
                {

                    SecondTrader.SetKamas(0);
                }
                InventoryHandler.SendExchangeObjectRemovedMessage(base.FirstTrader.Character.Client, trader != base.FirstTrader, item.Guid);
            }
            else
            {
                if (modified)
                {
                    InventoryHandler.SendExchangeObjectModifiedMessage(base.FirstTrader.Character.Client, trader != base.FirstTrader, item);
                }
                else
                {
                    InventoryHandler.SendExchangeObjectAddedMessage(base.FirstTrader.Character.Client, trader != base.FirstTrader, item);
                }

                if (trader is PlayerTrader && ItemIdToReceive != 0 && RateItem != 0 && item.Template.Id == ItemIdToGive)
                {
                    foreach (var @object in SecondTrader.Items)
                    {
                        SecondTrader.RemoveItem(@object.Template, @object.Stack);
                    }
                    SecondTrader.AddItem(ItemManager.Instance.TryGetTemplate(ItemIdToReceive), (uint)(item.Stack / RateItem));
                }
                if (trader is PlayerTrader)
                {
                    var items = FirstTrader.Items.FirstOrDefault(x => x.Template.Id == ItemIdToGive);
                    if (items != null)
                    {
                        SecondTrader.SetKamas((int)(Kamas * items.Stack));
                    }
                    else
                    {
                        SecondTrader.SetKamas(0);
                    }
                }
            }

        }
        protected override void OnTraderKamasChanged(Trader trader, ulong amount)
        {
            base.OnTraderKamasChanged(trader, amount);
            InventoryHandler.SendExchangeKamaModifiedMessage(base.FirstTrader.Character.Client, trader != base.FirstTrader, amount);
        }
    }
}
