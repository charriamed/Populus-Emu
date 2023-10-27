using System;
using System.Drawing;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class GoPosCommand : TargetCommand
    {
        public GoPosCommand()
        {
            Aliases = new[] {"gopos", "teleporto"};
            RequiredRole = RoleEnum.Administrator;
            Description = "Teleport the target to the given map position (x/y)";
            AddParameter<int>("x");
            AddParameter<int>("y");
            AddTargetParameter(true);
            AddParameter<short>("cellid", "cell", "Cell destination", isOptional: true);
            AddParameter("superarea", "area", "Super area containing the map (e.g 0 is continent, 3 is incarnam)", isOptional: true, converter:ParametersConverter.SuperAreaConverter);
            AddParameter<int>("outdoor", "out", isOptional:true);
        }

        public override void Execute(TriggerBase trigger)
        {
            var point = new Point(trigger.Get<int>("x"), trigger.Get<int>("y"));
            var outdoorDefined = trigger.IsArgumentDefined("outdoor");
            var outdoor = trigger.Get<int>("outdoor");

            foreach (var target in GetTargets(trigger))
            {                
                var reference = target.Map;

                Map map;
                if (trigger.IsArgumentDefined("superarea"))
                {
                    var superArea = trigger.Get<SuperArea>("superarea");

                    map = outdoorDefined
                        ? superArea.GetMaps(point, outdoorDefined).FirstOrDefault()
                        : superArea.GetMaps(point).ElementAtOrDefault(outdoor);
                }
                else
                {
                    map = outdoorDefined
                        ? World.Instance.GetMaps(reference, point.X, point.Y).ElementAtOrDefault(outdoor)
                        : World.Instance.GetMaps(reference, point.X, point.Y, outdoorDefined).FirstOrDefault();
                }

                if (map == null)
                {
                    trigger.ReplyError("Map x:{0} y:{1} not found", point.X, point.Y);
                }
                else
                {
                    var cell = trigger.IsArgumentDefined("cell")
                        ? map.Cells[trigger.Get<short>("cell")]
                        : target.Cell;

                    target.Teleport(new ObjectPosition(map, cell, target.Direction));

                    trigger.Reply("Teleported to {0} {1} ({2}).", point.X, point.Y, map.Id);
                }
            }
        }
    }

    public class GoCommand : TargetCommand
    {
        public GoCommand()
        {
            Aliases = new[] { "go", "tp" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Teleport the target given map id";
            AddParameter("map", "map", "Map destination", converter:ParametersConverter.MapConverter);
            AddTargetParameter(true);
            AddParameter<short>("cellid", "cell", "Cell destination", isOptional: true);
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {
                var map = trigger.Get<Map>("map");

                if (map == null)
                {
                    trigger.ReplyError("Map '{0}' doesn't exist", trigger.Get<int>("mapid"));
                }
                else
                {
                    var cell = trigger.IsArgumentDefined("cell") ? map.Cells[trigger.Get<short>("cell")] : target.Cell;

                    target.Teleport(new ObjectPosition(map, cell, target.Direction));

                    trigger.Reply("Teleported.");
                }
            }
        }
    }

    public class GoNameCommand : CommandBase
    {
        public GoNameCommand()
        {
            Aliases = new[] { "goname", "tptoname" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Teleport to the target";
            AddParameter("to", "to", "The character to rejoin", converter: ParametersConverter.CharacterConverter);
            AddParameter("from", "from", "The character that teleport", isOptional:true, converter: ParametersConverter.CharacterConverter);
        }

        public override void Execute(TriggerBase trigger)
        {
            var to = trigger.Get<Character>("to");
            Character from;

            if (trigger.IsArgumentDefined("from"))
                from = trigger.Get<Character>("from");
            else if (trigger is GameTrigger)
                from = ( trigger as GameTrigger ).Character;
            else
            {
                throw new Exception("Character to teleport not defined !");
            }

            from.Teleport(to.Position);
        }
    }

    public class NameGoCommand : TargetCommand
    {
        public NameGoCommand()
        {
            Aliases = new[] { "namego" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Teleport target to you";
            AddTargetParameter(false, "The character to teleport");
        }

        public override void Execute(TriggerBase trigger)
        {
            foreach (var target in GetTargets(trigger))
            {
                var to = ((GameTrigger) trigger).Character;

                Character target1 = target;
                target.Area.ExecuteInContext(() =>
                    target1.Teleport(to.Position));
            }
        }
    }
}