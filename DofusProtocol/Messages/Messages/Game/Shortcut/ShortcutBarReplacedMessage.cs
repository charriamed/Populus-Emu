namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutBarReplacedMessage : Message
    {
        public const uint Id = 6706;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte BarType { get; set; }
        public Shortcut Shortcut { get; set; }

        public ShortcutBarReplacedMessage(sbyte barType, Shortcut shortcut)
        {
            this.BarType = barType;
            this.Shortcut = shortcut;
        }

        public ShortcutBarReplacedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(BarType);
            writer.WriteShort(Shortcut.TypeId);
            Shortcut.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            BarType = reader.ReadSByte();
            Shortcut = ProtocolTypeManager.GetInstance<Shortcut>(reader.ReadShort());
            Shortcut.Deserialize(reader);
        }

    }
}
