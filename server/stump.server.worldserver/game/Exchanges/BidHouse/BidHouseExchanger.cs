﻿using System;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.BidHouse;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Game.Exchanges.BidHouse
{
    public class BidHouseExchanger : Exchanger
    {
        public BidHouseExchanger(Character character, BidHouseExchange exchange)
            : base(exchange)
        {
            Character = character;
        }

        public Character Character
        {
            get;
            private set;
        }

        public override bool MoveItem(uint id, int quantity)
        {
            var item = BidHouseManager.Instance.GetBidHouseItem((int)id);

            if (item == null)
                return false;

            BidHouseManager.Instance.RemoveBidHouseItem(item);

            var newItem = ItemManager.Instance.CreatePlayerItem(Character, item.Template, (int)item.Stack,
                                                                       item.Effects);

            Character.Inventory.AddItem(newItem);

            InventoryHandler.SendExchangeBidHouseItemRemoveOkMessage(Character.Client, item.Guid);

            return true;
        }

        public bool MovePricedItem(uint id, int quantity, long price)
        {
            if (!BidHouseManager.Quantities.Contains(quantity))
                return false;

            var item = Character.Inventory.TryGetItem((int)id);

            if (item == null)
                return false;

            //if (item.IsLinkedToPlayer() || item.IsLinkedToAccount())
            //    return false;

            if (item.Template.Level > ((BidHouseExchange) Dialog).MaxItemLevel)
                return false;

            if (BidHouseManager.Instance.GetBidHouseItems(Character.Account.Id, ((BidHouseExchange)Dialog).Types).Count() >= Character.Level)
                return false;

            if (quantity > item.Stack)
                quantity = (int)item.Stack;

            ulong tax = (ulong)Math.Round((price * BidHouseManager.TaxPercent) / 100);

            if (Character.Kamas < (ulong)tax)
            {
                //Vous ne disposez pas d'assez de kamas pour acquiter la taxe de mise en vente...
                Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 57);
                return false;
            }

            Character.Inventory.SubKamas(tax);

            var bidItem = BidHouseManager.Instance.CreateBidHouseItem(Character, item, quantity, price);
            BidHouseManager.Instance.AddBidHouseItem(bidItem);

            Character.Inventory.RemoveItem(item, quantity);

            InventoryHandler.SendExchangeBidHouseItemAddOkMessage(Character.Client, bidItem.GetObjectItemToSellInBid());

            return true;
        }

        public bool ModifyItem(uint id, long price)
        {
            var item = BidHouseManager.Instance.GetBidHouseItem((int)id);

            if (item == null)
                return false;

            if (item.Template.Level > ((BidHouseExchange)Dialog).MaxItemLevel)
                return false;

            var diff = (item.Price - price);
            ulong tax = 0;

            tax = (ulong)(diff < 0 ? (int)Math.Round((Math.Abs(diff) * BidHouseManager.TaxPercent) / 100) : (int)Math.Round((Math.Abs(price) * BidHouseManager.TaxModificationPercent) / 100));

            if (Character.Kamas < tax)
            {
                //Vous ne disposez pas d'assez de kamas pour acquiter la taxe de mise en vente...
                Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 57);
                return false;
            }

            Character.Inventory.SubKamas(tax);
            item.Price = price;

            InventoryHandler.SendExchangeBidHouseItemRemoveOkMessage(Character.Client, item.Guid);
            InventoryHandler.SendExchangeBidHouseItemAddOkMessage(Character.Client, item.GetObjectItemToSellInBid());

            return true;
        }

        public override bool SetKamas(long amount)
        {
            return false;
        }
    }
}
