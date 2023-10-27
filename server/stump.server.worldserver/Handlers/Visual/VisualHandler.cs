using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Handlers.Visual
{
    class VisualHandler : WorldHandlerContainer
    {
        public static void SendGameRolePlaySpellAnimMessage(IPacketReceiver client, Character character, int cellId, int SpellId)
        {
            client.Send(new GameRolePlaySpellAnimMessage((ulong)character.Id, (ushort)cellId, (ushort)SpellId, 1));
        }
    }
}
