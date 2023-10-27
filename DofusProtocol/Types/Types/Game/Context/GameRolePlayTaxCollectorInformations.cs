namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayTaxCollectorInformations : GameRolePlayActorInformations
    {
        public new const short Id = 148;
        public override short TypeId
        {
            get { return Id; }
        }
        public TaxCollectorStaticInformations Identification { get; set; }
        public byte GuildLevel { get; set; }
        public int TaxCollectorAttack { get; set; }

        public GameRolePlayTaxCollectorInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, TaxCollectorStaticInformations identification, byte guildLevel, int taxCollectorAttack)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Identification = identification;
            this.GuildLevel = guildLevel;
            this.TaxCollectorAttack = taxCollectorAttack;
        }

        public GameRolePlayTaxCollectorInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(Identification.TypeId);
            Identification.Serialize(writer);
            writer.WriteByte(GuildLevel);
            writer.WriteInt(TaxCollectorAttack);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Identification = ProtocolTypeManager.GetInstance<TaxCollectorStaticInformations>(reader.ReadShort());
            Identification.Deserialize(reader);
            GuildLevel = reader.ReadByte();
            TaxCollectorAttack = reader.ReadInt();
        }

    }
}
