namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChannelEnablingMessage : Message
    {
        public const uint Id = 890;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Channel { get; set; }
        public bool Enable { get; set; }

        public ChannelEnablingMessage(sbyte channel, bool enable)
        {
            this.Channel = channel;
            this.Enable = enable;
        }

        public ChannelEnablingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Channel);
            writer.WriteBoolean(Enable);
        }

        public override void Deserialize(IDataReader reader)
        {
            Channel = reader.ReadSByte();
            Enable = reader.ReadBoolean();
        }

    }
}
