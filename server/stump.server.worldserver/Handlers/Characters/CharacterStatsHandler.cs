using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Handlers.Characters
{
    public partial class CharacterHandler : WorldHandlerContainer
    {
        public static void SendLifePointsRegenBeginMessage(IPacketReceiver client, sbyte regenRate)
        {
            client.Send(new LifePointsRegenBeginMessage((byte)regenRate));
        }

        public static void SendUpdateLifePointsMessage(WorldClient client)
        {
            client.Send(new UpdateLifePointsMessage(
                (uint)client.Character.Stats.Health.Total,
                (uint)client.Character.Stats.Health.TotalMax));
        }

        public static void SendLifePointsRegenEndMessage(WorldClient client, int recoveredLife)
        {
            client.Send(new LifePointsRegenEndMessage(
                (uint)client.Character.Stats.Health.Total,
                (uint)client.Character.Stats.Health.TotalMax,
                (uint)recoveredLife));
        }

        public static void SendCharacterStatsListMessage(WorldClient client)
        {
            client.Send(new CharacterStatsListMessage(client.Character.GetCharacterCharacteristicsInformations()));
        }

        public static void SendCharacterLevelUpMessage(IPacketReceiver client, ushort level)
        {
            client.Send(new CharacterLevelUpMessage((ushort)level));
        }

        public static void SendCharacterLevelUpInformationMessage(IPacketReceiver client, Character character, ushort level)
        {
            client.Send(new CharacterLevelUpInformationMessage((ushort)level, character.Name, (ulong)character.Id));
        }

        public static void SendGameRolePlayPlayerLifeStatusMessage(IPacketReceiver client, PlayerLifeStatusEnum status, int phoenixMapId)
        {
            client.Send(new GameRolePlayPlayerLifeStatusMessage((sbyte)status, phoenixMapId));
        }

        public static void SendCharacterExperienceGainMessage(IPacketReceiver client, ulong experienceCharacter,
           ulong experienceMount, ulong experienceGuild, ulong experienceIncarnation)
        {
            client.Send(new CharacterExperienceGainMessage(experienceCharacter, experienceMount, experienceGuild,
                experienceIncarnation));
        }
    }
}