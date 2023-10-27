namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountToggleRidingRequestMessage : Message
    {
        public const uint Id = 5976;
        public override uint MessageId
        {
            get { return Id; }
        }

        public MountToggleRidingRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
