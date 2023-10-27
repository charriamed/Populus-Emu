namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockPropertiesMessage : Message
    {
        public const uint Id = 5824;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PaddockInstancesInformations Properties { get; set; }

        public PaddockPropertiesMessage(PaddockInstancesInformations properties)
        {
            this.Properties = properties;
        }

        public PaddockPropertiesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Properties.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Properties = new PaddockInstancesInformations();
            Properties.Deserialize(reader);
        }

    }
}
