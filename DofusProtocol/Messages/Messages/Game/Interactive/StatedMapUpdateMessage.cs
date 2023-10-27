namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class StatedMapUpdateMessage : Message
    {
        public const uint Id = 5716;
        public override uint MessageId
        {
            get { return Id; }
        }
        public StatedElement[] StatedElements { get; set; }

        public StatedMapUpdateMessage(StatedElement[] statedElements)
        {
            this.StatedElements = statedElements;
        }

        public StatedMapUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)StatedElements.Count());
            for (var statedElementsIndex = 0; statedElementsIndex < StatedElements.Count(); statedElementsIndex++)
            {
                var objectToSend = StatedElements[statedElementsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var statedElementsCount = reader.ReadUShort();
            StatedElements = new StatedElement[statedElementsCount];
            for (var statedElementsIndex = 0; statedElementsIndex < statedElementsCount; statedElementsIndex++)
            {
                var objectToAdd = new StatedElement();
                objectToAdd.Deserialize(reader);
                StatedElements[statedElementsIndex] = objectToAdd;
            }
        }

    }
}
