namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTeamMemberEntityInformation : FightTeamMemberInformations
    {
        public new const short Id = 549;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte EntityModelId { get; set; }
        public ushort Level { get; set; }
        public double MasterId { get; set; }

        public FightTeamMemberEntityInformation(double objectId, sbyte entityModelId, ushort level, double masterId)
        {
            this.ObjectId = objectId;
            this.EntityModelId = entityModelId;
            this.Level = level;
            this.MasterId = masterId;
        }

        public FightTeamMemberEntityInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(EntityModelId);
            writer.WriteVarUShort(Level);
            writer.WriteDouble(MasterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EntityModelId = reader.ReadSByte();
            Level = reader.ReadVarUShort();
            MasterId = reader.ReadDouble();
        }

    }
}
