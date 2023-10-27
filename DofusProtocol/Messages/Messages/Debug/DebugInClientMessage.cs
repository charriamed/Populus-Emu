namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DebugInClientMessage : Message
    {
        public const uint Id = 6028;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte level;
        public string message;

        public DebugInClientMessage(sbyte level, string message)
        {
            this.level = level;
            this.message = message;
        }

        public DebugInClientMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(level);
            writer.WriteUTF(message);
        }

        public override void Deserialize(IDataReader reader)
        {
            level = reader.ReadSByte();
            message = reader.ReadUTF();
        }

    }
}
