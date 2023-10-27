namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicNamedAllianceInformations : BasicAllianceInformations
    {
        public new const short Id = 418;
        public override short TypeId
        {
            get { return Id; }
        }
        public string AllianceName { get; set; }

        public BasicNamedAllianceInformations(uint allianceId, string allianceTag, string allianceName)
        {
            this.AllianceId = allianceId;
            this.AllianceTag = allianceTag;
            this.AllianceName = allianceName;
        }

        public BasicNamedAllianceInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(AllianceName);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceName = reader.ReadUTF();
        }

    }
}
