namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayNamedActorInformations : GameRolePlayActorInformations
    {
        public new const short Id = 154;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Name { get; set; }

        public GameRolePlayNamedActorInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, string name)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Name = name;
        }

        public GameRolePlayNamedActorInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
        }

    }
}
