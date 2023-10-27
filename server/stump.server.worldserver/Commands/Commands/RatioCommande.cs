//using System;
//using System.Drawing;
//using Stump.Core.Attributes;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.BaseServer.Commands;
//using Stump.Server.WorldServer.Commands.Commands.Patterns;
//using Stump.Server.WorldServer.Commands.Trigger;
//using Stump.Server.WorldServer.Game;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

//namespace Stump.Server.WorldServer.Commands.Commands
//{
//    class Ratio : SubCommandContainer
//    {
//        public Ratio()
//        {
//            Aliases = new[] { "ratio" };
//            Description = "Afficher les commandes de ratio";
//            RequiredRole = RoleEnum.Player;
//        }

//        public class ratiopvp : TargetSubCommand
//        {
//            public ratiopvp()
//            {
//                Aliases = new[] { "pvp" };
//                RequiredRole = RoleEnum.Player;
//                Description = "Affiche ton ratio PVP";
//                ParentCommandType = typeof(Ratio);
//            }
//            public override void Execute(TriggerBase trigger)
//            {
//                var gameTrigger = trigger as GameTrigger;
//                if (gameTrigger != null)
//                {


//                    var player = gameTrigger.Character;
//                    var ratio = player.CharacterWinPvp;
//                    if (player.Record.LosPvp != 0) ratio = Math.Round(player.CharacterWinPvp / player.CharacterLosPvp, 2);


//                    player.OpenPopup(" Votre ratio PVP est de <b>  " + ratio + " </b> avec un total de <b>" + player.CharacterWinPvp + "</b> victoires et <b> " + player.CharacterLosPvp + " </b> défaites");

//                }
//            }
//        }
    

//    public class ratiopvm : TargetSubCommand
//        {
//            public ratiopvm()
//            {
//                Aliases = new[] { "pvm" };
//                RequiredRole = RoleEnum.Player;
//                Description = "Affiche ton ratio PVM";
//                ParentCommandType = typeof(Ratio);
//            }
//            public override void Execute(TriggerBase trigger)
//            {
//                var gameTrigger = trigger as GameTrigger;
//                if (gameTrigger != null)
//                {

                    
//                    var player = gameTrigger.Character;
//                    var ratio = player.CharacterWinPvm;
//                    if (player.Record.LosPvm != 0) ratio = Math.Round(player.CharacterWinPvm / player.CharacterLosPvm, 2);
                  

//                    player.OpenPopup(" Votre ratio est de <b>  " + ratio + " </b> avec un total de <b>" + player.CharacterWinPvm + "</b> victoires et <b> " + player.CharacterLosPvm + " </b> défaites");
                  
//                    }
//                } } }

//            public class ratioinfo : TargetSubCommand
//            {
//                public ratioinfo()
//                {
//                    Aliases = new[] { "info" };
//                    RequiredRole = RoleEnum.Player;
//                    Description = "Explique le système de ratio";
//                    ParentCommandType = typeof(Ratio);
//                }
//                public override void Execute(TriggerBase trigger)
//                {
//                    var gameTrigger = trigger as GameTrigger;
//                    if (gameTrigger != null)
//                    {
//                        var player = gameTrigger.Character;
//                        player.SendServerMessage("Chaque combat gagné contre un monstre de niveau 120 minimum ajoute une victoire à votre compteur. Chaque défaite ajoute une défaite à votre compteur.");
//                    }
//                }
//            }
//        }
    
