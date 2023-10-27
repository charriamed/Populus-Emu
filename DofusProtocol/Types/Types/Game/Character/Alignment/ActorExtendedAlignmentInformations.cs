namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ActorExtendedAlignmentInformations : ActorAlignmentInformations
    {
        public new const short Id = 202;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Honor { get; set; }
        public ushort HonorGradeFloor { get; set; }
        public ushort HonorNextGradeFloor { get; set; }
        public sbyte Aggressable { get; set; }

        public ActorExtendedAlignmentInformations(sbyte alignmentSide, sbyte alignmentValue, sbyte alignmentGrade, double characterPower, ushort honor, ushort honorGradeFloor, ushort honorNextGradeFloor, sbyte aggressable)
        {
            this.AlignmentSide = alignmentSide;
            this.AlignmentValue = alignmentValue;
            this.AlignmentGrade = alignmentGrade;
            this.CharacterPower = characterPower;
            this.Honor = honor;
            this.HonorGradeFloor = honorGradeFloor;
            this.HonorNextGradeFloor = honorNextGradeFloor;
            this.Aggressable = aggressable;
        }

        public ActorExtendedAlignmentInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Honor);
            writer.WriteVarUShort(HonorGradeFloor);
            writer.WriteVarUShort(HonorNextGradeFloor);
            writer.WriteSByte(Aggressable);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Honor = reader.ReadVarUShort();
            HonorGradeFloor = reader.ReadVarUShort();
            HonorNextGradeFloor = reader.ReadVarUShort();
            Aggressable = reader.ReadSByte();
        }

    }
}
