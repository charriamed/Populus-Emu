namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ServerSessionConstant
    {
        public const short Id  = 430;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectId { get; set; }

        public ServerSessionConstant(ushort objectId)
        {
            this.ObjectId = objectId;
        }

        public ServerSessionConstant() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUShort();
        }

    }
}
