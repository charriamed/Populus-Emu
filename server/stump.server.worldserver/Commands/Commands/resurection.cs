//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.Commands.Commands.Patterns;
//using Stump.Server.WorldServer.Commands.Trigger;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
//using Stump.Server.WorldServer.Game.Items.Player;
//using System.Linq;

//namespace Stump.Server.WorldServer.Commands.Commands
//{
//    class resurection : InGameCommand
//    {
//        private const int SHOP_POINT = 30;
//        public resurection()
//        {
//            base.Aliases = new[] { "life" };
//            base.Description = "Permet de reprendre vie pour 30 points boutiques.";
//            base.RequiredRole = RoleEnum.Player;
//        }
//        public override void Execute(GameTrigger trigger)
//        {
//            BasePlayerItem pb = trigger.Character.Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED).First(x => x.Template.Id == 12124);
//            if (pb.Stack < SHOP_POINT && trigger.Character.Energy !=0)
//            {
//                trigger.Character.SendServerMessage("Vous n'avez pas assez de points boutique ou vous n'êtes pas mort");
//            }
    
//                Character player = trigger.Character;
//            player.Inventory.UnStackItem(player.Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED).First(x => x.Template.Id == 12124), SHOP_POINT);
//            player.Energy = 10000;
//                player.RefreshStats();
//                player.RefreshActor();
//                player.SaveNow();
               
//                player.SendServerMessage("Vous êtes revenus à la vie"); 

//            }
//        }
//    }




        
