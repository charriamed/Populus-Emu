namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutBarRefreshMessage : Message
    {
        public const uint Id = 6229;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte BarType { get; set; }
        public Shortcut Shortcut { get; set; }

        public ShortcutBarRefreshMessage(sbyte barType, Shortcut shortcut)
        {
            this.BarType = barType;
            this.Shortcut = shortcut;
        }

        public ShortcutBarRefreshMessage() { }

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
