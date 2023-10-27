namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class EnabledChannelsMessage : Message
    {
        public const uint Id = 892;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte[] Channels { get; set; }
        public byte[] Disallowed { get; set; }

        public EnabledChannelsMessage(byte[] channels, byte[] disallowed)
        {
            this.Channels = channels;
            this.Disallowed = disallowed;
        }

        public EnabledChannelsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Channels.Count());
            for (var channelsIndex = 0; channelsIndex < Channels.Count(); channelsIndex++)
            {
                writer.WriteByte(Channels[channelsIndex]);
            }
            writer.WriteShort((short)Disallowed.Count());
            for (var disallowedIndex = 0; disallowedIndex < Disallowed.Count(); disallowedIndex++)
            {
                writer.WriteByte(Disallowed[disallowedIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var channelsCount = reader.ReadUShort();
            Channels = new byte[channelsCount];
            for (var channelsIndex = 0; channelsIndex < channelsCount; channelsIndex++)
            {
                Channels[channelsIndex] = reader.ReadByte();
            }
            var disallowedCount = reader.ReadUShort();
            Disallowed = new byte[disallowedCount];
            for (var disallowedIndex = 0; disallowedIndex < disallowedCount; disallowedIndex++)
            {
                Disallowed[disallowedIndex] = reader.ReadByte();
            }
        }

    }
}
