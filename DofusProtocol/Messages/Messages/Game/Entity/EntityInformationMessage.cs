namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EntityInformationMessage : Message
    {
        public const uint Id = 6771;
        public override uint MessageId
        {
            get { return Id; }
        }
        public EntityInformation Entity { get; set; }

        public EntityInformationMessage(EntityInformation entity)
        {
            this.Entity = entity;
        }

        public EntityInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Entity.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Entity = new EntityInformation();
            Entity.Deserialize(reader);
        }

    }
}
