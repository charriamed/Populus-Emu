namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FightAllianceTeamInformations : FightTeamInformations
    {
        public new const short Id = 439;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte Relation { get; set; }

        public FightAllianceTeamInformations(sbyte teamId, double leaderId, sbyte teamSide, sbyte teamTypeId, sbyte nbWaves, FightTeamMemberInformations[] teamMembers, sbyte relation)
        {
            this.TeamId = teamId;
            this.LeaderId = leaderId;
            this.TeamSide = teamSide;
            this.TeamTypeId = teamTypeId;
            this.NbWaves = nbWaves;
            this.TeamMembers = teamMembers;
            this.Relation = relation;
        }

        public FightAllianceTeamInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Relation);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Relation = reader.ReadSByte();
        }

    }
}
