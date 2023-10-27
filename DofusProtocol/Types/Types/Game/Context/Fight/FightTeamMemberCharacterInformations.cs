namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTeamMemberCharacterInformations : FightTeamMemberInformations
    {
        public new const short Id = 13;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public ushort Level { get; set; }

        public FightTeamMemberCharacterInformations(double objectId, string name, ushort level)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
        }

        public FightTeamMemberCharacterInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
            writer.WriteVarUShort(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
            Level = reader.ReadVarUShort();
        }

    }
}
