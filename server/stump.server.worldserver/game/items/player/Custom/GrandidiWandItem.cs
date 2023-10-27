using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemId(30681)]
    public class GrandidiWandItem : BasePlayerItem
    {
        public GrandidiWandItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override ActorLook UpdateItemSkin(ActorLook characterLook)
        {
            characterLook.ResetScales();

            foreach (var subLook in characterLook.SubLooks)
                subLook.Look.ResetScales();

            if (IsEquiped())
            {
                characterLook.Rescale(1.4);

                foreach (var subLook in characterLook.SubLooks)
                    subLook.Look.Rescale(1.4);
            }

            return characterLook;
        }
    }
}
