namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AlliancePrismInformation : PrismInformation
    {
        public new const short Id = 427;
        public override short TypeId
        {
            get { return Id; }
        }
        public AllianceInformations Alliance { get; set; }

        public AlliancePrismInformation(sbyte @typeId, sbyte state, int nextVulnerabilityDate, int placementDate, uint rewardTokenCount, AllianceInformations alliance)
        {
            this.@typeId = @typeId;
            this.State = state;
            this.NextVulnerabilityDate = nextVulnerabilityDate;
            this.PlacementDate = placementDate;
            this.RewardTokenCount = rewardTokenCount;
            this.Alliance = alliance;
        }

        public AlliancePrismInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Alliance.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Alliance = new AllianceInformations();
            Alliance.Deserialize(reader);
        }

    }
}
