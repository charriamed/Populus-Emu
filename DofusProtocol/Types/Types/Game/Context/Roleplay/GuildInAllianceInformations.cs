namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInAllianceInformations : GuildInformations
    {
        public new const short Id = 420;
        public override short TypeId
        {
            get { return Id; }
        }
        public byte NbMembers { get; set; }
        public int JoinDate { get; set; }

        public GuildInAllianceInformations(uint guildId, string guildName, byte guildLevel, GuildEmblem guildEmblem, byte nbMembers, int joinDate)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
            this.GuildEmblem = guildEmblem;
            this.NbMembers = nbMembers;
            this.JoinDate = joinDate;
        }

        public GuildInAllianceInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(NbMembers);
            writer.WriteInt(JoinDate);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            NbMembers = reader.ReadByte();
            JoinDate = reader.ReadInt();
        }

    }
}
