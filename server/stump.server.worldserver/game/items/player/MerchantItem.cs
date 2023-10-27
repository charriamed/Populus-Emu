﻿using System.Collections.Generic;
using System.Linq;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items.Player
{
    public class MerchantItem : PersistantItem<PlayerMerchantItemRecord>
    {
        #region Fields

        public long Price
        {
            get { return Record.Price; }
            set { Record.Price = value; }
        }

        public uint StackSold
        {
            get { return Record.StackSold; }
            set { Record.StackSold = value; }
        }

        #endregion

        #region Constructors

        public MerchantItem(PlayerMerchantItemRecord record)
            : base(record)
        {
            Record = record;
        }

        public MerchantItem(Character owner, int guid, ItemTemplate template, List<EffectBase> effects, uint stack, uint price)
        {
            Record = new PlayerMerchantItemRecord // create the associated record
            {
                Id = guid,
                OwnerId = owner.Id,
                Template = template,
                Stack = stack,
                Price = price,
                Effects = effects,
            };
        }

        #endregion

        #region Functions

        public bool MustStackWith(MerchantItem compared)
        {
            return (compared.Template.Id == Template.Id &&
                    compared.Effects.CompareEnumerable(Effects));
        }

        public bool MustStackWith(BasePlayerItem compared)
        {
            return (compared.Template.Id == Template.Id &&
                    compared.Effects.CompareEnumerable(Effects));
        }

        public ObjectItemToSell GetObjectItemToSell()
        {
            return new ObjectItemToSell((ushort)Template.Id,
                                 Effects.Select(x => x.GetObjectEffect()).ToArray(),
                                 (uint)Guid, (uint)Stack, (uint)Price);
        }

        public override ObjectItem GetObjectItem()
        {
            return new ObjectItem(63, (ushort)Template.Id,
                Effects.Select(x => x.GetObjectEffect()).ToArray(), (uint)Guid,
                (uint)Stack);
        }

        public ObjectItemToSellInHumanVendorShop GetObjectItemToSellInHumanVendorShop()
        {
            return new ObjectItemToSellInHumanVendorShop((ushort)Template.Id,
                                 Effects.Select(x => x.GetObjectEffect()).ToArray(),
                                 (uint)Guid, (uint)Stack, (uint)Price, (uint)Template.Price);
        }

        #endregion
    }
}