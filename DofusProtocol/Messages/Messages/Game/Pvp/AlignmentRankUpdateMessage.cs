namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AlignmentRankUpdateMessage : Message
    {
        public const uint Id = 6058;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte AlignmentRank { get; set; }
        public bool Verbose { get; set; }

        public AlignmentRankUpdateMessage(sbyte alignmentRank, bool verbose)
        {
            this.AlignmentRank = alignmentRank;
            this.Verbose = verbose;
        }

        public AlignmentRankUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(AlignmentRank);
            writer.WriteBoolean(Verbose);
        }

        public override void Deserialize(IDataReader reader)
        {
            AlignmentRank = reader.ReadSByte();
            Verbose = reader.ReadBoolean();
        }

    }
}
