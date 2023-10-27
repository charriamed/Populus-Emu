namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayPrismInformations : GameRolePlayActorInformations
    {
        public new const short Id = 161;
        public override short TypeId
        {
            get { return Id; }
        }
        public PrismInformation Prism { get; set; }

        public GameRolePlayPrismInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, PrismInformation prism)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Prism = prism;
        }

        public GameRolePlayPrismInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(Prism.TypeId);
            Prism.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Prism = ProtocolTypeManager.GetInstance<PrismInformation>(reader.ReadShort());
            Prism.Deserialize(reader);
        }

    }
}
