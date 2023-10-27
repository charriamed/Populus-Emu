using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Dialogs.Interactives;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Commands.Commands.Teleport
{
    public class TpPanel : CommandBase
    {
        public TpPanel()
        {
            Aliases = new[] { "tp", "TP" };
            RequiredRole = RoleEnum.Player;
            Description = "Téléportation aux maps clés du serveur.";
        }

        public override void Execute(TriggerBase trigger)
        {
            Dictionary<Map, int> destinations = new Dictionary<Map, int>();
            destinations.Add(World.Instance.GetMap(191105026), 273);
            destinations.Add(World.Instance.GetMap(76284416), 203);
            destinations.Add(World.Instance.GetMap(5506048), 359);
            destinations.Add(World.Instance.GetMap(13631488), 359);
            destinations.Add(World.Instance.GetMap(185863169), 329);
            destinations.Add(World.Instance.GetMap(139724293), 484);

            if((trigger as GameTrigger).UserRole == RoleEnum.Administrator)
            {
                destinations.Add(World.Instance.GetMap(154627), 382);
            }

            var gameTrigger = trigger as GameTrigger;
            DungsDialog s = new DungsDialog(gameTrigger.Character, destinations);
            s.Open();
        }
    }
}