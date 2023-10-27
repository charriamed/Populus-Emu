namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameDataPaddockObjectListAddMessage : Message
    {
        public const uint Id = 5992;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PaddockItem[] PaddockItemDescription { get; set; }

        public GameDataPaddockObjectListAddMessage(PaddockItem[] paddockItemDescription)
        {
            this.PaddockItemDescription = paddockItemDescription;
        }

        public GameDataPaddockObjectListAddMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)PaddockItemDescription.Count());
            for (var paddockItemDescriptionIndex = 0; paddockItemDescriptionIndex < PaddockItemDescription.Count(); paddockItemDescriptionIndex++)
            {
                var objectToSend = PaddockItemDescription[paddockItemDescriptionIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var paddockItemDescriptionCount = reader.ReadUShort();
            PaddockItemDescription = new PaddockItem[paddockItemDescriptionCount];
            for (var paddockItemDescriptionIndex = 0; paddockItemDescriptionIndex < paddockItemDescriptionCount; paddockItemDescriptionIndex++)
            {
                var objectToAdd = new PaddockItem();
                objectToAdd.Deserialize(reader);
                PaddockItemDescription[paddockItemDescriptionIndex] = objectToAdd;
            }
        }

    }
}
