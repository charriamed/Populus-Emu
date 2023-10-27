namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceFactsErrorMessage : Message
    {
        public const uint Id = 6423;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint AllianceId { get; set; }

        public AllianceFactsErrorMessage(uint allianceId)
        {
            this.AllianceId = allianceId;
        }

        public AllianceFactsErrorMessage() { }

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
