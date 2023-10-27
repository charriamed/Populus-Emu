namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorStaticExtendedInformations : TaxCollectorStaticInformations
    {
        public new const short Id = 440;
        public override short TypeId
        {
            get { return Id; }
        }
        public AllianceInformations AllianceIdentity { get; set; }

        public TaxCollectorStaticExtendedInformations(ushort firstNameId, ushort lastNameId, GuildInformations guildIdentity, AllianceInformations allianceIdentity)
        {
            this.FirstNameId = firstNameId;
            this.LastNameId = lastNameId;
            this.GuildIdentity = guildIdentity;
            this.AllianceIdentity = allianceIdentity;
        }

        public TaxCollectorStaticExtendedInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            AllianceIdentity.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceIdentity = new AllianceInformations();
            AllianceIdentity.Deserialize(reader);
        }

    }
}
