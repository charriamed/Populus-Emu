//using Stump.Core.Reflection;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.Commands.Commands.Patterns;
//using Stump.Server.WorldServer.Commands.Trigger;
//using Stump.Server.WorldServer.Game;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
//using System;
//namespace Stump.Server.WorldServer.Commands.Commands
//{
//    public class StaffListCommand : InGameCommand
//    {
//        public StaffListCommand()
//        {
//            base.Aliases = new string[]
//            {
//                "staff"
//            };
//            base.RequiredRole = RoleEnum.Player;
//            base.Description = "Affiche le staff connectés";
//        }
//        public override void Execute(GameTrigger trigger)
//        {
//            System.Predicate<Character> predicate = (Character x) => true;

//            System.Collections.Generic.IEnumerable<Character> characters = Singleton<World>.Instance.GetCharacters(predicate);
//            int num = 0;
//            foreach (Character current in characters)
//            {
//                if (current.Account.UserGroupId > 1)
              
//                {
//                    trigger.ReplyBold(" - {0} - {1}", new object[]
//                    {
//                    current.Name,
//                    current.Account.UserGroupId.ToString()
//                    });
//                }
//                num++;
//                if (num >= WhoCommand.DisplayedElementsLimit)
//                {
//                    trigger.Reply("(...)");
//                    break;
//                }
//            }

//        }
//    }
//}
