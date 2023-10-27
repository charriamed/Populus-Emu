namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutEmote : Shortcut
    {
        public new const short Id = 389;
        public override short TypeId
        {
            get { return Id; }
        }
        public byte EmoteId { get; set; }

        public ShortcutEmote(sbyte slot, byte emoteId)
        {
            this.Slot = slot;
            this.EmoteId = emoteId;
        }

        public ShortcutEmote() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(EmoteId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EmoteId = reader.ReadByte();
        }

    }
}
