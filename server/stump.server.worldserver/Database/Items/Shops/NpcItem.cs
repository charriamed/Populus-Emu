using System.Linq;
using Stump.Core.Cache;
using Stump.DofusProtocol.Types;
using Stump.ORM;
using System.Collections.Generic;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database.Items.Shops
{
    public class NpcItemRelator
    {
        public static string FetchQuery = "SELECT * FROM npcs_items";

        /// <summary>
        /// Use string.Format
        /// </summary>
        public static string FetchByNpcShop = "SELECT * FROM npcs_items WHERE NpcShopId = {0}";
    }

    [TableName("npcs_items")]
    public class NpcItem : ItemToSell, IAutoGeneratedRecord
    {
        private string m_buyCriterion;
        private double? m_customPrice;

        private static List<int> ignoreActionId = new List<int>()
        {
            808,
            807,
            806,
            119,
            153,
            717,
            10
        };

        public NpcItem()
        {
            m_objectItemToSellInNpcShop = new ObjectValidator<ObjectItemToSellInNpcShop>(BuildObjectItemToSellInNpcShop);
        }

        public int NpcShopId
        {
            get;
            set;
        }

        public double Price
        {
            get { return CustomPrice.HasValue ? CustomPrice.Value : Item.Price; }
        }

        public double? CustomPrice
        {
            get
            {
                if (m_customPrice.HasValue) return m_customPrice; //Si y'a une valeur en bdd �a la met direct.
                else
                {
                int multiplicateur = 0;
                long result = 0;
                if (NpcShopId == 3 | NpcShopId == 7 | NpcShopId == 1 | NpcShopId == 2)
                {
                    if (Item.Level <= 200 && Item.Level > 195)
                        multiplicateur = 21250;
                    else if (Item.Level <= 195 && Item.Level > 150)
                        multiplicateur = 12750;
                    else if (Item.Level <= 150 && Item.Level > 100)
                        multiplicateur = 6800;
                    else if (Item.Level <= 100 && Item.Level > 50)
                        multiplicateur = 3000;
                    else if (Item.Level <= 50)
                        multiplicateur = 1500;
                    result = Item.Level * multiplicateur;
                    CustomPrice = result;
                }
                else if (NpcShopId == 21 | NpcShopId == 23 | NpcShopId == 14 | NpcShopId == 4 | NpcShopId == 5 | NpcShopId == 6 | NpcShopId == 8 && Item.TypeId != 23)
                {
                    if (Item.Level <= 200 && Item.Level > 195)
                        multiplicateur = 75000;
                    else if (Item.Level <= 195 && Item.Level > 150)
                        multiplicateur = 45000;
                    else if (Item.Level <= 150 && Item.Level > 100)
                        multiplicateur = 24000;
                    else if (Item.Level <= 100 && Item.Level > 50)
                        multiplicateur = 12000;
                    else if (Item.Level <= 50)
                        multiplicateur = 6000;
                    result = Item.Level * multiplicateur;
                    CustomPrice = result;
                }
                else if (NpcShopId == 17 && Item.Id != 16333 && Item.Id != 16335)
                {
                    if (Item.Level == 150)
                        multiplicateur = 100000;
                    else if (Item.Level == 100)
                        multiplicateur = 100000;
                    else if (Item.Level == 50)
                        multiplicateur = 100000;
                    result = Item.Level * multiplicateur;
                    CustomPrice = result;
                }
                else if (NpcShopId == 27)
                {
                    if (Item.Level > 100)
                        multiplicateur = 20000;
                    else if (Item.Level <= 100)
                        multiplicateur = 10000;
                    result = multiplicateur;
                    CustomPrice = result;
                }
                return result;
                }
            }
            set
            {
                m_customPrice = value;
                m_objectItemToSellInNpcShop.Invalidate();
            }
        }

        [NullString]
        public string BuyCriterion
        {
            get { return m_buyCriterion; }
            set
            {
                m_buyCriterion = value ?? string.Empty;
                m_objectItemToSellInNpcShop.Invalidate();
            }
        }

        public bool MaxStats
        {
            get;
            set;
        }

        #region ObjectItemToSellInNpcShop

        private readonly ObjectValidator<ObjectItemToSellInNpcShop> m_objectItemToSellInNpcShop;     

        private ObjectItemToSellInNpcShop BuildObjectItemToSellInNpcShop()
        {         
            return new ObjectItemToSellInNpcShop(
               (ushort)Item.Id,
               Item.Effects.Select(entry => entry.GetObjectEffect()).ToArray(),
               (uint)(CustomPrice.HasValue && CustomPrice != 0 ? CustomPrice.Value : Item.Price),
               BuyCriterion ?? string.Empty);          
        }


        public override Item GetNetworkItem()
        {
            return m_objectItemToSellInNpcShop;
        }

        #endregion
    }
}