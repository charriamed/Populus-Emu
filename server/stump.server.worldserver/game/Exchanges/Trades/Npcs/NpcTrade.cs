#region License GNU GPL
// NpcTrade.cs
// 
// Copyright (C) 2013 - BehaviorIsManaged
// 
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation;
// either version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details. 
// You should have received a copy of the GNU General Public License along with this program; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#endregion

using System;
using System.Globalization;
using System.Linq;
using MongoDB.Bson;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Logging;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Players;
using Stump.Server.WorldServer.Handlers.Inventory;
using System.Collections.Generic;
using Database.Npcs.Actions;

namespace Stump.Server.WorldServer.Game.Exchanges.Trades.Npcs
{
    public class NpcTrade : Trade<PlayerTrader, NpcTrader>
    {
        public NpcTrade(Character character, Npc npc, List<List<ItemList>> itemsToTake, List<List<ItemList>> itemsToReceive)
        {
            FirstTrader = new PlayerTrader(character, this);
            SecondTrader = new NpcTrader(npc, this);
            ItemsToTake = itemsToTake;
            ItemsToReceive = itemsToReceive;
        }

        public override ExchangeTypeEnum ExchangeType
        {
            get { return ExchangeTypeEnum.NPC_TRADE; }
        }

        public List<List<ItemList>> ItemsToReceive
        {
            get; set;
        }

        public int Kamas
        {
            get;
            set;
        }

        public int RateItem
        {
            get;
            set;
        }

        public List<List<ItemList>> ItemsToTake
        {
            get; set;
        }
        public override void Open()
        {
            base.Open();
            FirstTrader.Character.SetDialoger(FirstTrader);

            InventoryHandler.SendExchangeStartOkNpcTradeMessage(FirstTrader.Character.Client, this);
        }

        public override void Close()
        {
            base.Close();

            InventoryHandler.SendExchangeLeaveMessage(FirstTrader.Character.Client, DialogTypeEnum.DIALOG_EXCHANGE,
                                                      FirstTrader.ReadyToApply);

            FirstTrader.Character.CloseDialog(this);
        }

        protected override void Apply()
        {
            // check all items are still there
            if (!FirstTrader.Items.All(x =>
            {
                var item = FirstTrader.Character.Inventory.TryGetItem(x.Guid);

                return item != null && item.Stack >= x.Stack && !item.IsEquiped();
            }))
            {
                return;
            }

            //Check if kamas still here
            if (FirstTrader.Character.Inventory.Kamas < FirstTrader.Kamas)
                return;

            FirstTrader.Character.Inventory.SetKamas(FirstTrader.Character.Inventory.Kamas + (SecondTrader.Kamas - FirstTrader.Kamas));


            // trade items
            foreach (var tradeItem in FirstTrader.Items)
            {
                var item = FirstTrader.Character.Inventory.TryGetItem(tradeItem.Guid);
                FirstTrader.Character.Inventory.RemoveItem(item, (int)tradeItem.Stack);
            }

            foreach (var tradeItem in SecondTrader.Items)
            {
                FirstTrader.Character.Inventory.AddItem(tradeItem.Template, (int)tradeItem.Stack);
            }

            InventoryHandler.SendInventoryWeightMessage(FirstTrader.Character.Client);

            var document = new BsonDocument
                    {
                        { "NpcId", SecondTrader.Npc.TemplateId },
                        { "PlayerId", FirstTrader.Character.Id },
                        { "PlayerName", FirstTrader.Character.Name },
                        { "NpcKamas", (long)SecondTrader.Kamas },
                        { "PlayerItems", FirstTrader.ItemsString },
                        { "NpcItems", SecondTrader.ItemsString },
                        { "Date", DateTime.Now.ToString(CultureInfo.InvariantCulture) }
                    };

            MongoLogger.Instance.Insert("NpcTrade", document);

        }

        protected override void OnTraderReadyStatusChanged(Trader trader, bool status)
        {
            base.OnTraderReadyStatusChanged(trader, status);

            InventoryHandler.SendExchangeIsReadyMessage(FirstTrader.Character.Client,
                                                        trader, status);

            int quantity = 0;

            if (trader is PlayerTrader && status)
            {
                if (HasAllItems(out var goodRecipe, out quantity))
                {
                    NpcPutAllItem(goodRecipe, quantity);
                    SecondTrader.ToggleReady(true);
                }
            }
        }

        private void NpcPutAllItem(List<ItemList> recipe, int quantity)
        {
            foreach (var item in recipe)
            {
                var itm = SecondTrader.Items.FirstOrDefault(x => x.Template.Id == item.Item.Id);

                if (itm == null)
                {
                    continue; // UNE COUILLE
                }

                if (itm.Stack != item.Quantiy * quantity)
                    itm.Stack = (uint)(item.Quantiy * quantity);
            }
        }

        private bool HasAllItems(out List<ItemList> correctRecipe, out int quantity)
        {
            correctRecipe = null;
            quantity = 0;

            foreach (var itemsRecipe in ItemsToTake)
            {
                if (RecipeIsCorrect(itemsRecipe, out quantity))
                {
                    correctRecipe = itemsRecipe;
                    return true;
                }
            }

            return false;
        }

        private bool RecipeIsCorrect(List<ItemList> itemList, out int quantity)
        {
            quantity = 99;

            if (itemList.Count == 0 || itemList.Count != FirstTrader.Items.Count)
                return false;

            foreach (var i in itemList)
            {
                var item = FirstTrader.Items.FirstOrDefault(x => x.Template.Id == i.Item.Id);

                if (item == null)
                    return false;

                double cf = (double)(item.Stack / i.Quantiy);

                if (cf < 1)
                    return false;

                int qt = (int)Math.Truncate(cf);

                if (qt < quantity)
                    quantity = qt;
            }

            return true;
        }

        protected override void OnTraderItemMoved(Trader trader, TradeItem item, bool modified, int difference)
        {
            base.OnTraderItemMoved(trader, item, modified, difference);

            if (SecondTrader.Items.Count > 0 && trader != SecondTrader)
            {
                this.SecondTrader.Clear();
            }

            if (item.Stack == 0 || difference == 0)
            {
                InventoryHandler.SendExchangeObjectRemovedMessage(FirstTrader.Character.Client, trader != FirstTrader, item.Guid);
                if (trader == SecondTrader)
                    return;
            }
            else if (modified)
            {
                InventoryHandler.SendExchangeObjectModifiedMessage(FirstTrader.Character.Client, trader != FirstTrader, item);
            }
            else
            {
                InventoryHandler.SendExchangeObjectAddedMessage(FirstTrader.Character.Client, trader != FirstTrader, item);
            }

            if (HasAllItems(out var goodRecipe, out var quantity) && trader != SecondTrader)
            {
                int index = ItemsToTake.IndexOf(goodRecipe);

                foreach (var itm in ItemsToReceive[index])
                {
                    this.SecondTrader.AddItem(itm.Item, (uint)(itm.Quantiy * quantity));
                }
            }
        }

        protected override void OnTraderKamasChanged(Trader trader, ulong amount)
        {
            base.OnTraderKamasChanged(trader, amount);

            InventoryHandler.SendExchangeKamaModifiedMessage(FirstTrader.Character.Client, trader != FirstTrader,
                                                             amount);
        }
    }
}