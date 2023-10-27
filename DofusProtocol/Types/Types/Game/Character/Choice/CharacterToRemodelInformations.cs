namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterToRemodelInformations : CharacterRemodelingInformation
    {
        public new const short Id = 477;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte PossibleChangeMask { get; set; }
        public sbyte MandatoryChangeMask { get; set; }

        public CharacterToRemodelInformations(ulong objectId, string name, sbyte breed, bool sex, ushort cosmeticId, int[] colors, sbyte possibleChangeMask, sbyte mandatoryChangeMask)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Breed = breed;
            this.Sex = sex;
            this.CosmeticId = cosmeticId;
            this.Colors = colors;
            this.PossibleChangeMask = possibleChangeMask;
            this.MandatoryChangeMask = mandatoryChangeMask;
        }

        public CharacterToRemodelInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(PossibleChangeMask);
            writer.WriteSByte(MandatoryChangeMask);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PossibleChangeMask = reader.ReadSByte();
            MandatoryChangeMask = reader.ReadSByte();
        }

    }
}
