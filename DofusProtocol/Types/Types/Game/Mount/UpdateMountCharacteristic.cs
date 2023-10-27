namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class UpdateMountCharacteristic
    {
        public const short Id  = 536;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte Type { get; set; }

        public UpdateMountCharacteristic(sbyte type)
        {
            this.Type = type;
        }

        public UpdateMountCharacteristic() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Type);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Type = reader.ReadSByte();
        }

    }
}
