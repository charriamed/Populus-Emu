namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class Preset
    {
        public const short Id  = 355;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public short ObjectId { get; set; }

        public Preset(short objectId)
        {
            this.ObjectId = objectId;
        }

        public Preset() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort(ObjectId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadShort();
        }

    }
}
