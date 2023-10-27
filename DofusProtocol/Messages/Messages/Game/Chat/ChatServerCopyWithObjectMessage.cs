namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ChatServerCopyWithObjectMessage : ChatServerCopyMessage
    {
        public new const uint Id = 884;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem[] Objects { get; set; }

        public ChatServerCopyWithObjectMessage(sbyte channel, string content, int timestamp, string fingerprint, ulong receiverId, string receiverName, ObjectItem[] objects)
        {
            this.Channel = channel;
            this.Content = content;
            this.Timestamp = timestamp;
            this.Fingerprint = fingerprint;
            this.ReceiverId = receiverId;
            this.ReceiverName = receiverName;
            this.Objects = objects;
        }

        public ChatServerCopyWithObjectMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Objects.Count());
            for (var objectsIndex = 0; objectsIndex < Objects.Count(); objectsIndex++)
            {
                var objectToSend = Objects[objectsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var objectsCount = reader.ReadUShort();
            Objects = new ObjectItem[objectsCount];
            for (var objectsIndex = 0; objectsIndex < objectsCount; objectsIndex++)
            {
                var objectToAdd = new ObjectItem();
                objectToAdd.Deserialize(reader);
                Objects[objectsIndex] = objectToAdd;
            }
        }

    }
}
