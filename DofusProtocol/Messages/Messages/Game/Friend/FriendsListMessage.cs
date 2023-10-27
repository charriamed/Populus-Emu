namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FriendsListMessage : Message
    {
        public const uint Id = 4002;
        public override uint MessageId
        {
            get { return Id; }
        }
        public FriendInformations[] FriendsList { get; set; }

        public FriendsListMessage(FriendInformations[] friendsList)
        {
            this.FriendsList = friendsList;
        }

        public FriendsListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)FriendsList.Count());
            for (var friendsListIndex = 0; friendsListIndex < FriendsList.Count(); friendsListIndex++)
            {
                var objectToSend = FriendsList[friendsListIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var friendsListCount = reader.ReadUShort();
            FriendsList = new FriendInformations[friendsListCount];
            for (var friendsListIndex = 0; friendsListIndex < friendsListCount; friendsListIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<FriendInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                FriendsList[friendsListIndex] = objectToAdd;
            }
        }

    }
}
