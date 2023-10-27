using Stump.DofusProtocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Database.Companion;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Game.Companions;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom.Companion
{
    [ItemType(ItemTypeEnum.COMPAGNON)]
    public class CompanionSign : BasePlayerItem
    {
        public CompanionSign(Character owner, PlayerItemRecord record) : base(owner, record)
        {
        }
        public override bool OnEquipItem(bool unequip)
        {

            if (Owner.IsInFight() &&
                       Owner.Fight.State == FightState.Placement && !Owner.Team.IsFull())
            {
                if (!unequip)
                {
                    Owner.Team.AddFighter(Owner.CreateCompanion(Owner.Team, Owner.Fighter));
                }
                else
                {
                    Owner.Companion?.LeaveFight();
                }
               
            }

            Owner.Party?.UpdateMember(Owner);
            return base.OnEquipItem(unequip);
            

        }
       
    }
}
