using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Actions;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Npcs;
using Stump.Server.WorldServer.Database.Items.Templates;
using System.Collections.Generic;
using Stump.Core.IO;
using ServiceStack;
using System.Linq;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Exchanges;

namespace Database.Npcs.Actions
{
    [Discriminator("Exchange", typeof(NpcActionDatabase), new System.Type[]
   {
        typeof(NpcActionRecord)
   })]
    class NpcExchangeAction : NpcActionDatabase
    {

        public override NpcActionTypeEnum[] ActionType
        {
            get
            {
                return new[] { NpcActionTypeEnum.ACTION_EXCHANGE };
            }
        }
        public string ItemsToTakeCSV
        {
            get { return Record.GetParameter<string>(0, true); }
            set { Record.SetParameter(0, value); }
        }

        public string ItemsToReceiveCSV
        {
            get { return Record.GetParameter<string>(1, true); }
            set { Record.SetParameter(1, value); }
        }

        private List<List<ItemList>> m_itemsToTake;
        public List<List<ItemList>> ItemsToTake
        {
            get
            {
                if (m_itemsToTake == null)
                {
                    m_itemsToTake = new List<List<ItemList>>();

                    if (ItemsToTakeCSV.Contains("|"))
                    {
                        var arrayItem = ItemsToTakeCSV.Split('|');

                        foreach (var itm in arrayItem)
                        {
                            List<ItemList> listItm = itm.FromCSV<string>(";").Select(x => new ItemList(int.Parse(x.Split(',')[1]), ItemManager.Instance.TryGetTemplate(int.Parse(x.Split(',')[0])))).ToList();
                            m_itemsToTake.Add(listItm);
                        }

                    }
                    else
                    {
                        List<ItemList> listItm = ItemsToTakeCSV.FromCSV<string>(";").Select(x => new ItemList(int.Parse(x.Split(',')[1]), ItemManager.Instance.TryGetTemplate(int.Parse(x.Split(',')[0])))).ToList();
                        m_itemsToTake.Add(listItm);
                    }
                }
                return m_itemsToTake;
            }
        }

        private List<List<ItemList>> m_itemsToReceive;

        public List<List<ItemList>> ItemsToReceive
        {
            get
            {
                if (m_itemsToReceive == null)
                {
                    m_itemsToReceive = new List<List<ItemList>>();

                    if (ItemsToReceiveCSV.Contains("|"))
                    {
                        var arrayItem = ItemsToReceiveCSV.Split('|');

                        foreach (var itm in arrayItem)
                        {
                            List<ItemList> listItm = itm.FromCSV<string>(";").Select(x => new ItemList(int.Parse(x.Split(',')[1]), ItemManager.Instance.TryGetTemplate(int.Parse(x.Split(',')[0])))).ToList();
                            m_itemsToReceive.Add(listItm);
                        }

                    }
                    else
                    {
                        List<ItemList> listItm = ItemsToReceiveCSV.FromCSV<string>(";").Select(x => new ItemList(int.Parse(x.Split(',')[1]), ItemManager.Instance.TryGetTemplate(int.Parse(x.Split(',')[0])))).ToList();
                        m_itemsToReceive.Add(listItm);
                    }
                }
                return m_itemsToReceive;
            }
        }

        public NpcExchangeAction(NpcActionRecord record) : base(record)
        {
        }
        public override void Execute(Npc npc, Character character)
        {
            NpcTrade dialog = new NpcTrade(character, npc, ItemsToTake, ItemsToReceive);
            dialog.Open();
        }
    }

    public class ItemList
    {
        public int Quantiy { get; set; }
        public ItemTemplate Item { get; set; }

        public ItemList(int q, ItemTemplate item)
        {
            Quantiy = q;
            Item = item;
        }
    }
}