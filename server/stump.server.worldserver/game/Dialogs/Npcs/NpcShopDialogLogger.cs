using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MongoDB.Bson;
using Stump.Server.BaseServer.IPC.Messages;
using Stump.Server.BaseServer.Logging;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Database.Items.Shops;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;

namespace Stump.Server.WorldServer.Game.Dialogs.Npcs
{
    public class NpcShopDialogLogger : NpcShopDialog
    {
        public NpcShopDialogLogger(Character character, Npc npc, IEnumerable<NpcItem> items) : base(character, npc, items)
        {
            Character = character;
            Npc = npc;
            Items = items;
            CanSell = true;
        }

        public NpcShopDialogLogger(Character character, Npc npc, IEnumerable<NpcItem> items, ItemTemplate token) : base(character, npc, items, token)
        {
            Character = character;
            Npc = npc;
            Items = items;
            Token = token;
            CanSell = true;
        }

        public override bool BuyItem(uint itemId, uint amount)
        {
            if (!base.BuyItem(itemId, amount)) 
                return false;

            if (Token == null)
                return true;

            var itemToSell = Items.FirstOrDefault(entry => entry.Item.Id == itemId);

            var message = new TokenMessage();
            message.Token = Character.Inventory.Tokens == null ? 0 : (int)Character.Inventory.Tokens.Stack;
            message.Login = Character.Account.Login;
            IPCAccessor.Instance.SendRequest(message,
                ok => Console.WriteLine("Actualisation effectué des Tokens"),
                error => Console.WriteLine("Actualisation raté des Tokens"));

            var document = new BsonDocument
            {
                { "AcctId", Character.Account.Id },
                { "AcctName", Character.Account.Login },
                { "CharacterId", Character.Id },
                { "CharacterName", Character.Name },
                { "ItemId", itemToSell.ItemId },
                { "ItemName", itemToSell.Item.Name },
                { "Amount", amount },
                { "FinalPrice", (itemToSell.Price * amount) },
                { "Date", DateTime.Now.ToString(CultureInfo.InvariantCulture) }
            };

            return true;
        }
    }
}
