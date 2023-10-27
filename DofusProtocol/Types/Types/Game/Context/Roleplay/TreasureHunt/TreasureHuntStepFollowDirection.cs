namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntStepFollowDirection : TreasureHuntStep
    {
        public new const short Id = 468;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte Direction { get; set; }
        public ushort MapCount { get; set; }

        public TreasureHuntStepFollowDirection(sbyte direction, ushort mapCount)
        {
            this.Direction = direction;
            this.MapCount = mapCount;
        }

        public TreasureHuntStepFollowDirection() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Direction);
            writer.WriteVarUShort(MapCount);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Direction = reader.ReadSByte();
            MapCount = reader.ReadVarUShort();
        }

    }
}
