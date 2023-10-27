namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ChatSmileyExtraPackListMessage : Message
    {
        public const uint Id = 6596;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte[] PackIds { get; set; }

        public ChatSmileyExtraPackListMessage(byte[] packIds)
        {
            this.PackIds = packIds;
        }

        public ChatSmileyExtraPackListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)PackIds.Count());
            for (var packIdsIndex = 0; packIdsIndex < PackIds.Count(); packIdsIndex++)
            {
                writer.WriteByte(PackIds[packIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var packIdsCount = reader.ReadUShort();
            PackIds = new byte[packIdsCount];
            for (var packIdsIndex = 0; packIdsIndex < packIdsCount; packIdsIndex++)
            {
                PackIds[packIdsIndex] = reader.ReadByte();
            }
        }

    }
}
