namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class NotificationListMessage : Message
    {
        public const uint Id = 6087;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int[] Flags { get; set; }

        public NotificationListMessage(int[] flags)
        {
            this.Flags = flags;
        }

        public NotificationListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Flags.Count());
            for (var flagsIndex = 0; flagsIndex < Flags.Count(); flagsIndex++)
            {
                writer.WriteVarInt(Flags[flagsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var flagsCount = reader.ReadUShort();
            Flags = new int[flagsCount];
            for (var flagsIndex = 0; flagsIndex < flagsCount; flagsIndex++)
            {
                Flags[flagsIndex] = reader.ReadVarInt();
            }
        }

    }
}
