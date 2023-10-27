namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobBookSubscribeRequestMessage : Message
    {
        public const uint Id = 6592;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte[] JobIds { get; set; }

        public JobBookSubscribeRequestMessage(byte[] jobIds)
        {
            this.JobIds = jobIds;
        }

        public JobBookSubscribeRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)JobIds.Count());
            for (var jobIdsIndex = 0; jobIdsIndex < JobIds.Count(); jobIdsIndex++)
            {
                writer.WriteByte(JobIds[jobIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var jobIdsCount = reader.ReadUShort();
            JobIds = new byte[jobIdsCount];
            for (var jobIdsIndex = 0; jobIdsIndex < jobIdsCount; jobIdsIndex++)
            {
                JobIds[jobIdsIndex] = reader.ReadByte();
            }
        }

    }
}
