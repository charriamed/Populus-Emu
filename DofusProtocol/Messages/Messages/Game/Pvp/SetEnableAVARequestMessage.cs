namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SetEnableAVARequestMessage : Message
    {
        public const uint Id = 6443;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Enable { get; set; }

        public SetEnableAVARequestMessage(bool enable)
        {
            this.Enable = enable;
        }

        public SetEnableAVARequestMessage() { }

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
