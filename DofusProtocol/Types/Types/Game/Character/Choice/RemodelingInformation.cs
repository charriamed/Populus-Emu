namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class RemodelingInformation
    {
        public const short Id  = 480;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }
        public ushort CosmeticId { get; set; }
        public int[] Colors { get; set; }

        public RemodelingInformation(string name, sbyte breed, bool sex, ushort cosmeticId, int[] colors)
        {
            this.Name = name;
            this.Breed = breed;
            this.Sex = sex;
            this.CosmeticId = cosmeticId;
            this.Colors = colors;
        }

        public RemodelingInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
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

        public virtual void Deserialize(IDataReader reader)
        {
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
