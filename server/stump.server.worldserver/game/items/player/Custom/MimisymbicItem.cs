﻿using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.MIMIBIOTE)]
    public sealed class MimisymbicItem : BasePlayerItem
    {
        public MimisymbicItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override uint UseItem(int amount = 1, Cell targetCell = null, Character target = null)
        {
            if (Owner.IsInFight())
                return 0;

            Owner.Client.Send(new ClientUIOpenedByObjectMessage(3, (uint)Guid));

            return 0;
        }
    }
}
