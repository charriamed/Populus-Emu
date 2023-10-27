namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceInformations : BasicNamedAllianceInformations
    {
        public new const short Id = 417;
        public override short TypeId
        {
            get { return Id; }
        }
        public GuildEmblem AllianceEmblem { get; set; }

        public AllianceInformations(uint allianceId, string allianceTag, string allianceName, GuildEmblem allianceEmblem)
        {
            this.AllianceId = allianceId;
            this.AllianceTag = allianceTag;
            this.AllianceName = allianceName;
            this.AllianceEmblem = allianceEmblem;
        }

        public AllianceInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            AllianceEmblem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceEmblem = new GuildEmblem();
            AllianceEmblem.Deserialize(reader);
        }

    }
}
