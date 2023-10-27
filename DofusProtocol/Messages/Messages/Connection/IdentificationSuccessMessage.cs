namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdentificationSuccessMessage : Message
    {
        public const uint Id = 22;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool HasRights { get; set; }
        public bool WasAlreadyConnected { get; set; }
        public string Login { get; set; }
        public string Nickname { get; set; }
        public int AccountId { get; set; }
        public sbyte CommunityId { get; set; }
        public string SecretQuestion { get; set; }
        public double AccountCreation { get; set; }
        public double SubscriptionElapsedDuration { get; set; }
        public double SubscriptionEndDate { get; set; }
        public byte HavenbagAvailableRoom { get; set; }

        public IdentificationSuccessMessage(bool hasRights, bool wasAlreadyConnected, string login, string nickname, int accountId, sbyte communityId, string secretQuestion, double accountCreation, double subscriptionElapsedDuration, double subscriptionEndDate, byte havenbagAvailableRoom)
        {
            this.HasRights = hasRights;
            this.WasAlreadyConnected = wasAlreadyConnected;
            this.Login = login;
            this.Nickname = nickname;
            this.AccountId = accountId;
            this.CommunityId = communityId;
            this.SecretQuestion = secretQuestion;
            this.AccountCreation = accountCreation;
            this.SubscriptionElapsedDuration = subscriptionElapsedDuration;
            this.SubscriptionEndDate = subscriptionEndDate;
            this.HavenbagAvailableRoom = havenbagAvailableRoom;
        }

        public IdentificationSuccessMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, HasRights);
            flag = BooleanByteWrapper.SetFlag(flag, 1, WasAlreadyConnected);
            writer.WriteByte(flag);
            writer.WriteUTF(Login);
            writer.WriteUTF(Nickname);
            writer.WriteInt(AccountId);
            writer.WriteSByte(CommunityId);
            writer.WriteUTF(SecretQuestion);
            writer.WriteDouble(AccountCreation);
            writer.WriteDouble(SubscriptionElapsedDuration);
            writer.WriteDouble(SubscriptionEndDate);
            writer.WriteByte(HavenbagAvailableRoom);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            HasRights = BooleanByteWrapper.GetFlag(flag, 0);
            WasAlreadyConnected = BooleanByteWrapper.GetFlag(flag, 1);
            Login = reader.ReadUTF();
            Nickname = reader.ReadUTF();
            AccountId = reader.ReadInt();
            CommunityId = reader.ReadSByte();
            SecretQuestion = reader.ReadUTF();
            AccountCreation = reader.ReadDouble();
            SubscriptionElapsedDuration = reader.ReadDouble();
            SubscriptionEndDate = reader.ReadDouble();
            HavenbagAvailableRoom = reader.ReadByte();
        }

    }
}
