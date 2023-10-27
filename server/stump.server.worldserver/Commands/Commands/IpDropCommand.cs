using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Commands.Commands
{
    class IpDropCommand : InGameCommand
    {
        public IpDropCommand()
        {
            base.Aliases = new[] { "ipdrop" };
            base.Description = "Le personnage ayant activé cette commande recevra tous le drop de tous vos personnages.";
            base.RequiredRole = RoleEnum.Player;
        }
        public override void Execute(GameTrigger trigger)
        {
            Character player = trigger.Character;
            if (!player.IsIpDrop)
            {
                player.IsIpDrop = true;
                player.SendServerMessage("Vous avez activé l'ip drop.");
            }
            else
            {
                player.IsIpDrop = false;
                player.SendServerMessage("Vous avez désactivé l'ip drop.");
            }
        }
    }
}
