namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceJoinedMessage : Message
    {
        public const uint Id = 6402;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AllianceInformations AllianceInfo { get; set; }
        public bool Enabled { get; set; }
        public uint LeadingGuildId { get; set; }

        public AllianceJoinedMessage(AllianceInformations allianceInfo, bool enabled, uint leadingGuildId)
        {
            this.AllianceInfo = allianceInfo;
            this.Enabled = enabled;
            this.LeadingGuildId = leadingGuildId;
        }

        public AllianceJoinedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            AllianceInfo.Serialize(writer);
            writer.WriteBoolean(Enabled);
            writer.WriteVarUInt(LeadingGuildId);
        }

        public override void Deserialize(IDataReader reader)
        {
            AllianceInfo = new AllianceInformations();
            AllianceInfo.Deserialize(reader);
            Enabled = reader.ReadBoolean();
            LeadingGuildId = reader.ReadVarUInt();
        }

    }
}
