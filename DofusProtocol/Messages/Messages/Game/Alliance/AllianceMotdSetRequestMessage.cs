namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceMotdSetRequestMessage : SocialNoticeSetRequestMessage
    {
        public new const uint Id = 6687;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Content { get; set; }

        public AllianceMotdSetRequestMessage(string content)
        {
            this.Content = content;
        }

        public AllianceMotdSetRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Content);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Content = reader.ReadUTF();
        }

    }
}
