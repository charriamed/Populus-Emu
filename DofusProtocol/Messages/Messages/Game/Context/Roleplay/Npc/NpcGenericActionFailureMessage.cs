namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NpcGenericActionFailureMessage : Message
    {
        public const uint Id = 5900;
        public override uint MessageId
        {
            get { return Id; }
        }

        public NpcGenericActionFailureMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
