namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntStepFollowDirectionToPOI : TreasureHuntStep
    {
        public new const short Id = 461;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte Direction { get; set; }
        public ushort PoiLabelId { get; set; }

        public TreasureHuntStepFollowDirectionToPOI(sbyte direction, ushort poiLabelId)
        {
            this.Direction = direction;
            this.PoiLabelId = poiLabelId;
        }

        public TreasureHuntStepFollowDirectionToPOI() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Direction);
            writer.WriteVarUShort(PoiLabelId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Direction = reader.ReadSByte();
            PoiLabelId = reader.ReadVarUShort();
        }

    }
}
