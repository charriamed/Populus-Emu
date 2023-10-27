using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Bank;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class PassCommand : InGameCommand
    {
        public PassCommand()
        {
            Aliases = new[] { "pass" };
            Description = "Permet de passer automatiquement les tours.";
            RequiredRole = RoleEnum.Player;
        }

        CharacterFighter Fighter;

        public override void Execute(GameTrigger trigger)
        {
            if (!trigger.Character.ForcePassTurn)
            {
                if(trigger.Character.Fighter != null)
                {
                    Fighter = trigger.Character.Fighter;
                    trigger.Character.ContextChanged += OnContextChanged;
                    trigger.Character.Fighter.Fight.TurnStarted += OnTurnStarted;
                }
                else
                {
                    trigger.Character.ContextChanged += OnContextChanged;
                }
                trigger.Character.SendServerMessage("Vous passerez désormais automatiquement vos tours.");
                trigger.Character.ForcePassTurn = true;
            }
            else
            {
                if (trigger.Character.Fighter != null)
                {
                    Fighter = trigger.Character.Fighter;
                    trigger.Character.ContextChanged -= OnContextChanged;
                    trigger.Character.Fighter.Fight.TurnStarted -= OnTurnStarted;
                }
                else
                {
                    trigger.Character.ContextChanged -= OnContextChanged;
                }
                trigger.Character.SendServerMessage("Vous pouvez désormais jouer normalement vos tours.");
                trigger.Character.ForcePassTurn = false;
            }
        }

        private void OnContextChanged(Character character, bool inFight)
        {
            if(character.Fighter != null)
            {
                Fighter = character.Fighter;
                character.Fighter.Fight.TurnStarted += OnTurnStarted;
            }
        }

        private void OnTurnStarted(IFight fight, FightActor actor)
        {
            if (Fighter != null && actor != Fighter)
                return;

            Fighter.PassTurn();
        }
    }
}