namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayPortalInformations : GameRolePlayActorInformations
    {
        public new const short Id = 467;
        public override short TypeId
        {
            get { return Id; }
        }
        public PortalInformation Portal { get; set; }

        public GameRolePlayPortalInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, PortalInformation portal)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Portal = portal;
        }

        public GameRolePlayPortalInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(Portal.TypeId);
            Portal.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Portal = ProtocolTypeManager.GetInstance<PortalInformation>(reader.ReadShort());
            Portal.Deserialize(reader);
        }

    }
}
