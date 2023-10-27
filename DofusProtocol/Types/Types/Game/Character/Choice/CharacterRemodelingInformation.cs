namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterRemodelingInformation : AbstractCharacterInformation
    {
        public new const short Id = 479;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }
        public ushort CosmeticId { get; set; }
        public int[] Colors { get; set; }

        public CharacterRemodelingInformation(ulong objectId, string name, sbyte breed, bool sex, ushort cosmeticId, int[] colors)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Breed = breed;
            this.Sex = sex;
            this.CosmeticId = cosmeticId;
            this.Colors = colors;
        }

        public CharacterRemodelingInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
            writer.WriteVarUShort(CosmeticId);
            writer.WriteShort((short)Colors.Count());
            for (var colorsIndex = 0; colorsIndex < Colors.Count(); colorsIndex++)
            {
                writer.WriteInt(Colors[colorsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
            CosmeticId = reader.ReadVarUShort();
            var colorsCount = reader.ReadUShort();
            Colors = new int[colorsCount];
            for (var colorsIndex = 0; colorsIndex < colorsCount; colorsIndex++)
            {
                Colors[colorsIndex] = reader.ReadInt();
            }
        }

    }
}
