namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AcquaintanceAddedMessage : Message
    {
        public const uint Id = 6818;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AcquaintanceInformation AcquaintanceAdded { get; set; }

        public AcquaintanceAddedMessage(AcquaintanceInformation acquaintanceAdded)
        {
            this.AcquaintanceAdded = acquaintanceAdded;
        }

        public AcquaintanceAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(AcquaintanceAdded.TypeId);
            AcquaintanceAdded.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            AcquaintanceAdded = ProtocolTypeManager.GetInstance<AcquaintanceInformation>(reader.ReadShort());
            AcquaintanceAdded.Deserialize(reader);
        }

    }
}
