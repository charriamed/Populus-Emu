//using System.Drawing;
//using Stump.Core.Attributes;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.BaseServer.Commands;
//using Stump.Server.WorldServer.Commands.Trigger;
//using Stump.Server.WorldServer.Game;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

//namespace Stump.Server.WorldServer.Commands.Commands
//{
//    class RankCommand : CommandBase
//    {
//        public RankCommand()
//        {
//            Aliases = new[] { "rank", "r" };
//            Description = "Afficher des informations sur ton rank actuel";
//            RequiredRole = RoleEnum.Player;
//        }

//        public override void Execute(TriggerBase trigger)
//        {
//            var gameTrigger = trigger as GameTrigger;
//            if (gameTrigger != null)
//            {
//                var player = gameTrigger.Character;
//                player.SendServerMessage("Tu es actuellement au rank de <b>" + player.GetCharacterRankName() + "</b> et tu as cumulé <b>" + player.CharacterRankExp + "</b> CP, avec <b>" + player.CharacterRankWin + "</b> victoire et <b>" + player.CharacterRankLose + " défaite</b>.");
//            }
//        }
//    }
//}