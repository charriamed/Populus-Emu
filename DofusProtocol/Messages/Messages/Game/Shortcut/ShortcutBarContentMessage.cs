namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutBarContentMessage : Message
    {
        public const uint Id = 6231;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte BarType { get; set; }
        public Shortcut[] Shortcuts { get; set; }

        public ShortcutBarContentMessage(sbyte barType, Shortcut[] shortcuts)
        {
            this.BarType = barType;
            this.Shortcuts = shortcuts;
        }

        public ShortcutBarContentMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(BarType);
            writer.WriteShort((short)Shortcuts.Count());
            for (var shortcutsIndex = 0; shortcutsIndex < Shortcuts.Count(); shortcutsIndex++)
            {
                var objectToSend = Shortcuts[shortcutsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            BarType = reader.ReadSByte();
            var shortcutsCount = reader.ReadUShort();
            Shortcuts = new Shortcut[shortcutsCount];
            for (var shortcutsIndex = 0; shortcutsIndex < shortcutsCount; shortcutsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<Shortcut>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Shortcuts[shortcutsIndex] = objectToAdd;
            }
        }

    }
}
