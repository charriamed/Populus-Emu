namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChannelEnablingChangeMessage : Message
    {
        public const uint Id = 891;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Channel { get; set; }
        public bool Enable { get; set; }

        public ChannelEnablingChangeMessage(sbyte channel, bool enable)
        {
            this.Channel = channel;
            this.Enable = enable;
        }

        public ChannelEnablingChangeMessage() { }

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
