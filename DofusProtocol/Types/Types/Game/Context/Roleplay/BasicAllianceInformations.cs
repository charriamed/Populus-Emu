namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicAllianceInformations : AbstractSocialGroupInfos
    {
        public new const short Id = 419;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint AllianceId { get; set; }
        public string AllianceTag { get; set; }

        public BasicAllianceInformations(uint allianceId, string allianceTag)
        {
            this.AllianceId = allianceId;
            this.AllianceTag = allianceTag;
        }

        public BasicAllianceInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(AllianceId);
            writer.WriteUTF(AllianceTag);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceId = reader.ReadVarUInt();
            AllianceTag = reader.ReadUTF();
        }

    }
}
