namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ItemNoMoreAvailableMessage : Message
    {
        public const uint Id = 5769;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ItemNoMoreAvailableMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
