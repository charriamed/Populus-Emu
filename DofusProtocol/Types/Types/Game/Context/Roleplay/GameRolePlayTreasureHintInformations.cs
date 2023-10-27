namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayTreasureHintInformations : GameRolePlayActorInformations
    {
        public new const short Id = 471;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort NpcId { get; set; }

        public GameRolePlayTreasureHintInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, ushort npcId)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.NpcId = npcId;
        }

        public GameRolePlayTreasureHintInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(NpcId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            NpcId = reader.ReadVarUShort();
        }

    }
}
