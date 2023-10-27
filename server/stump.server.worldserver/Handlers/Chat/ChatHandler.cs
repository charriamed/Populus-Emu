using System;
using System.Collections.Generic;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Social;
using MongoDB.Bson;
using Stump.Server.BaseServer.Logging;
using System.Globalization;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Chat
{
    public partial class ChatHandler : WorldHandlerContainer
    {
        [WorldHandler(ChatClientPrivateMessage.Id)]
        public static void HandleChatClientPrivateMessage(WorldClient client, ChatClientPrivateMessage message)
        {
            if (string.IsNullOrEmpty(message.Content))
                return;

            var sender = client.Character;
            var receiver = World.Instance.GetCharacter(message.Receiver);

            if (receiver == null)
            {
                SendChatErrorMessage(client, ChatErrorEnum.CHAT_ERROR_RECEIVER_NOT_FOUND);
                return;
            }

            if (sender.IsMuted())
            {
                //Le principe de pr�caution vous a rendu muet pour %1 seconde(s).
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 123, (int)client.Character.GetMuteRemainingTime().TotalSeconds);
                return;
            }

            if (receiver.IsMuted())
            {
                //Message automatique : Le joueur <b>%1</b> a �t� rendu muet pour ne pas avoir respect� les r�gles. <b>%1</b> ne pourra pas vous r�pondre avant <b>%2</b> minutes.
                sender.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 168, receiver.Name, (int)receiver.GetMuteRemainingTime().TotalMinutes);
                return;
            }

            if (sender == receiver)
            {
                //Le joueur %1 �tait absent et n'a donc pas re�u votre message.
                SendChatErrorMessage(client, ChatErrorEnum.CHAT_ERROR_INTERIOR_MONOLOGUE);
                return;
            }

            var badword = ChatManager.Instance.CanSendMessage(message.Content);
            if (badword != string.Empty)
            {
                client.Character.SendServerMessage($"Message non envoy�. Le terme <b>{badword}</b> est interdit sur le serveur !");
                return;
            }

            if (receiver.FriendsBook.IsIgnored(sender.Account.Id))
            {
                //Le joueur %1 �tait absent et n'a donc pas re�u votre message.
                sender.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 14, receiver.Name);
                return;
            }

            if (!receiver.IsAvailable(sender, true))
            {
                //Le joueur %1 �tait absent et n'a donc pas re�u votre message.
                sender.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 14, receiver.Name);
                return;
            }

            if (sender.Status.StatusId != (sbyte)PlayerStatusEnum.PLAYER_STATUS_AVAILABLE
                && sender.Status.StatusId != (sbyte)PlayerStatusEnum.PLAYER_STATUS_PRIVATE || !sender.FriendsBook.IsFriend(receiver.Account.Id))
                sender.SetStatus(PlayerStatusEnum.PLAYER_STATUS_AVAILABLE);

            var document = new BsonDocument
                    {
                        { "SenderId", sender.Id },
                        { "SenderName", sender.Name },
                        { "SenderAccountId", sender.Account.Id },
                        { "ReceiverId", receiver.Id },
                        { "ReceiverName", receiver.Name },
                        { "ReceiverAccountId", receiver.Account.Id },
                        { "Message", message.Content },
                        { "Channel", (int)ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE },
                        { "Date", DateTime.Now.ToString(CultureInfo.InvariantCulture) }
                    };

            //MongoLogger.Instance.Insert("Chats", document);

            //Send to receiver
            SendChatServerMessage(receiver.Client, sender, ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.Content);
            //Send a copy to sender
            SendChatServerCopyMessage(client, sender, receiver, ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.Content);

            if (receiver.Status.StatusId == (sbyte)PlayerStatusEnum.PLAYER_STATUS_AFK && receiver.Status is PlayerStatusExtended)
                SendChatServerMessage(sender.Client, receiver, ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, $"R�ponse automatique:{((PlayerStatusExtended)receiver.Status).Message}");
        }
    
        [WorldHandler(ChatClientPrivateWithObjectMessage.Id)]
        public static void HandleChatClientPrivateWithObjectMessage(WorldClient client, ChatClientPrivateWithObjectMessage message)
        {
            if (string.IsNullOrEmpty(message.Content))
                return;

            var sender = client.Character;
            var receiver = World.Instance.GetCharacter(message.Receiver);

            if (receiver == null)
            {
                SendChatErrorMessage(client, ChatErrorEnum.CHAT_ERROR_RECEIVER_NOT_FOUND);
                return;
            }

            if (sender.IsMuted())
            {
                //Le principe de pr�caution vous a rendu muet pour %1 seconde(s).
                sender.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 123, (int)sender.GetMuteRemainingTime().TotalSeconds);
                return;
            }

            if (receiver.IsMuted())
            {
                //Message automatique : Le joueur <b>%1</b> a �t� rendu muet pour ne pas avoir respect� les r�gles. <b>%1</b> ne pourra pas vous r�pondre avant <b>%2</b> minutes.
                sender.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 168, receiver.Name, receiver.Name, (int)receiver.GetMuteRemainingTime().TotalMinutes);
                return;
            }

            if (sender == receiver)
            {
                SendChatErrorMessage(client, ChatErrorEnum.CHAT_ERROR_INTERIOR_MONOLOGUE);
                return;
            }

            var badword = ChatManager.Instance.CanSendMessage(message.Content);
            if (badword != string.Empty)
            {
                client.Character.SendServerMessage($"Message non envoy�. Le terme <b>{badword}</b> est interdit sur le serveur !");
                return;
            }

            if (receiver.FriendsBook.IsIgnored(sender.Account.Id))
            {
                //Le joueur %1 �tait absent et n'a donc pas re�u votre message.
                sender.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 14, receiver.Name);
                return;
            }

            if (!receiver.IsAvailable(sender, true))
            {
                //Le joueur %1 �tait absent et n'a donc pas re�u votre message.
                sender.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 14, receiver.Name);
                return;
            }

            if (sender.Status.StatusId != (sbyte)PlayerStatusEnum.PLAYER_STATUS_AVAILABLE
                && sender.Status.StatusId != (sbyte)PlayerStatusEnum.PLAYER_STATUS_PRIVATE || !sender.FriendsBook.IsFriend(receiver.Account.Id))
                sender.SetStatus(PlayerStatusEnum.PLAYER_STATUS_AVAILABLE);

            var document = new BsonDocument
                    {
                        { "SenderId", sender.Id },
                        { "SenderName", sender.Name },
                        { "SenderAccountId", sender.Account.Id },
                        { "ReceiverId", receiver.Id },
                        { "ReceiverName", receiver.Name },
                        { "ReceiverAccountId", receiver.Account.Id },
                        { "Message", message.Content },
                        { "Channel", (int)ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE },
                        { "Date", DateTime.Now.ToString(CultureInfo.InvariantCulture) }
                    };

            //MongoLogger.Instance.Insert("Chats", document);

            //Send to receiver
            SendChatServerWithObjectMessage(receiver.Client, sender, ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.Content, "", message.Objects);
            //Send a copy to sender
            SendChatServerCopyWithObjectMessage(client, sender, receiver, ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.Content, message.Objects);
            
            if (receiver.Status.StatusId == (sbyte)PlayerStatusEnum.PLAYER_STATUS_AFK && receiver.Status is PlayerStatusExtended)
                SendChatServerMessage(sender.Client, receiver, ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, $"R�ponse automatique:{((PlayerStatusExtended)receiver.Status).Message}");
        }

        [WorldHandler(ChatClientMultiMessage.Id)]
        public static void HandleChatClientMultiMessage(WorldClient client, ChatClientMultiMessage message)
        {
            ChatManager.Instance.HandleChat(client, (ChatActivableChannelsEnum)message.Channel, message.Content);
        }

        [WorldHandler(ChatClientMultiWithObjectMessage.Id)]
        public static void HandleChatClientMultiWithObjectMessage(WorldClient client, ChatClientMultiWithObjectMessage message)
        {
            ChatManager.Instance.HandleChat(client, (ChatActivableChannelsEnum)message.Channel, message.Content, message.Objects);
        }

        public static  void SendChatServerWithObjectMessage(IPacketReceiver client, INamedActor sender, ChatActivableChannelsEnum channel, string content, string fingerprint, IEnumerable<ObjectItem> objectItems)
        {
            client.Send(new ChatServerWithObjectMessage((sbyte)channel, content, DateTime.Now.GetUnixTimeStamp(), fingerprint, sender.Id, sender.Name, "", 0, objectItems.ToArray()));
        }

        public static void SendChatServerMessage(IPacketReceiver client, string message)
        {
            SendChatServerMessage(client, ChatActivableChannelsEnum.PSEUDO_CHANNEL_INFO, message, DateTime.Now.GetUnixTimeStamp(), "", 0, "", 0);
        }

        public static void SendChatServerMessage(IPacketReceiver client, INamedActor sender, ChatActivableChannelsEnum channel, string message)
        {
            SendChatServerMessage(client, sender, channel, message, DateTime.Now.GetUnixTimeStamp(), "");
        }

        public static void SendChatServerMessage(IPacketReceiver client, INamedActor sender, ChatActivableChannelsEnum channel, string message,
                                                 int timestamp, string fingerprint)
        {
            client.Send(new ChatServerMessage(
                            (sbyte)channel,
                            message,
                            timestamp,
                            fingerprint,
                            sender.Id,
                            sender.Name,
                            "",
                            0));
        }

        public static void SendChatServerMessage(IPacketReceiver client, Character sender, ChatActivableChannelsEnum channel, string message)
        {
            SendChatServerMessage(client, sender, channel, message, DateTime.Now.GetUnixTimeStamp(), "");
        }

        public static void SendChatServerMessage(IPacketReceiver client, Character sender, ChatActivableChannelsEnum channel, string message, int timestamp, string fingerprint)
        {
            if (string.IsNullOrEmpty(message))
                return;

            if (sender.UserGroup.Role <= RoleEnum.Moderator)
                message = message.HtmlEntities();

            client.Send(new ChatServerMessage(
                (sbyte)channel,
                message,
                timestamp,
                fingerprint,
                sender.Id,
                sender.Name,
                "",
                sender.Account.Id));
        }

        public static void SendChatServerMessage(IPacketReceiver client, ChatActivableChannelsEnum channel, string message, int timestamp, string fingerprint,
            int senderId, string senderName, int accountId)
        {
            if (!string.IsNullOrEmpty(message))
            {
                client.Send(new ChatServerMessage(
                                (sbyte)channel,
                                message,
                                timestamp,
                                fingerprint,
                                senderId,
                                senderName,
                                "",
                                accountId));
            }
        }

        //public static void SendChatAdminServerMessage(IPacketReceiver client, Character sender, ChatActivableChannelsEnum channel, string message)
        //{
        //    SendChatAdminServerMessage(client, sender, channel, message, DateTime.Now.GetUnixTimeStamp(), "");
        //}

        //public static void SendChatAdminServerMessage(IPacketReceiver client, Character sender, ChatActivableChannelsEnum channel, string message,
        //                                              int timestamp, string fingerprint)
        //{
        //    SendChatAdminServerMessage(client, channel,
        //                               message,
        //                               timestamp,
        //                               fingerprint,
        //                               sender.Id,
        //                               sender.Name,
        //                               sender.Account.Id);
        //}

        //public static void SendChatAdminServerMessage(IPacketReceiver client, ChatActivableChannelsEnum channel, string message, int timestamp,
        //    string fingerprint, int senderId, string senderName, int accountId)
        //{
        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        client.Send(new ChatAdminServerMessage((sbyte)channel,
        //                                               message,
        //                                               timestamp,
        //                                               fingerprint,
        //                                               senderId,
        //                                               senderName,
        //                                               accountId));
        //    }
        //}
        
        public static void SendChatServerCopyMessage(IPacketReceiver client, Character sender, Character receiver, ChatActivableChannelsEnum channel, string message)
        {
            SendChatServerCopyMessage(client, sender, receiver, channel, message, DateTime.Now.GetUnixTimeStamp(), "");
        }

        public static void SendChatServerCopyMessage(IPacketReceiver client, Character sender, Character receiver, ChatActivableChannelsEnum channel, string message, int timestamp, string fingerprint)
        {
            if (!sender.UserGroup.IsGameMaster)
                message = message.HtmlEntities();

            client.Send(new ChatServerCopyMessage(
                            (sbyte)channel,
                            message,
                            timestamp,
                            fingerprint,
                            (ulong)receiver.Id,
                            receiver.Name));
        }

        public static void SendChatServerCopyWithObjectMessage(IPacketReceiver client, Character sender, Character receiver, ChatActivableChannelsEnum channel,
                                                     string message, IEnumerable<ObjectItem> objectItems)
        {
            SendChatServerCopyWithObjectMessage(client, sender, receiver, channel, message, DateTime.Now.GetUnixTimeStamp(), "", objectItems);
        }

        public static void SendChatServerCopyWithObjectMessage(IPacketReceiver client, Character sender, Character receiver, ChatActivableChannelsEnum channel, string message,
                                                     int timestamp, string fingerprint, IEnumerable<ObjectItem> objectItems)
        {
            if (!sender.UserGroup.IsGameMaster)
                message = message.HtmlEntities();

            client.Send(new ChatServerCopyWithObjectMessage(
                            (sbyte)channel,
                            message,
                            timestamp,
                            fingerprint,
                            (ulong)receiver.Id,
                            receiver.Name,
                            objectItems.ToArray()));
        }

        public static void SendChatErrorMessage(IPacketReceiver client, ChatErrorEnum error)
        {
            client.Send(new ChatErrorMessage((sbyte)error));
        }
    }
}