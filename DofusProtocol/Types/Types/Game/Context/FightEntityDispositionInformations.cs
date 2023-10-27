namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightEntityDispositionInformations : EntityDispositionInformations
    {
        public new const short Id = 217;
        public override short TypeId
        {
            get { return Id; }
        }
        public double CarryingCharacterId { get; set; }

        public FightEntityDispositionInformations(short cellId, sbyte direction, double carryingCharacterId)
        {
            this.CellId = cellId;
            this.Direction = direction;
            this.CarryingCharacterId = carryingCharacterId;
        }

        public FightEntityDispositionInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(CarryingCharacterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CarryingCharacterId = reader.ReadDouble();
        }

    }
}
