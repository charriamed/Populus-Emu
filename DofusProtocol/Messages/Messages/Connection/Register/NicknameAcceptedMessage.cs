namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NicknameAcceptedMessage : Message
    {
        public const uint Id = 5641;
        public override uint MessageId
        {
            get { return Id; }
        }

        public NicknameAcceptedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
