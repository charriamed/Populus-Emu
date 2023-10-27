namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceVersatileInformations
    {
        public const short Id  = 432;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint AllianceId { get; set; }
        public ushort NbGuilds { get; set; }
        public ushort NbMembers { get; set; }
        public ushort NbSubarea { get; set; }

        public AllianceVersatileInformations(uint allianceId, ushort nbGuilds, ushort nbMembers, ushort nbSubarea)
        {
            this.AllianceId = allianceId;
            this.NbGuilds = nbGuilds;
            this.NbMembers = nbMembers;
            this.NbSubarea = nbSubarea;
        }

        public AllianceVersatileInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(AllianceId);
            writer.WriteVarUShort(NbGuilds);
            writer.WriteVarUShort(NbMembers);
            writer.WriteVarUShort(NbSubarea);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            AllianceId = reader.ReadVarUInt();
            NbGuilds = reader.ReadVarUShort();
            NbMembers = reader.ReadVarUShort();
            NbSubarea = reader.ReadVarUShort();
        }

    }
}
