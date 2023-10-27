using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;

namespace Stump.Server.WorldServer.Commands.Commands
{

    public class GodCommand : SubCommandContainer
    {
        public GodCommand()
        {
            Aliases = new[] { "god" };
            RequiredRole = RoleEnum.GameMaster_Padawan;
            Description = "Just to be all powerful.";
        }
    }


    public class GodOnCommand : TargetSubCommand
    {
        public GodOnCommand()
        {
            Aliases = new[] { "on" };
            RequiredRole = RoleEnum.GameMaster_Padawan;
            ParentCommandType = typeof(GodCommand);
            Description = "Activate god mode";
            AddTargetParameter(true);
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {
                target.ToggleGodMode(true);
                trigger.Reply("You are god !");
            }
        }
    }
    public class GodOffCommand : TargetSubCommand
    {
        public GodOffCommand()
        {
            Aliases = new[] { "off" };
            RequiredRole = RoleEnum.GameMaster_Padawan;
            ParentCommandType = typeof(GodCommand);
            Description = "Disable god mode";
            AddTargetParameter(true);
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {
                target.ToggleGodMode(false);
                trigger.Reply("You're not god anymore");
            }
        }
    }

    public class AdminChatCommand : InGameCommand
    {
        public AdminChatCommand()
        {
            Aliases = new[] { "admin" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Enable/disable admin chat mode";
        }
        public override void Execute(GameTrigger trigger)
        {
            trigger.Reply("Admin chat mode is : {0}", trigger.Bold(
                (trigger.Character.AdminMessagesEnabled = !trigger.Character.AdminMessagesEnabled)
                    ? "Enabled"
                    : "Disabled"));
        }
    }

    public class KamasCommand : TargetCommand
    {
        public KamasCommand()
        {
            Aliases = new[] { "kamas" };
            RequiredRole = RoleEnum.GameMaster_Padawan;
            Description = "Add the amount kamas to target's inventory";
            AddParameter<ulong>("amount", "amount", "Amount of kamas to add");
            AddTargetParameter(true);
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {
                var amount = trigger.Get<ulong>("amount");

                target.Inventory.AddKamas(amount);
                trigger.ReplyBold($"{amount} Kamas was added to {target} and he now have {target.Kamas} kamas");
            }
        }
    }

    public class SetStatsCommand : TargetCommand
    {
        public SetStatsCommand()
        {
            Aliases = new[] { "stats" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Set the amount of stats point of the target";
            AddParameter<ushort>("amount", "amount", "Amount of stats points to set");
            AddTargetParameter(true);
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {
                var statsPoints = trigger.Get<ushort>("amount");

                target.StatsPoints = statsPoints;
                target.RefreshStats();
                trigger.Reply("{0} has now {1} stats points", target, statsPoints);
            }

        }
    }

    public class InvisibleCommand : TargetCommand
    {
        public InvisibleCommand()
        {
            Aliases = new[] { "invisible", "setinv" };
            RequiredRole = RoleEnum.GameMaster_Padawan;
            Description = "Toggle invisible state";
            AddTargetParameter(true);
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {

                trigger.Reply(target.ToggleInvisibility() ? "{0} is now invisible" : "{0} is now visible", target);
            }
        }
    }

    public class CriticalModeCommand : TargetCommand
    {
        public CriticalModeCommand()
        {
            Aliases = new[] { "criticalmode" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Critical damages each time";
            AddTargetParameter(true);
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {
                target.ToggleCriticalMode(!target.CriticalMode);
                trigger.Reply(target.CriticalMode ? "Critical Mode ON" : "Critical Mode OFF");
            }
        }
    }
}