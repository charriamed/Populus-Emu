namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightResultPvpData : FightResultAdditionalData
    {
        public new const short Id = 190;
        public override short TypeId
        {
            get { return Id; }
        }
        public byte Grade { get; set; }
        public ushort MinHonorForGrade { get; set; }
        public ushort MaxHonorForGrade { get; set; }
        public ushort Honor { get; set; }
        public short HonorDelta { get; set; }

        public FightResultPvpData(byte grade, ushort minHonorForGrade, ushort maxHonorForGrade, ushort honor, short honorDelta)
        {
            this.Grade = grade;
            this.MinHonorForGrade = minHonorForGrade;
            this.MaxHonorForGrade = maxHonorForGrade;
            this.Honor = honor;
            this.HonorDelta = honorDelta;
        }

        public FightResultPvpData() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(Grade);
            writer.WriteVarUShort(MinHonorForGrade);
            writer.WriteVarUShort(MaxHonorForGrade);
            writer.WriteVarUShort(Honor);
            writer.WriteVarShort(HonorDelta);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Grade = reader.ReadByte();
            MinHonorForGrade = reader.ReadVarUShort();
            MaxHonorForGrade = reader.ReadVarUShort();
            Honor = reader.ReadVarUShort();
            HonorDelta = reader.ReadVarShort();
        }

    }
}
