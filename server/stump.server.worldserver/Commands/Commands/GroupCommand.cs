using Stump.DofusProtocol.Enums;
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
using System.Linq;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class GroupCommand : InGameCommand
    {
        public GroupCommand()
        {
            Aliases = new[] { "groupe" };
            Description = "Permet d'inviter dans un groupe tous vos personnages.";
            RequiredRole = RoleEnum.Player;
        }

        public override void Execute(GameTrigger trigger)
        {
            var character = trigger.Character;

            foreach(var perso in WorldServer.Instance.FindClients(x => x.IP == character.Client.IP && x.Character != character))
            {
                if (character.Party != null && character.Party.Members.Contains(perso.Character))
                    continue;

                character.Invite(perso.Character, PartyTypeEnum.PARTY_TYPE_CLASSICAL, true);
            }
        }

    }
}