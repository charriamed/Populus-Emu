namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInAllianceVersatileInformations : GuildVersatileInformations
    {
        public new const short Id = 437;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint AllianceId { get; set; }

        public GuildInAllianceVersatileInformations(uint guildId, ulong leaderId, byte guildLevel, byte nbMembers, uint allianceId)
        {
            this.GuildId = guildId;
            this.LeaderId = leaderId;
            this.GuildLevel = guildLevel;
            this.NbMembers = nbMembers;
            this.AllianceId = allianceId;
        }

        public GuildInAllianceVersatileInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(AllianceId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceId = reader.ReadVarUInt();
        }

    }
}
