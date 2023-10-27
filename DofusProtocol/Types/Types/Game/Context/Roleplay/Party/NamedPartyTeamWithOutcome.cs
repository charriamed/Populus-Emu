namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NamedPartyTeamWithOutcome
    {
        public const short Id  = 470;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public NamedPartyTeam Team { get; set; }
        public ushort Outcome { get; set; }

        public NamedPartyTeamWithOutcome(NamedPartyTeam team, ushort outcome)
        {
            this.Team = team;
            this.Outcome = outcome;
        }

        public NamedPartyTeamWithOutcome() { }

        public virtual void Serialize(IDataWriter writer)
        {
            Team.Serialize(writer);
            writer.WriteVarUShort(Outcome);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Team = new NamedPartyTeam();
            Team.Deserialize(reader);
            Outcome = reader.ReadVarUShort();
        }

    }
}
