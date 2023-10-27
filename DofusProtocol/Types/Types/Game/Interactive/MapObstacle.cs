namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapObstacle
    {
        public const short Id  = 200;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ObstacleCellId { get; set; }
        public sbyte State { get; set; }

        public MapObstacle(ushort obstacleCellId, sbyte state)
        {
            this.ObstacleCellId = obstacleCellId;
            this.State = state;
        }

        public MapObstacle() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObstacleCellId);
            writer.WriteSByte(State);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObstacleCellId = reader.ReadVarUShort();
            State = reader.ReadSByte();
        }

    }
}
