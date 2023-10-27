namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ActorOrientation
    {
        public const short Id  = 353;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }
        public sbyte Direction { get; set; }

        public ActorOrientation(double objectId, sbyte direction)
        {
            this.ObjectId = objectId;
            this.Direction = direction;
        }

        public ActorOrientation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ObjectId);
            writer.WriteSByte(Direction);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadDouble();
            Direction = reader.ReadSByte();
        }

    }
}
