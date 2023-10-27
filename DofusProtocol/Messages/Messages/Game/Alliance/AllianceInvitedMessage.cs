namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceInvitedMessage : Message
    {
        public const uint Id = 6397;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong RecruterId { get; set; }
        public string RecruterName { get; set; }
        public BasicNamedAllianceInformations AllianceInfo { get; set; }

        public AllianceInvitedMessage(ulong recruterId, string recruterName, BasicNamedAllianceInformations allianceInfo)
        {
            this.RecruterId = recruterId;
            this.RecruterName = recruterName;
            this.AllianceInfo = allianceInfo;
        }

        public AllianceInvitedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(RecruterId);
            writer.WriteUTF(RecruterName);
            AllianceInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            RecruterId = reader.ReadVarULong();
            RecruterName = reader.ReadUTF();
            AllianceInfo = new BasicNamedAllianceInformations();
            AllianceInfo.Deserialize(reader);
        }

    }
}
