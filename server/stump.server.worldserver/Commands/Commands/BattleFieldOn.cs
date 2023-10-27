
//using System;
//using System.Drawing;
//using Stump.Core.Attributes;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.BaseServer.Commands;
//using Stump.Server.WorldServer.Commands.Trigger;
//using Stump.Server.WorldServer.Game;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

//namespace Stump.Server.WorldServer.Commands.Commands
//{
//    class BattleFieldOn : CommandBase
//    {
//        public BattleFieldOn()
//        {
//            Aliases = new[] { "defi", "dm" };
//            Description = "Active ou désactive la recherche automatique de combat Dead Match.";
//            RequiredRole = RoleEnum.Player;
//        }

//        public override void Execute(TriggerBase trigger)
//        {
//            var gameTrigger = trigger as GameTrigger;
//            if (gameTrigger.Character.Level > 59)
//            if (gameTrigger != null)
//            {
//                var player = gameTrigger.Character;
//                if (player.AgressionPenality >= DateTime.Now)
//                {
//                    player.SendServerMessage("Vous devez attendre de ne plus être banni pour chercher un combat en Dead Match.", Color.DarkRed);
//                    return;
//                }
//                if (player.battleFieldOn)
//                {
//                    player.battleFieldOn = false;
//                    player.SendServerMessage("Le mode DeadMatch est désactivé.", Color.Blue);
//                } else if (!player.battleFieldOn)
//                {
//                    player.battleFieldOn = true;
//                    player.SendServerMessage("Le mode DeadMatch est activé.", Color.Blue);
//                    player.OpenPopup("Le mode DeadMatch : Dans le mode DeadMatch deux joueurs s'affrontent - le gagnant gagne un item équipé du perdant qui lui le perd. Vous gagnez aussi des points pour le rang et des jetons de DeadMatch. Pour annuler la recherche, refaite .DM");
//                }
//                    else if ((player.Level > 1))

//                    {
                       
//                        player.SendServerMessage("Vous n'avez pas le niveau nécessaire pour utiliser cette commande");
//                    }

//                }
//        }
//    }
//}
