namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobAllowMultiCraftRequestMessage : Message
    {
        public const uint Id = 5748;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Enabled { get; set; }

        public JobAllowMultiCraftRequestMessage(bool enabled)
        {
            this.Enabled = enabled;
        }

        public JobAllowMultiCraftRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Enabled);
        }

        public override void Deserialize(IDataReader reader)
        {
            Enabled = reader.ReadBoolean();
        }

    }
}
