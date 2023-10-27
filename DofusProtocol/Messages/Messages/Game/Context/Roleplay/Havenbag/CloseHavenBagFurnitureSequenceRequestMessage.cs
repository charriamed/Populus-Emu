namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CloseHavenBagFurnitureSequenceRequestMessage : Message
    {
        public const uint Id = 6621;
        public override uint MessageId
        {
            get { return Id; }
        }

        public CloseHavenBagFurnitureSequenceRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
