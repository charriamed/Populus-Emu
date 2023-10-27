namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FightTeamInformations : AbstractFightTeamInformations
    {
        public new const short Id = 33;
        public override short TypeId
        {
            get { return Id; }
        }
        public FightTeamMemberInformations[] TeamMembers { get; set; }

        public FightTeamInformations(sbyte teamId, double leaderId, sbyte teamSide, sbyte teamTypeId, sbyte nbWaves, FightTeamMemberInformations[] teamMembers)
        {
            this.TeamId = teamId;
            this.LeaderId = leaderId;
            this.TeamSide = teamSide;
            this.TeamTypeId = teamTypeId;
            this.NbWaves = nbWaves;
            this.TeamMembers = teamMembers;
        }

        public FightTeamInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)TeamMembers.Count());
            for (var teamMembersIndex = 0; teamMembersIndex < TeamMembers.Count(); teamMembersIndex++)
            {
                var objectToSend = TeamMembers[teamMembersIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var teamMembersCount = reader.ReadUShort();
            TeamMembers = new FightTeamMemberInformations[teamMembersCount];
            for (var teamMembersIndex = 0; teamMembersIndex < teamMembersCount; teamMembersIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<FightTeamMemberInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                TeamMembers[teamMembersIndex] = objectToAdd;
            }
        }

    }
}
