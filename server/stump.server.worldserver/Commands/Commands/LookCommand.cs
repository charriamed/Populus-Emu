using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System;
namespace Stump.Server.WorldServer.Commands.Commands
{
    public class LookCommand : TargetCommand
    {
        public LookCommand()
        {
            base.Aliases = new string[]
            {
                "look"
            };
            base.RequiredRole = RoleEnum.Moderator;
            base.Description = "Change l'apparence de la cible";
            base.AddParameter<string>("look", "l", "The new look for the target", null, true, null);
            base.AddTargetParameter(true, "Defined target");
            base.AddParameter<bool>("demorph", "demorph", "Redonne l'apparence de base à la cible", false, true, null);
        }
        public override void Execute(TriggerBase trigger)
        {
            Character[] targets = base.GetTargets(trigger);
            int i = 0;
            while (i < targets.Length)
            {
                Character target = targets[i];
                if (!trigger.IsArgumentDefined("demorph"))
                {
                    if (trigger.IsArgumentDefined("look"))
                    {
                        target.CustomLook = ActorLook.Parse(trigger.Get<string>("look"));
                        target.CustomLookActivated = true;
                        target.RefreshActor();
                        i++;
                        continue;
                    }
                    trigger.ReplyError("Look not defined");
                }
                else
                {
                    target.CustomLookActivated = false;
                    target.CustomLook = null;
                    trigger.Reply("Demorphed");
                    target.Map.Area.ExecuteInContext(delegate
                    {
                        target.Map.Refresh(target);
                    });
                }
                return;
            }
        }
    }
}