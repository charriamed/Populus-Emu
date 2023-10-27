namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildVersatileInformations
    {
        public const short Id  = 435;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint GuildId { get; set; }
        public ulong LeaderId { get; set; }
        public byte GuildLevel { get; set; }
        public byte NbMembers { get; set; }

        public GuildVersatileInformations(uint guildId, ulong leaderId, byte guildLevel, byte nbMembers)
        {
            this.GuildId = guildId;
            this.LeaderId = leaderId;
            this.GuildLevel = guildLevel;
            this.NbMembers = nbMembers;
        }

        public GuildVersatileInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(GuildId);
            writer.WriteVarULong(LeaderId);
            writer.WriteByte(GuildLevel);
            writer.WriteByte(NbMembers);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            GuildId = reader.ReadVarUInt();
            LeaderId = reader.ReadVarULong();
            GuildLevel = reader.ReadByte();
            NbMembers = reader.ReadByte();
        }

    }
}
