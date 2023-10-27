﻿using System.Collections.Generic;
using System.Linq;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.TaxCollectors;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items.Player;

namespace Stump.Server.WorldServer.Game.Items.TaxCollector
{
    public class TaxCollectorItem : PersistantItem<TaxCollectorItemRecord>
    {
        #region Constructors

        public TaxCollectorItem(TaxCollectorItemRecord record)
            : base(record)
        {
        }

        public TaxCollectorItem(TaxCollectorNpc owner, int guid, ItemTemplate template, List<EffectBase> effects, uint stack)
        {
            Record = new TaxCollectorItemRecord // create the associated record
                         {
                             Id = guid,
                             OwnerId = owner.GlobalId,
                             Template = template,
                             Stack = stack,
                             Effects = effects,
                         };
        }

        #endregion

        #region Functions

        public bool MustStackWith(TaxCollectorItem compared)
        {
            return (compared.Template.Id == Template.Id &&
                    compared.Effects.CompareEnumerable(Effects));
        }

        public bool MustStackWith(BasePlayerItem compared)
        {
            return (compared.Template.Id == Template.Id &&
                    compared.Effects.CompareEnumerable(Effects));
        }

        public override ObjectItem GetObjectItem()
        {
            return new ObjectItem((int)CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED,
                (ushort)Template.Id, Effects.Select(x => x.GetObjectEffect()).ToArray(), (uint)Guid, (uint)Stack);
        }

        public ObjectItemQuantity GetObjectItemQuantity()
        {
            return new ObjectItemQuantity((uint)Guid, (uint)Stack);
        }

        public ObjectItemGenericQuantity GetObjectItemGenericQuantity()
        {
            return new ObjectItemGenericQuantity((ushort)Template.Id, (uint)Stack);
        }

        #endregion
    }
}
