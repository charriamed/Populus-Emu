using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.Basic;

namespace Stump.Server.WorldServer.Handlers.Characters
{
    public partial class CharacterHandler : WorldHandlerContainer
    {
        [Variable]
        public static bool EnableNameSuggestion = true;

        [WorldHandler(CharacterCreationRequestMessage.Id, ShouldBeLogged = false, IsGamePacket = false)]
        public static void HandleCharacterCreationRequestMessage(WorldClient client, CharacterCreationRequestMessage message)
        {
            if (!IPCAccessor.Instance.IsConnected)
            {
                SendCharacterCreationResultMessage(client, CharacterCreationResultEnum.ERR_NO_REASON);
                return;
            }

            CharacterManager.Instance.CreateCharacter(client, message.Name, message.Breed, message.Sex, message.Colors, message.CosmeticId,
                () => OnCharacterCreationSuccess(client), x => OnCharacterCreationFailed(client, x));
        }

        private static void OnCharacterCreationSuccess(WorldClient client)
        {
            SendCharacterCreationResultMessage(client, CharacterCreationResultEnum.OK);
            BasicHandler.SendBasicNoOperationMessage(client);
            SendCharactersListWithRemodelingMessage(client);
        }

        private static void OnCharacterCreationFailed(IPacketReceiver client, CharacterCreationResultEnum result)
        {
            SendCharacterCreationResultMessage(client, result);
        }

        [WorldHandler(CharacterNameSuggestionRequestMessage.Id, ShouldBeLogged = false, IsGamePacket = false)]
        public static void HandleCharacterNameSuggestionRequestMessage(WorldClient client, CharacterNameSuggestionRequestMessage message)
        {
            if (!EnableNameSuggestion)
            {
                client.Send(new CharacterNameSuggestionFailureMessage((sbyte)NicknameGeneratingFailureEnum.NICKNAME_GENERATOR_UNAVAILABLE));
                return;
            }

            var generatedName = CharacterManager.Instance.GenerateName();

            client.Send(new CharacterNameSuggestionSuccessMessage(generatedName));
        }

        public static void SendCharacterCreationResultMessage(IPacketReceiver client, CharacterCreationResultEnum result)
        {
            client.Send(new CharacterCreationResultMessage((sbyte) result));
        }
    }
}