using Stump.DofusProtocol.Enums;
using Stump.ORM.SubSonic.Extensions;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Bank;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Parties;
using Stump.Server.WorldServer.Handlers.Inventory;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class FollowerTest : InGameCommand
    {
        public FollowerTest()
        {
            Aliases = new[] { "follower" };
            Description = "Permet d'inviter dans un groupe tous vos personnages.";
            RequiredRole = RoleEnum.Moderator;
            AddParameter<string>("id", "id", "Monstre");
        }

        public override void Execute(GameTrigger trigger)
        {
            if (trigger.Get<string>("id") == null)
                return;

            var character = trigger.Character;

            switch (trigger.Get<string>("id"))
            {
                case "Goultard":
                    character.Look.SetFollowerSkin(1, new short[] { 1463 }, new short[] { 155 }, null);
                    break;

                case "Solar":
                    character.Look.SetFollowerSkin(4514, null, new short[] { 90 }, null);
                    break;

                case "Self":
                    Dictionary<int, Color> color = new Dictionary<int, Color>();
                    foreach(var colore in character.Look.Colors)
                    {
                        color.Add(colore.Key, colore.Value);
                    }
                    List<short> scales = new List<short>();
                    foreach (var scale in character.Look.Scales)
                    {
                        scales.Add(scale);
                    }
                    character.Look.SetFollowerSkin(1, character.Look.Skins.ToArray(), scales.ToArray(), color);
                    break;

                case "None":
                    character.Look.RemoveFollower();
                    break;

                default:
                    character.SendServerMessage($"Le Monstre {trigger.Get<string>("id")} n'est pas disponible ...", Color.OrangeRed);
                    break;
            }
            character.SendServerMessage("Les Disponibles sont : Goultard / Solar / Self / None pour désactiver");


            character.RefreshActor();
        }

    }
}