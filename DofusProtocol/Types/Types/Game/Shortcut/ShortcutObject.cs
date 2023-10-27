namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutObject : Shortcut
    {
        public new const short Id = 367;
        public override short TypeId
        {
            get { return Id; }
        }

        public ShortcutObject(sbyte slot)
        {
            this.Slot = slot;
        }

        public ShortcutObject() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
