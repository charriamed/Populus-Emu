namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceFactsRequestMessage : Message
    {
        public const uint Id = 6409;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint AllianceId { get; set; }

        public AllianceFactsRequestMessage(uint allianceId)
        {
            this.AllianceId = allianceId;
        }

        public AllianceFactsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(AllianceId);
        }

        public override void Deserialize(IDataReader reader)
        {
            AllianceId = reader.ReadVarUInt();
        }

    }
}
