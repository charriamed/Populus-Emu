namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AtlasPointInformationsMessage : Message
    {
        public const uint Id = 5956;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AtlasPointsInformations Type { get; set; }

        public AtlasPointInformationsMessage(AtlasPointsInformations type)
        {
            this.Type = type;
        }

        public AtlasPointInformationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Type.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Type = new AtlasPointsInformations();
            Type.Deserialize(reader);
        }

    }
}
