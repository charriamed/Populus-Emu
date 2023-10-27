namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextRemoveMultipleElementsMessage : Message
    {
        public const uint Id = 252;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double[] ElementsIds { get; set; }

        public GameContextRemoveMultipleElementsMessage(double[] elementsIds)
        {
            this.ElementsIds = elementsIds;
        }

        public GameContextRemoveMultipleElementsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ElementsIds.Count());
            for (var elementsIdsIndex = 0; elementsIdsIndex < ElementsIds.Count(); elementsIdsIndex++)
            {
                writer.WriteDouble(ElementsIds[elementsIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var elementsIdsCount = reader.ReadUShort();
            ElementsIds = new double[elementsIdsCount];
            for (var elementsIdsIndex = 0; elementsIdsIndex < elementsIdsCount; elementsIdsIndex++)
            {
                ElementsIds[elementsIdsIndex] = reader.ReadDouble();
            }
        }

    }
}
