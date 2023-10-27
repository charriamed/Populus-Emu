//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.Database.Items;
//using Stump.Server.WorldServer.Game.Actors.Look;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

//namespace Stump.Server.WorldServer.Game.Items.Player.Custom
//{
//    [ItemId(ItemIdEnum.AMULETTE_DU_KAM_ASSUTRA_323)]
//    public class LookalignBaton : BasePlayerItem
//    {
//        public LookalignBaton(Character owner, PlayerItemRecord record)
//            : base(owner, record)
//        {
//        }

//        public override ActorLook UpdateItemSkin(ActorLook characterLook)
//        {

//            if (IsEquiped() && Owner.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
//            {

//                Owner.CustomLook = ActorLook.Parse("{693|||85}");
//                Owner.CustomLookActivated = true;
//                Owner.RefreshActor();
//                Owner.Spells.LearnSpell(771);

//            }
//            else
//            {
//                Owner.CustomLook = null;
//                Owner.CustomLookActivated = false;
//                Owner.Map.Area.ExecuteInContext(() => Owner.Map.Refresh(Owner));
//                if (Owner.Spells.HasSpell(771))
//                    Owner.Spells.UnLearnSpell(771);
//                Owner.RefreshActor();
              
//            }

//            if (IsEquiped() && Owner.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
//            {

//                Owner.CustomLook = ActorLook.Parse("{694|||80}");
//                Owner.CustomLookActivated = true;
//                Owner.RefreshActor();
//                Owner.Spells.LearnSpell(771);

//            }
//            else
//            {
//                Owner.CustomLook = null;
//                Owner.CustomLookActivated = false;
//                if(Owner.Spells.HasSpell(771))
//                   Owner.Spells.UnLearnSpell(771);
//                Owner.RefreshActor();
//                Owner.Map.Area.ExecuteInContext(() => Owner.Map.Refresh(Owner));

//            }


//            return Owner.Look;
//        }
//    }
//}