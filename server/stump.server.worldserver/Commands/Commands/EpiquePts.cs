//using System.Drawing;
//using Stump.Core.Attributes;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.BaseServer.Commands;
//using Stump.Server.WorldServer.Commands.Trigger;
//using Stump.Server.WorldServer.Game;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

//namespace Stump.Server.WorldServer.Commands.Commands
//{
//    class EpiquePts : CommandBase
//    {
//        public EpiquePts()
//        {
//            Aliases = new[] { "PE", "Epiquepoint" };
//            Description = "Afficher tes points épiques";
//            RequiredRole = RoleEnum.Player;
//        }

//        public override void Execute(TriggerBase trigger)
//        {
//            var gameTrigger = trigger as GameTrigger;
//            if (gameTrigger != null)
//            {
//                var player = gameTrigger.Character;
//                player.SendServerMessage(" tu as cumulé <b>" + player.CharacterExpEpique + "</b> PE.");
//            }
//        }
//    }
//}