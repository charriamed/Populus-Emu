namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterMinimalGuildPublicInformations : CharacterMinimalInformations
    {
        public new const short Id = 556;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint Rank { get; set; }

        public CharacterMinimalGuildPublicInformations(ulong objectId, string name, ushort level, uint rank)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.Rank = rank;
        }

        public CharacterMinimalGuildPublicInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Rank);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Rank = reader.ReadVarUInt();
        }

    }
}
