namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntFlag
    {
        public const short Id  = 473;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double MapId { get; set; }
        public sbyte State { get; set; }

        public TreasureHuntFlag(double mapId, sbyte state)
        {
            this.MapId = mapId;
            this.State = state;
        }

        public TreasureHuntFlag() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
            writer.WriteSByte(State);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
            State = reader.ReadSByte();
        }

    }
}
