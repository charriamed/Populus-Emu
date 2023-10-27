namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFactSheetInformations : GuildInformations
    {
        public new const short Id = 424;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong LeaderId { get; set; }
        public ushort NbMembers { get; set; }

        public GuildFactSheetInformations(uint guildId, string guildName, byte guildLevel, GuildEmblem guildEmblem, ulong leaderId, ushort nbMembers)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
            this.GuildEmblem = guildEmblem;
            this.LeaderId = leaderId;
            this.NbMembers = nbMembers;
        }

        public GuildFactSheetInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(LeaderId);
            writer.WriteVarUShort(NbMembers);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            LeaderId = reader.ReadVarULong();
            NbMembers = reader.ReadVarUShort();
        }

    }
}
