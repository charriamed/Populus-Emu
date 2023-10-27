namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceFactSheetInformations : AllianceInformations
    {
        public new const short Id = 421;
        public override short TypeId
        {
            get { return Id; }
        }
        public int CreationDate { get; set; }

        public AllianceFactSheetInformations(uint allianceId, string allianceTag, string allianceName, GuildEmblem allianceEmblem, int creationDate)
        {
            this.AllianceId = allianceId;
            this.AllianceTag = allianceTag;
            this.AllianceName = allianceName;
            this.AllianceEmblem = allianceEmblem;
            this.CreationDate = creationDate;
        }

        public AllianceFactSheetInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(CreationDate);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CreationDate = reader.ReadInt();
        }

    }
}
