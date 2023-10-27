namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayNpcInformations : GameRolePlayActorInformations
    {
        public new const short Id = 156;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort NpcId { get; set; }
        public bool Sex { get; set; }
        public ushort SpecialArtworkId { get; set; }

        public GameRolePlayNpcInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, ushort npcId, bool sex, ushort specialArtworkId)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.NpcId = npcId;
            this.Sex = sex;
            this.SpecialArtworkId = specialArtworkId;
        }

        public GameRolePlayNpcInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(NpcId);
            writer.WriteBoolean(Sex);
            writer.WriteVarUShort(SpecialArtworkId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            NpcId = reader.ReadVarUShort();
            Sex = reader.ReadBoolean();
            SpecialArtworkId = reader.ReadVarUShort();
        }

    }
}
