namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutBarRemovedMessage : Message
    {
        public const uint Id = 6224;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte BarType { get; set; }
        public sbyte Slot { get; set; }

        public ShortcutBarRemovedMessage(sbyte barType, sbyte slot)
        {
            this.BarType = barType;
            this.Slot = slot;
        }

        public ShortcutBarRemovedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(BarType);
            writer.WriteSByte(Slot);
        }

        public override void Deserialize(IDataReader reader)
        {
            BarType = reader.ReadSByte();
            Slot = reader.ReadSByte();
        }

    }
}
