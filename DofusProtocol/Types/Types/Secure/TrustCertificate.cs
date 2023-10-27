namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TrustCertificate
    {
        public const short Id  = 377;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int ObjectId { get; set; }
        public string Hash { get; set; }

        public TrustCertificate(int objectId, string hash)
        {
            this.ObjectId = objectId;
            this.Hash = hash;
        }

        public TrustCertificate() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(ObjectId);
            writer.WriteUTF(Hash);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadInt();
            Hash = reader.ReadUTF();
        }

    }
}
