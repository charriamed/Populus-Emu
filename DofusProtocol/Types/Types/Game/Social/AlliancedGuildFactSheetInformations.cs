namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AlliancedGuildFactSheetInformations : GuildInformations
    {
        public new const short Id = 422;
        public override short TypeId
        {
            get { return Id; }
        }
        public BasicNamedAllianceInformations AllianceInfos { get; set; }

        public AlliancedGuildFactSheetInformations(uint guildId, string guildName, byte guildLevel, GuildEmblem guildEmblem, BasicNamedAllianceInformations allianceInfos)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
            this.GuildEmblem = guildEmblem;
            this.AllianceInfos = allianceInfos;
        }

        public AlliancedGuildFactSheetInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            AllianceInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceInfos = new BasicNamedAllianceInformations();
            AllianceInfos.Deserialize(reader);
        }

    }
}
