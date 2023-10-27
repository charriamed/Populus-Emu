namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdentificationSuccessWithLoginTokenMessage : IdentificationSuccessMessage
    {
        public new const uint Id = 6209;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string LoginToken { get; set; }

        public IdentificationSuccessWithLoginTokenMessage(bool hasRights, bool wasAlreadyConnected, string login, string nickname, int accountId, sbyte communityId, string secretQuestion, double accountCreation, double subscriptionElapsedDuration, double subscriptionEndDate, byte havenbagAvailableRoom, string loginToken)
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
            this.LoginToken = loginToken;
        }

        public IdentificationSuccessWithLoginTokenMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(LoginToken);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            LoginToken = reader.ReadUTF();
        }

    }
}
