using Stump.Core.Attributes;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using System;
using System.Drawing;
namespace Stump.Server.WorldServer.Commands.Commands.Players
{
    public class CanalAll : CommandBase
    {
        [Variable(true)]
        public static string player = ColorTranslator.ToHtml(Color.Yellow);
        public CanalAll()
        {
            base.Aliases = new string[]
            {
                "all",
            };
            base.Description = "Canal pour tout les joueurs sans limites";
            base.RequiredRole = RoleEnum.Player;
            base.AddParameter<string>("message", "msg", "all", null, false, null);
        }
        public override void Execute(TriggerBase trigger)
        {
            Color color = ColorTranslator.FromHtml(CanalAll.player);
            string text = trigger.Get<string>("msg");
            string announce = (trigger is GameTrigger) ? string.Format("(JOUEUR) {0} : {1}", ((GameTrigger)trigger).Character.Name, text) : string.Format("(JOUEUR) {0}", text);
            Singleton<World>.Instance.SendAnnounce(announce, color);
        }
    }
}
