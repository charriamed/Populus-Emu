using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps;

namespace Stump.Server.WorldServer.Commands.Commands
{
    class FollowCommand : CommandBase
    {
        public FollowCommand()
        {
            Aliases = new[] { "follow" };
            Description = "Suis automatiquement une position.";
            RequiredRole = RoleEnum.Administrator;
            AddParameter<int>("x");
            AddParameter<int>("y");
        }

        public override void Execute(TriggerBase trigger)
        {
            var gameTrigger = trigger as GameTrigger;
            if (gameTrigger != null)
            {
               var player = gameTrigger.Character;
               var point = new Point(trigger.Get<int>("x"), trigger.Get<int>("y"));
                var map = World.Instance.GetMaps(player.Map, point.X, point.Y).ToList();
                if (map.Count >= 1)
                {
                    player.onFollowMap(map[0]);
                    player.SendServerMessage("Suivi automatique activé.");
                }
                else
                {
                    player.SendServerMessage("Impossible de trouver cette map dans votre area", Color.DarkOrange);
                }
            }
        }
    }
}