namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractCharacterInformation
    {
        public const short Id  = 400;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ulong ObjectId { get; set; }

        public AbstractCharacterInformation(ulong objectId)
        {
            this.ObjectId = objectId;
        }

        public AbstractCharacterInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(ObjectId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarULong();
        }

    }
}
