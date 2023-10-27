namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayActorInformations : GameContextActorInformations
    {
        public new const short Id = 141;
        public override short TypeId
        {
            get { return Id; }
        }

        public GameRolePlayActorInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
        }

        public GameRolePlayActorInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
