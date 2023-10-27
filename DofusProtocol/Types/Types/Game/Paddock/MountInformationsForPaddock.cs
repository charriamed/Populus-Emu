namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountInformationsForPaddock
    {
        public const short Id  = 184;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ModelId { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }

        public MountInformationsForPaddock(ushort modelId, string name, string ownerName)
        {
            this.ModelId = modelId;
            this.Name = name;
            this.OwnerName = ownerName;
        }

        public MountInformationsForPaddock() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ModelId);
            writer.WriteUTF(Name);
            writer.WriteUTF(OwnerName);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ModelId = reader.ReadVarUShort();
            Name = reader.ReadUTF();
            OwnerName = reader.ReadUTF();
        }

    }
}
