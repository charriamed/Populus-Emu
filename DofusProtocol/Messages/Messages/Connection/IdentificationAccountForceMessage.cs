namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class IdentificationAccountForceMessage : IdentificationMessage
    {
        public new const uint Id = 6119;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string ForcedAccountLogin { get; set; }

        public IdentificationAccountForceMessage(bool autoconnect, bool useCertificate, bool useLoginToken, VersionExtended version, string lang, sbyte[] credentials, short serverId, long sessionOptionalSalt, ushort[] failedAttempts, string forcedAccountLogin)
        {
            this.Autoconnect = autoconnect;
            this.UseCertificate = useCertificate;
            this.UseLoginToken = useLoginToken;
            this.Version = version;
            this.Lang = lang;
            this.Credentials = credentials;
            this.ServerId = serverId;
            this.SessionOptionalSalt = sessionOptionalSalt;
            this.FailedAttempts = failedAttempts;
            this.ForcedAccountLogin = forcedAccountLogin;
        }

        public IdentificationAccountForceMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(ForcedAccountLogin);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ForcedAccountLogin = reader.ReadUTF();
        }

    }
}
