using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.AuthServer.Managers;
using Stump.Server.AuthServer.Network;

namespace Stump.Server.AuthServer.Handlers.Connection
{
    public partial class ConnectionHandler
    {
        [AuthHandler(AcquaintanceSearchMessage.Id)]
        public static void HandleAcquaintanceSearchMessage(AuthClient client, AcquaintanceSearchMessage message)
        {
            var account = AccountManager.Instance.FindAccountByNickname(message.Nickname);

            if (account == null)
            {
                SendAcquaintanceSearchErrorMessage(client, AcquaintanceErrorEnum.NO_RESULT);
                return;
            }

            SendAcquaintanceSearchServerListMessage(client, account.WorldCharacters.Select(wcr => (ushort)wcr.WorldId).Distinct());
        }

        public static void SendAcquaintanceSearchServerListMessage(AuthClient client, IEnumerable<ushort> serverIds)
        {
            client.Send(new AcquaintanceServerListMessage(serverIds.ToArray()));
        }

        public static void SendAcquaintanceSearchErrorMessage(AuthClient client, AcquaintanceErrorEnum reason)
        {
            client.Send(new AcquaintanceSearchErrorMessage((sbyte) reason));
        }

    }
}