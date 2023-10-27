namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayMountInformations : GameRolePlayNamedActorInformations
    {
        public new const short Id = 180;
        public override short TypeId
        {
            get { return Id; }
        }
        public string OwnerName { get; set; }
        public byte Level { get; set; }

        public GameRolePlayMountInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, string name, string ownerName, byte level)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Name = name;
            this.OwnerName = ownerName;
            this.Level = level;
        }

        public GameRolePlayMountInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(OwnerName);
            writer.WriteByte(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            OwnerName = reader.ReadUTF();
            Level = reader.ReadByte();
        }

    }
}
