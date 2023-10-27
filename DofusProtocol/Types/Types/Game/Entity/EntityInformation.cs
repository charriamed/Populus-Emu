namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EntityInformation
    {
        public const short Id  = 546;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectId { get; set; }
        public uint Experience { get; set; }
        public bool Status { get; set; }

        public EntityInformation(ushort objectId, uint experience, bool status)
        {
            this.ObjectId = objectId;
            this.Experience = experience;
            this.Status = status;
        }

        public EntityInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectId);
            writer.WriteVarUInt(Experience);
            writer.WriteBoolean(Status);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUShort();
            Experience = reader.ReadVarUInt();
            Status = reader.ReadBoolean();
        }

    }
}
