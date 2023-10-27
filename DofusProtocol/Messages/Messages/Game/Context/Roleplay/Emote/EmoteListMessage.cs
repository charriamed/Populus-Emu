namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class EmoteListMessage : Message
    {
        public const uint Id = 5689;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte[] EmoteIds { get; set; }

        public EmoteListMessage(byte[] emoteIds)
        {
            this.EmoteIds = emoteIds;
        }

        public EmoteListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)EmoteIds.Count());
            for (var emoteIdsIndex = 0; emoteIdsIndex < EmoteIds.Count(); emoteIdsIndex++)
            {
                writer.WriteByte(EmoteIds[emoteIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var emoteIdsCount = reader.ReadUShort();
            EmoteIds = new byte[emoteIdsCount];
            for (var emoteIdsIndex = 0; emoteIdsIndex < emoteIdsCount; emoteIdsIndex++)
            {
                EmoteIds[emoteIdsIndex] = reader.ReadByte();
            }
        }

    }
}
