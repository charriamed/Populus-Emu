namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyEntityUpdateLightMessage : PartyUpdateLightMessage
    {
        public new const uint Id = 6781;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte IndexId { get; set; }

        public PartyEntityUpdateLightMessage(uint partyId, ulong objectId, uint lifePoints, uint maxLifePoints, ushort prospecting, byte regenRate, sbyte indexId)
        {
            this.PartyId = partyId;
            this.ObjectId = objectId;
            this.LifePoints = lifePoints;
            this.MaxLifePoints = maxLifePoints;
            this.Prospecting = prospecting;
            this.RegenRate = regenRate;
            this.IndexId = indexId;
        }

        public PartyEntityUpdateLightMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(IndexId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            IndexId = reader.ReadSByte();
        }

    }
}
