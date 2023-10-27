using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Achievements;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class AchievementCommands : SubCommandContainer
    {
        public AchievementCommands()
        {
            Aliases = new[] { "achievements" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Provides commands to manage the achievements";
        }
    }

    public class AchievementAddCommands : InGameSubCommand
    {
        public AchievementAddCommands()
        {
            Aliases = new[] { "add" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Add achievement to the current character";
            AddParameter<int>("id", description: "Id of achievement");
            ParentCommandType = typeof(AchievementCommands);
        }

        public override void Execute(GameTrigger trigger)
        {
            int achievementId = trigger.Get<int>("id");
            AchievementTemplate achievementTemplate = AchievementManager.Instance.TryGetAchievement((uint)achievementId);
            if (achievementTemplate != null)
            {
                trigger.Character.Achievement.CompleteAchievement(achievementTemplate);
            }
            else
            {
                trigger.Character.SendServerMessage("Cette Id n'existe pas !", System.Drawing.Color.Red);
            }
        }
    }
}
