namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AccessoryPreviewMessage : Message
    {
        public const uint Id = 6517;
        public override uint MessageId
        {
            get { return Id; }
        }
        public EntityLook Look { get; set; }

        public AccessoryPreviewMessage(EntityLook look)
        {
            this.Look = look;
        }

        public AccessoryPreviewMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Look.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Look = new EntityLook();
            Look.Deserialize(reader);
        }

    }
}
