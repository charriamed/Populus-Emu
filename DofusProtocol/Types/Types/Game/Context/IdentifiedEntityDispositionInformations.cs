namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdentifiedEntityDispositionInformations : EntityDispositionInformations
    {
        public new const short Id = 107;
        public override short TypeId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }

        public IdentifiedEntityDispositionInformations(short cellId, sbyte direction, double objectId)
        {
            this.CellId = cellId;
            this.Direction = direction;
            this.ObjectId = objectId;
        }

        public IdentifiedEntityDispositionInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectId = reader.ReadDouble();
        }

    }
}
