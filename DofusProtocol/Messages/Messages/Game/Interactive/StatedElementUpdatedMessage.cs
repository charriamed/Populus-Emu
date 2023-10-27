namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatedElementUpdatedMessage : Message
    {
        public const uint Id = 5709;
        public override uint MessageId
        {
            get { return Id; }
        }
        public StatedElement StatedElement { get; set; }

        public StatedElementUpdatedMessage(StatedElement statedElement)
        {
            this.StatedElement = statedElement;
        }

        public StatedElementUpdatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            StatedElement.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            StatedElement = new StatedElement();
            StatedElement.Deserialize(reader);
        }

    }
}
