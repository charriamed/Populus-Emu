namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryEntryRequestMessage : Message
    {
        public const uint Id = 6043;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }

        public JobCrafterDirectoryEntryRequestMessage(ulong playerId)
        {
            this.PlayerId = playerId;
        }

        public JobCrafterDirectoryEntryRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarULong();
        }

    }
}
