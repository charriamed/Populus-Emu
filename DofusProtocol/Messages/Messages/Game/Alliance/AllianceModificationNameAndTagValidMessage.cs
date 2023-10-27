namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceModificationNameAndTagValidMessage : Message
    {
        public const uint Id = 6449;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string AllianceName { get; set; }
        public string AllianceTag { get; set; }

        public AllianceModificationNameAndTagValidMessage(string allianceName, string allianceTag)
        {
            this.AllianceName = allianceName;
            this.AllianceTag = allianceTag;
        }

        public AllianceModificationNameAndTagValidMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(AllianceName);
            writer.WriteUTF(AllianceTag);
        }

        public override void Deserialize(IDataReader reader)
        {
            AllianceName = reader.ReadUTF();
            AllianceTag = reader.ReadUTF();
        }

    }
}
