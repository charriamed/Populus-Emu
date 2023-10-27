namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntLegendaryRequestMessage : Message
    {
        public const uint Id = 6499;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort LegendaryId { get; set; }

        public TreasureHuntLegendaryRequestMessage(ushort legendaryId)
        {
            this.LegendaryId = legendaryId;
        }

        public TreasureHuntLegendaryRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(LegendaryId);
        }

        public override void Deserialize(IDataReader reader)
        {
            LegendaryId = reader.ReadVarUShort();
        }

    }
}
