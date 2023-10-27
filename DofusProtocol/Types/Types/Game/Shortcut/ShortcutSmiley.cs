namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutSmiley : Shortcut
    {
        public new const short Id = 388;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort SmileyId { get; set; }

        public ShortcutSmiley(sbyte slot, ushort smileyId)
        {
            this.Slot = slot;
            this.SmileyId = smileyId;
        }

        public ShortcutSmiley() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(SmileyId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SmileyId = reader.ReadVarUShort();
        }

    }
}
