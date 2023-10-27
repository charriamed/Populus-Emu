namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInsiderFactSheetInformations : GuildFactSheetInformations
    {
        public new const short Id = 423;
        public override short TypeId
        {
            get { return Id; }
        }
        public string LeaderName { get; set; }
        public ushort NbConnectedMembers { get; set; }
        public sbyte NbTaxCollectors { get; set; }
        public int LastActivity { get; set; }

        public GuildInsiderFactSheetInformations(uint guildId, string guildName, byte guildLevel, GuildEmblem guildEmblem, ulong leaderId, ushort nbMembers, string leaderName, ushort nbConnectedMembers, sbyte nbTaxCollectors, int lastActivity)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
            this.GuildEmblem = guildEmblem;
            this.LeaderId = leaderId;
            this.NbMembers = nbMembers;
            this.LeaderName = leaderName;
            this.NbConnectedMembers = nbConnectedMembers;
            this.NbTaxCollectors = nbTaxCollectors;
            this.LastActivity = lastActivity;
        }

        public GuildInsiderFactSheetInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(LeaderName);
            writer.WriteVarUShort(NbConnectedMembers);
            writer.WriteSByte(NbTaxCollectors);
            writer.WriteInt(LastActivity);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            LeaderName = reader.ReadUTF();
            NbConnectedMembers = reader.ReadVarUShort();
            NbTaxCollectors = reader.ReadSByte();
            LastActivity = reader.ReadInt();
        }

    }
}
