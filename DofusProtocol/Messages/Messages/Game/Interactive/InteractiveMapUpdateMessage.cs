namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveMapUpdateMessage : Message
    {
        public const uint Id = 5002;
        public override uint MessageId
        {
            get { return Id; }
        }
        public InteractiveElement[] InteractiveElements { get; set; }

        public InteractiveMapUpdateMessage(InteractiveElement[] interactiveElements)
        {
            this.InteractiveElements = interactiveElements;
        }

        public InteractiveMapUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)InteractiveElements.Count());
            for (var interactiveElementsIndex = 0; interactiveElementsIndex < InteractiveElements.Count(); interactiveElementsIndex++)
            {
                var objectToSend = InteractiveElements[interactiveElementsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var interactiveElementsCount = reader.ReadUShort();
            InteractiveElements = new InteractiveElement[interactiveElementsCount];
            for (var interactiveElementsIndex = 0; interactiveElementsIndex < interactiveElementsCount; interactiveElementsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<InteractiveElement>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                InteractiveElements[interactiveElementsIndex] = objectToAdd;
            }
        }

    }
}
