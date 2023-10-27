namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HavenBagFurnituresMessage : Message
    {
        public const uint Id = 6634;
        public override uint MessageId
        {
            get { return Id; }
        }
        public HavenBagFurnitureInformation[] FurnituresInfos { get; set; }

        public HavenBagFurnituresMessage(HavenBagFurnitureInformation[] furnituresInfos)
        {
            this.FurnituresInfos = furnituresInfos;
        }

        public HavenBagFurnituresMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)FurnituresInfos.Count());
            for (var furnituresInfosIndex = 0; furnituresInfosIndex < FurnituresInfos.Count(); furnituresInfosIndex++)
            {
                var objectToSend = FurnituresInfos[furnituresInfosIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var furnituresInfosCount = reader.ReadUShort();
            FurnituresInfos = new HavenBagFurnitureInformation[furnituresInfosCount];
            for (var furnituresInfosIndex = 0; furnituresInfosIndex < furnituresInfosCount; furnituresInfosIndex++)
            {
                var objectToAdd = new HavenBagFurnitureInformation();
                objectToAdd.Deserialize(reader);
                FurnituresInfos[furnituresInfosIndex] = objectToAdd;
            }
        }

    }
}
