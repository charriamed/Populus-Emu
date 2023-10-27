﻿using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Arena;

namespace Stump.Server.WorldServer.Handlers.Context
{
    public partial class ContextHandler : WorldHandlerContainer
    {
        [WorldHandler(GameRolePlayArenaFightAnswerMessage.Id)]
        public static void HandleGameRolePlayArenaFightAnswerMessage(WorldClient client, GameRolePlayArenaFightAnswerMessage message)
        {
            var popup = client.Character.ArenaPopup;

            if (popup == null)
                return;

            if (message.Accept)
                popup.Accept();
            else
                popup.Deny();
        }

        [WorldHandler(GameRolePlayArenaRegisterMessage.Id)]
        public static void HandleGameRolePlayArenaRegisterMessage(WorldClient client, GameRolePlayArenaRegisterMessage message)
        {
            if (client.Character.ArenaParty != null)
            {
                if (message.BattleMode == 1)
                {
                    client.Character.SendServerMessage("Não foi possível completar a requisição.", System.Drawing.Color.DarkOrange);
                    return;
                }

                if (client.Character.IsPartyLeader(client.Character.ArenaParty.Id))
                    ArenaManager.Instance.AddToQueue(client.Character.ArenaParty);
            }

            else
                ArenaManager.Instance.AddToQueue(client.Character, message.BattleMode);
        }

        [WorldHandler(GameRolePlayArenaUnregisterMessage.Id)]
        public static void HandleGameRolePlayArenaUnregisterMessage(WorldClient client, GameRolePlayArenaUnregisterMessage message)
        {
            if (client.Character.ArenaParty != null)
            {
                if (client.Character.IsPartyLeader(client.Character.ArenaParty.Id))
                    ArenaManager.Instance.RemoveFromQueue(client.Character.ArenaParty);
            }
            else
                ArenaManager.Instance.RemoveFromQueue(client.Character);
        }   
        
        public static void SendGameRolePlayArenaFightPropositionMessage(IPacketReceiver client, ArenaPopup popup, int delay)
        {
            var members = popup.Team.Members.Select(x => (double)x.Character.Id);
            client.Send(new GameRolePlayArenaFightPropositionMessage((ushort)popup.Team.Fight.Id, members.ToArray(), (ushort)delay));
        }

        public static void SendGameRolePlayArenaFighterStatusMessage(IPacketReceiver client, int fightId, Character character, bool accepted)
        {
            client.Send(new GameRolePlayArenaFighterStatusMessage((ushort)fightId, character.Id, accepted));
        }

        public static void SendGameRolePlayArenaRegistrationStatusMessage(IPacketReceiver client, bool registred, PvpArenaStepEnum step, PvpArenaTypeEnum type)
        {
            client.Send(new GameRolePlayArenaRegistrationStatusMessage(registred, (sbyte)step, (sbyte)type));
        }
    }
}