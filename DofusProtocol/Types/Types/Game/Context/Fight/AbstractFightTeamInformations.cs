namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractFightTeamInformations
    {
        public const short Id  = 116;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte TeamId { get; set; }
        public double LeaderId { get; set; }
        public sbyte TeamSide { get; set; }
        public sbyte TeamTypeId { get; set; }
        public sbyte NbWaves { get; set; }

        public AbstractFightTeamInformations(sbyte teamId, double leaderId, sbyte teamSide, sbyte teamTypeId, sbyte nbWaves)
        {
            this.TeamId = teamId;
            this.LeaderId = leaderId;
            this.TeamSide = teamSide;
            this.TeamTypeId = teamTypeId;
            this.NbWaves = nbWaves;
        }

        public AbstractFightTeamInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(TeamId);
            writer.WriteDouble(LeaderId);
            writer.WriteSByte(TeamSide);
            writer.WriteSByte(TeamTypeId);
            writer.WriteSByte(NbWaves);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            TeamId = reader.ReadSByte();
            LeaderId = reader.ReadDouble();
            TeamSide = reader.ReadSByte();
            TeamTypeId = reader.ReadSByte();
            NbWaves = reader.ReadSByte();
        }

    }
}
