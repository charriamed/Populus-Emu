namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HavenBagPackListMessage : Message
    {
        public const uint Id = 6620;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte[] PackIds { get; set; }

        public HavenBagPackListMessage(sbyte[] packIds)
        {
            this.PackIds = packIds;
        }

        public HavenBagPackListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)PackIds.Count());
            for (var packIdsIndex = 0; packIdsIndex < PackIds.Count(); packIdsIndex++)
            {
                writer.WriteSByte(PackIds[packIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var packIdsCount = reader.ReadUShort();
            PackIds = new sbyte[packIdsCount];
            for (var packIdsIndex = 0; packIdsIndex < packIdsCount; packIdsIndex++)
            {
                PackIds[packIdsIndex] = reader.ReadSByte();
            }
        }

    }
}
