using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Achievements;
using Stump.Server.WorldServer.Game.Actors;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Game.HavenBags;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Pathfinding;

namespace Stump.Server.WorldServer.Handlers.Breach
{
    public partial class BreachHandler : WorldHandlerContainer
    {
        [WorldHandler(BreachTeleportRequestMessage.Id)]
        public static void HandleBreachTeleportRequestMessage(WorldClient client,
            BreachTeleportRequestMessage message)
        {
            if (client.Character.Record.IsInHavenBag)
            {
                client.Character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(client.Character.Record.MapBeforeHavenBagId), (short)client.Character.Record.CellBeforeHavenBagId));
                client.Character.Record.IsInHavenBag = false;
                client.Character.Record.CellBeforeHavenBagId = 0;
                client.Character.Record.MapBeforeHavenBagId = 0;
            }
            else
            {
                client.Character.Record.IsInHavenBag = true;
                client.Character.Record.CellBeforeHavenBagId = client.Character.Cell.Id;
                client.Character.Record.MapBeforeHavenBagId = client.Character.Map.Id;
                client.Character.Teleport(new ObjectPosition(Singleton<World>.Instance.GetMap(195559424), 382, DirectionsEnum.DIRECTION_SOUTH_EAST));
            }
        }
    }
}