using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using System;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("AddItem", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    [Discriminator("Additem", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class AddItemReply : NpcReply
    {
        private ItemTemplate m_itemTemplate;

        public int ItemId
        {
            get
            {
                return this.Record.GetParameter<int>(0U, false);
            }
            set
            {
                this.Record.SetParameter<int>(0U, value);
            }
        }

        public ItemTemplate Item
        {
            get
            {
                ItemTemplate itemTemplate;
                if ((itemTemplate = this.m_itemTemplate) == null)
                    itemTemplate = this.m_itemTemplate = Singleton<ItemManager>.Instance.TryGetTemplate(this.ItemId);
                return itemTemplate;
            }
            set
            {
                this.m_itemTemplate = value;
                this.ItemId = value.Id;
            }
        }

        public uint Amount
        {
            get
            {
                return this.Record.GetParameter<uint>(1U, false);
            }
            set
            {
                this.Record.SetParameter<uint>(1U, value);
            }
        }

        public AddItemReply(NpcReplyRecord record)
            : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            bool flag;
            if (!base.Execute(npc, character)){
                flag = false;

            }else{
                var item = ItemManager.Instance.CreatePlayerItem(character, ItemId, (int)Amount, true);
                //BasePlayerItem playerItem = Singleton<ItemManager>.Instance.CreatePlayerItem(character, , false);
                character.Inventory.AddItem(item);
                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, Amount, ItemId);
                flag = true;
            }
            return flag;
        }
    }
}