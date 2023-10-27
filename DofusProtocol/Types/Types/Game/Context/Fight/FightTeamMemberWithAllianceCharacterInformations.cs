namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTeamMemberWithAllianceCharacterInformations : FightTeamMemberCharacterInformations
    {
        public new const short Id = 426;
        public override short TypeId
        {
            get { return Id; }
        }
        public BasicAllianceInformations AllianceInfos { get; set; }

        public FightTeamMemberWithAllianceCharacterInformations(double objectId, string name, ushort level, BasicAllianceInformations allianceInfos)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.AllianceInfos = allianceInfos;
        }

        public FightTeamMemberWithAllianceCharacterInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            AllianceInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceInfos = new BasicAllianceInformations();
            AllianceInfos.Deserialize(reader);
        }

    }
}
