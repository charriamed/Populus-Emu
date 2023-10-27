namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SpouseGetInformationsMessage : Message
    {
        public const uint Id = 6355;
        public override uint MessageId
        {
            get { return Id; }
        }

        public SpouseGetInformationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
