namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ActorAlignmentInformations
    {
        public const short Id  = 201;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte AlignmentSide { get; set; }
        public sbyte AlignmentValue { get; set; }
        public sbyte AlignmentGrade { get; set; }
        public double CharacterPower { get; set; }

        public ActorAlignmentInformations(sbyte alignmentSide, sbyte alignmentValue, sbyte alignmentGrade, double characterPower)
        {
            this.AlignmentSide = alignmentSide;
            this.AlignmentValue = alignmentValue;
            this.AlignmentGrade = alignmentGrade;
            this.CharacterPower = characterPower;
        }

        public ActorAlignmentInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(AlignmentSide);
            writer.WriteSByte(AlignmentValue);
            writer.WriteSByte(AlignmentGrade);
            writer.WriteDouble(CharacterPower);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            AlignmentSide = reader.ReadSByte();
            AlignmentValue = reader.ReadSByte();
            AlignmentGrade = reader.ReadSByte();
            CharacterPower = reader.ReadDouble();
        }

    }
}
