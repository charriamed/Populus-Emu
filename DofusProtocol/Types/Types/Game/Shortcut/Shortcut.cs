namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class Shortcut
    {
        public const short Id  = 369;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte Slot { get; set; }

        public Shortcut(sbyte slot)
        {
            this.Slot = slot;
        }

        public Shortcut() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Slot);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Slot = reader.ReadSByte();
        }

    }
}
