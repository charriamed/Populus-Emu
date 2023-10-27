//using D2pReader.MapInformations;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.BaseServer.Commands;
//using Stump.Server.WorldServer.Commands.Commands.Patterns;
//using Stump.Server.WorldServer.Commands.Trigger;
//using Stump.Server.WorldServer.Game;
//using Stump.Server.WorldServer.Game.Actors;
//using Stump.Server.WorldServer.Game.Actors.Fight;
//using Stump.Server.WorldServer.Game.Actors.RolePlay;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
//using Stump.Server.WorldServer.Game.Exchanges.Bank;
//using Stump.Server.WorldServer.Game.Fights;
//using Stump.Server.WorldServer.Game.Maps;
//using Stump.Server.WorldServer.Game.Maps.Cells;
//using Stump.Server.WorldServer.Game.Maps.Pathfinding;
//using Stump.Server.WorldServer.Game.Parties;
//using Stump.Server.WorldServer.Handlers.Inventory;
//using System.Drawing;
//using System.Linq;

//namespace Stump.Server.WorldServer.Commands.Commands
//{
//    public class RejoinFightCommand : InGameCommand
//    {
//        public RejoinFightCommand()
//        {
//            Aliases = new[] { "joinf" };
//            Description = "Permet de faire rejoindre les combats de votre leader à tous vos personnages.";
//            RequiredRole = RoleEnum.Player;
//        }

//        public override void Execute(GameTrigger trigger)
//        {
//            var character = trigger.Character;

//            if (!character.MuleJoinFight)
//            {
//                character.MuleJoinFight = true;
//                character.EnterFight += OnEnterFight;
//                character.SendServerMessage("Vos personnages rejoindrons désormais les combats !");
//            }
//            else
//            {
//                character.MuleJoinFight = false;
//                character.EnterFight -= OnEnterFight;
//                character.SendServerMessage("Vos personnages ne rejoindrons plus les combats !");
//            }
//        }

//        private void OnEnterFight(CharacterFighter fighter)
//        {
//            if (fighter == null || fighter.Map.IsDungeon())
//                return;

//            foreach (var perso in WorldServer.Instance.FindClients(x => x.IP == fighter.Character.Client.IP && x.Character != fighter.Character))
//            {
//                if (perso.Character.Map.Id == fighter.Map.Id && !perso.Character.IsInFight())
//                {
//                    var fighterr = perso.Character.CreateFighter(fighter.Team);
//                    fighter.Team.AddFighter(fighterr);
//                }
//            }
//        }
//    }
//}