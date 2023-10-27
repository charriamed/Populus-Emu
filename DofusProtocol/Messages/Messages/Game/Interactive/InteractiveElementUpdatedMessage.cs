namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveElementUpdatedMessage : Message
    {
        public const uint Id = 5708;
        public override uint MessageId
        {
            get { return Id; }
        }
        public InteractiveElement InteractiveElement { get; set; }

        public InteractiveElementUpdatedMessage(InteractiveElement interactiveElement)
        {
            this.InteractiveElement = interactiveElement;
        }

        public InteractiveElementUpdatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            InteractiveElement.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            InteractiveElement = new InteractiveElement();
            InteractiveElement.Deserialize(reader);
        }

    }
}
