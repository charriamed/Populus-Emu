//using Stump.Core.Reflection;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.BaseServer.Database;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
//using Stump.Server.WorldServer.Game.Items;
//using Stump.Server.WorldServer.Game.Items.Player;
//using System;
//using System.Linq;

//namespace Stump.Server.WorldServer.Database.Npcs.Replies
//{
//    [Discriminator("ChangeToSerian", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
//    public class ChangeToSerian : NpcReply
//    {
//        public ChangeToSerian(NpcReplyRecord record)
//            : base(record)
//        {
//        }

//        public override bool Execute(Npc npc, Character character)
//        {
         
//            var finditem_ = character.Inventory.TryGetItem(ItemIdEnum.BADGE_DUN_PSYCHOPATHE_9564);
//            var finditem = character.Inventory.TryGetItem(ItemIdEnum.BADGE_DAIGURRAF_2110);

//            if (finditem_ == null || finditem == null){
//                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 252);
//                return false;
//            }

//            else {
//                var item = ItemManager.Instance.CreatePlayerItem(character, (int)ItemIdEnum.ALITON_17019, 5);
//                character.Inventory.RemoveItem(finditem_, 1);
//                character.Inventory.RemoveItem(finditem, 1);
//                character.Inventory.AddItem(item);
//                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 21, 5, (int)ItemIdEnum.ALITON_17019);
//                character.ChangeAlignementSide(AlignmentSideEnum.ALIGNMENT_MERCENARY);
//                character.AlignmentGrade = 1;
//                character.SendServerMessage("Félicitations, vous etes devenus Mercenaire.", System.Drawing.Color.DarkOrange);
//            }

//            return true;
//        }
//    }
//}
