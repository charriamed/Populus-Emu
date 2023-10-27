using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Commands.Commands
{
    class VieCommand : InGameCommand
    {
        public VieCommand()
        {
            base.Aliases = new[] { "hp" };
            base.Description = "Rend l'intégralité des points de vie.";
            base.RequiredRole = RoleEnum.Administrator;
        }
        public override void Execute(GameTrigger trigger)
        {
            Character player = trigger.Character;
            if (!player.IsFighting())
            {
                player.Record.DamageTaken = 0;
                player.Stats.Health.DamageTaken = 0;
                player.RefreshStats();
                player.SaveLater();
            }
            else
            {
                if (player.Fight.State == Game.Fights.FightState.Placement)
                {
                    player.Record.DamageTaken = 0;
                    player.Stats.Health.DamageTaken = 0;
                    player.RefreshStats();
                    player.SaveLater();
                }
                player.SendServerMessage("Action impossible en combat...");
            }

        }
    }
}
