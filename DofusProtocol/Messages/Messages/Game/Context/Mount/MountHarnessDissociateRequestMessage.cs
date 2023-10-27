namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountHarnessDissociateRequestMessage : Message
    {
        public const uint Id = 6696;
        public override uint MessageId
        {
            get { return Id; }
        }

        public MountHarnessDissociateRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
