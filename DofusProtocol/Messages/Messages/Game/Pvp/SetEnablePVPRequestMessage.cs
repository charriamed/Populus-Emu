namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SetEnablePVPRequestMessage : Message
    {
        public const uint Id = 1810;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Enable { get; set; }

        public SetEnablePVPRequestMessage(bool enable)
        {
            this.Enable = enable;
        }

        public SetEnablePVPRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Enable);
        }

        public override void Deserialize(IDataReader reader)
        {
            Enable = reader.ReadBoolean();
        }

    }
}
