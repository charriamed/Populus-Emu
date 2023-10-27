namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ClientYouAreDrunkMessage : DebugInClientMessage
    {
        public new const uint Id = 6594;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ClientYouAreDrunkMessage(sbyte level, string message)
        {
            this.level = level;
            this.message = message;
        }

        public ClientYouAreDrunkMessage() { }

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
