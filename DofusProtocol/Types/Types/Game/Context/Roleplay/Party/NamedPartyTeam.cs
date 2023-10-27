namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NamedPartyTeam
    {
        public const short Id  = 469;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte TeamId { get; set; }
        public string PartyName { get; set; }

        public NamedPartyTeam(sbyte teamId, string partyName)
        {
            this.TeamId = teamId;
            this.PartyName = partyName;
        }

        public NamedPartyTeam() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(TeamId);
            writer.WriteUTF(PartyName);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            TeamId = reader.ReadSByte();
            PartyName = reader.ReadUTF();
        }

    }
}
