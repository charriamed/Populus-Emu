namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInvitationAnswerMessage : Message
    {
        public const uint Id = 5556;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Accept { get; set; }

        public GuildInvitationAnswerMessage(bool accept)
        {
            this.Accept = accept;
        }

        public GuildInvitationAnswerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Accept);
        }

        public override void Deserialize(IDataReader reader)
        {
            Accept = reader.ReadBoolean();
        }

    }
}
