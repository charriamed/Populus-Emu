namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyUpdateLightMessage : AbstractPartyEventMessage
    {
        public new const uint Id = 6054;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong ObjectId { get; set; }
        public uint LifePoints { get; set; }
        public uint MaxLifePoints { get; set; }
        public ushort Prospecting { get; set; }
        public byte RegenRate { get; set; }

        public PartyUpdateLightMessage(uint partyId, ulong objectId, uint lifePoints, uint maxLifePoints, ushort prospecting, byte regenRate)
        {
            this.PartyId = partyId;
            this.ObjectId = objectId;
            this.LifePoints = lifePoints;
            this.MaxLifePoints = maxLifePoints;
            this.Prospecting = prospecting;
            this.RegenRate = regenRate;
        }

        public PartyUpdateLightMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(ObjectId);
            writer.WriteVarUInt(LifePoints);
            writer.WriteVarUInt(MaxLifePoints);
            writer.WriteVarUShort(Prospecting);
            writer.WriteByte(RegenRate);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectId = reader.ReadVarULong();
            LifePoints = reader.ReadVarUInt();
            MaxLifePoints = reader.ReadVarUInt();
            Prospecting = reader.ReadVarUShort();
            RegenRate = reader.ReadByte();
        }

    }
}
