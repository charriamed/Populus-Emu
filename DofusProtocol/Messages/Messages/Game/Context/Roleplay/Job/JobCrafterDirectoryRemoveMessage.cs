namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryRemoveMessage : Message
    {
        public const uint Id = 5653;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte JobId { get; set; }
        public ulong PlayerId { get; set; }

        public JobCrafterDirectoryRemoveMessage(sbyte jobId, ulong playerId)
        {
            this.JobId = jobId;
            this.PlayerId = playerId;
        }

        public JobCrafterDirectoryRemoveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(JobId);
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            JobId = reader.ReadSByte();
            PlayerId = reader.ReadVarULong();
        }

    }
}
