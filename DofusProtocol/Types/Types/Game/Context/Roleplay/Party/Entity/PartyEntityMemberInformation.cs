namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyEntityMemberInformation : PartyEntityBaseInformation
    {
        public new const short Id = 550;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Initiative { get; set; }
        public uint LifePoints { get; set; }
        public uint MaxLifePoints { get; set; }
        public ushort Prospecting { get; set; }
        public byte RegenRate { get; set; }

        public PartyEntityMemberInformation(sbyte indexId, sbyte entityModelId, EntityLook entityLook, ushort initiative, uint lifePoints, uint maxLifePoints, ushort prospecting, byte regenRate)
        {
            this.IndexId = indexId;
            this.EntityModelId = entityModelId;
            this.EntityLook = entityLook;
            this.Initiative = initiative;
            this.LifePoints = lifePoints;
            this.MaxLifePoints = maxLifePoints;
            this.Prospecting = prospecting;
            this.RegenRate = regenRate;
        }

        public PartyEntityMemberInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Initiative);
            writer.WriteVarUInt(LifePoints);
            writer.WriteVarUInt(MaxLifePoints);
            writer.WriteVarUShort(Prospecting);
            writer.WriteByte(RegenRate);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Initiative = reader.ReadVarUShort();
            LifePoints = reader.ReadVarUInt();
            MaxLifePoints = reader.ReadVarUInt();
            Prospecting = reader.ReadVarUShort();
            RegenRate = reader.ReadByte();
        }

    }
}
