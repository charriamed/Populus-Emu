namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorStaticInformations
    {
        public const short Id  = 147;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort FirstNameId { get; set; }
        public ushort LastNameId { get; set; }
        public GuildInformations GuildIdentity { get; set; }

        public TaxCollectorStaticInformations(ushort firstNameId, ushort lastNameId, GuildInformations guildIdentity)
        {
            this.FirstNameId = firstNameId;
            this.LastNameId = lastNameId;
            this.GuildIdentity = guildIdentity;
        }

        public TaxCollectorStaticInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FirstNameId);
            writer.WriteVarUShort(LastNameId);
            GuildIdentity.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            FirstNameId = reader.ReadVarUShort();
            LastNameId = reader.ReadVarUShort();
            GuildIdentity = new GuildInformations();
            GuildIdentity.Deserialize(reader);
        }

    }
}
