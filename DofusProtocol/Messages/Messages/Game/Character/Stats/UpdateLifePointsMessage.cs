namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class UpdateLifePointsMessage : Message
    {
        public const uint Id = 5658;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint LifePoints { get; set; }
        public uint MaxLifePoints { get; set; }

        public UpdateLifePointsMessage(uint lifePoints, uint maxLifePoints)
        {
            this.LifePoints = lifePoints;
            this.MaxLifePoints = maxLifePoints;
        }

        public UpdateLifePointsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(LifePoints);
            writer.WriteVarUInt(MaxLifePoints);
        }

        public override void Deserialize(IDataReader reader)
        {
            LifePoints = reader.ReadVarUInt();
            MaxLifePoints = reader.ReadVarUInt();
        }

    }
}
