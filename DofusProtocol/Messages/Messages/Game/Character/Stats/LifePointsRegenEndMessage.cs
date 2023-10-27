namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LifePointsRegenEndMessage : UpdateLifePointsMessage
    {
        public new const uint Id = 5686;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint LifePointsGained { get; set; }

        public LifePointsRegenEndMessage(uint lifePoints, uint maxLifePoints, uint lifePointsGained)
        {
            this.LifePoints = lifePoints;
            this.MaxLifePoints = maxLifePoints;
            this.LifePointsGained = lifePointsGained;
        }

        public LifePointsRegenEndMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(LifePointsGained);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            LifePointsGained = reader.ReadVarUInt();
        }

    }
}
