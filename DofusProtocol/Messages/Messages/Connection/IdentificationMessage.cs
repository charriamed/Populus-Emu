namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class IdentificationMessage : Message
    {
        public const uint Id = 4;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Autoconnect { get; set; }
        public bool UseCertificate { get; set; }
        public bool UseLoginToken { get; set; }
        public VersionExtended Version { get; set; }
        public string Lang { get; set; }
        public sbyte[] Credentials { get; set; }
        public short ServerId { get; set; }
        public long SessionOptionalSalt { get; set; }
        public ushort[] FailedAttempts { get; set; }

        public IdentificationMessage(bool autoconnect, bool useCertificate, bool useLoginToken, VersionExtended version, string lang, sbyte[] credentials, short serverId, long sessionOptionalSalt, ushort[] failedAttempts)
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
        }

        public IdentificationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Autoconnect);
            flag = BooleanByteWrapper.SetFlag(flag, 1, UseCertificate);
            flag = BooleanByteWrapper.SetFlag(flag, 2, UseLoginToken);
            writer.WriteByte(flag);
            Version.Serialize(writer);
            writer.WriteUTF(Lang);
            writer.WriteVarInt(Credentials.Count());
            for (var credentialsIndex = 0; credentialsIndex < Credentials.Count(); credentialsIndex++)
            {
                writer.WriteSByte(Credentials[credentialsIndex]);
            }
            writer.WriteShort(ServerId);
            writer.WriteVarLong(SessionOptionalSalt);
            writer.WriteShort((short)FailedAttempts.Count());
            for (var failedAttemptsIndex = 0; failedAttemptsIndex < FailedAttempts.Count(); failedAttemptsIndex++)
            {
                writer.WriteVarUShort(FailedAttempts[failedAttemptsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            Autoconnect = BooleanByteWrapper.GetFlag(flag, 0);
            UseCertificate = BooleanByteWrapper.GetFlag(flag, 1);
            UseLoginToken = BooleanByteWrapper.GetFlag(flag, 2);
            Version = new VersionExtended();
            Version.Deserialize(reader);
            Lang = reader.ReadUTF();
            var credentialsCount = reader.ReadVarInt();
            Credentials = new sbyte[credentialsCount];
            for (var credentialsIndex = 0; credentialsIndex < credentialsCount; credentialsIndex++)
            {
                Credentials[credentialsIndex] = reader.ReadSByte();
            }
            ServerId = reader.ReadShort();
            SessionOptionalSalt = reader.ReadVarLong();
            var failedAttemptsCount = reader.ReadUShort();
            FailedAttempts = new ushort[failedAttemptsCount];
            for (var failedAttemptsIndex = 0; failedAttemptsIndex < failedAttemptsCount; failedAttemptsIndex++)
            {
                FailedAttempts[failedAttemptsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
