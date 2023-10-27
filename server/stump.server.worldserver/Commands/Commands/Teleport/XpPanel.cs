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
    public class XpPanel : CommandBase
    {
        public XpPanel()
        {
            Aliases = new[] { "xp", "XP" };
            RequiredRole = RoleEnum.Player;
            Description = "Téléportation aux maps xp clés du serveur.";
        }

        public override void Execute(TriggerBase trigger)
        {
            Dictionary<Map, int> destinations = new Dictionary<Map, int>();
            destinations.Add(World.Instance.GetMap(153360), 442);
            destinations.Add(World.Instance.GetMap(153090), 385);
            destinations.Add(World.Instance.GetMap(153621), 145);
            destinations.Add(World.Instance.GetMap(179307526), 370);
            destinations.Add(World.Instance.GetMap(76287498), 386);
            destinations.Add(World.Instance.GetMap(54159142), 214);
            destinations.Add(World.Instance.GetMap(186384902), 427);
            destinations.Add(World.Instance.GetMap(54175012), 163);
            destinations.Add(World.Instance.GetMap(173277701), 203);
            destinations.Add(World.Instance.GetMap(99614728), 331);
            destinations.Add(World.Instance.GetMap(118096384), 215);
            destinations.Add(World.Instance.GetMap(156893704), 147);
            destinations.Add(World.Instance.GetMap(64489222), 288);
            destinations.Add(World.Instance.GetMap(63177216), 106);
            destinations.Add(World.Instance.GetMap(196345858), 213);

            var gameTrigger = trigger as GameTrigger;
            DungsDialog s = new DungsDialog(gameTrigger.Character, destinations);
            s.Open();
        }
    }
}