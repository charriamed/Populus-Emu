namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceBulletinSetErrorMessage : SocialNoticeSetErrorMessage
    {
        public new const uint Id = 6692;
        public override uint MessageId
        {
            get { return Id; }
        }

        public AllianceBulletinSetErrorMessage(sbyte reason)
        {
            this.Reason = reason;
        }

        public AllianceBulletinSetErrorMessage() { }

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
