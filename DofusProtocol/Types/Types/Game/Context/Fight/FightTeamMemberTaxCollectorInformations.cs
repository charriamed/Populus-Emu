namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTeamMemberTaxCollectorInformations : FightTeamMemberInformations
    {
        public new const short Id = 177;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort FirstNameId { get; set; }
        public ushort LastNameId { get; set; }
        public byte Level { get; set; }
        public uint GuildId { get; set; }
        public double Uid { get; set; }

        public FightTeamMemberTaxCollectorInformations(double objectId, ushort firstNameId, ushort lastNameId, byte level, uint guildId, double uid)
        {
            this.ObjectId = objectId;
            this.FirstNameId = firstNameId;
            this.LastNameId = lastNameId;
            this.Level = level;
            this.GuildId = guildId;
            this.Uid = uid;
        }

        public FightTeamMemberTaxCollectorInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(FirstNameId);
            writer.WriteVarUShort(LastNameId);
            writer.WriteByte(Level);
            writer.WriteVarUInt(GuildId);
            writer.WriteDouble(Uid);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            FirstNameId = reader.ReadVarUShort();
            LastNameId = reader.ReadVarUShort();
            Level = reader.ReadByte();
            GuildId = reader.ReadVarUInt();
            Uid = reader.ReadDouble();
        }

    }
}
