namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextRemoveMultipleElementsWithEventsMessage : GameContextRemoveMultipleElementsMessage
    {
        public new const uint Id = 6416;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte[] ElementEventIds { get; set; }

        public GameContextRemoveMultipleElementsWithEventsMessage(double[] elementsIds, byte[] elementEventIds)
        {
            this.ElementsIds = elementsIds;
            this.ElementEventIds = elementEventIds;
        }

        public GameContextRemoveMultipleElementsWithEventsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)ElementEventIds.Count());
            for (var elementEventIdsIndex = 0; elementEventIdsIndex < ElementEventIds.Count(); elementEventIdsIndex++)
            {
                writer.WriteByte(ElementEventIds[elementEventIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var elementEventIdsCount = reader.ReadUShort();
            ElementEventIds = new byte[elementEventIdsCount];
            for (var elementEventIdsIndex = 0; elementEventIdsIndex < elementEventIdsCount; elementEventIdsIndex++)
            {
                ElementEventIds[elementEventIdsIndex] = reader.ReadByte();
            }
        }

    }
}
