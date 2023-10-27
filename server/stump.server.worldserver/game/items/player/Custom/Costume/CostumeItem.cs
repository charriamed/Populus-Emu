using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Arena;
using Stump.Server.WorldServer.Handlers.Basic;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.COSTUME)]
    public class CostumeItem : BasePlayerItem
    {
        public CostumeItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override bool OnEquipItem(bool unequip)
        {
            if (unequip)
                return base.OnEquipItem(true);

            short apparence = (short)Record.Template.AppearanceId;

            if (apparence == 0)
                return false;

            Owner.Look.AddSkin(apparence);
            Owner.UpdateLook();

            return true;
        }
    }
}
