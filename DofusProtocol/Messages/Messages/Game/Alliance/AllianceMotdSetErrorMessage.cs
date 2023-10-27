namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceMotdSetErrorMessage : SocialNoticeSetErrorMessage
    {
        public new const uint Id = 6683;
        public override uint MessageId
        {
            get { return Id; }
        }

        public AllianceMotdSetErrorMessage(sbyte reason)
        {
            this.Reason = reason;
        }

        public AllianceMotdSetErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
