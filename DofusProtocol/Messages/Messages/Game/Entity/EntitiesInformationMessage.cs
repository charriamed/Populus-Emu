namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class EntitiesInformationMessage : Message
    {
        public const uint Id = 6775;
        public override uint MessageId
        {
            get { return Id; }
        }
        public EntityInformation[] Entities { get; set; }

        public EntitiesInformationMessage(EntityInformation[] entities)
        {
            this.Entities = entities;
        }

        public EntitiesInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Entities.Count());
            for (var entitiesIndex = 0; entitiesIndex < Entities.Count(); entitiesIndex++)
            {
                var objectToSend = Entities[entitiesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var entitiesCount = reader.ReadUShort();
            Entities = new EntityInformation[entitiesCount];
            for (var entitiesIndex = 0; entitiesIndex < entitiesCount; entitiesIndex++)
            {
                var objectToAdd = new EntityInformation();
                objectToAdd.Deserialize(reader);
                Entities[entitiesIndex] = objectToAdd;
            }
        }

    }
}
