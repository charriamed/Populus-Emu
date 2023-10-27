namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightMonsterInformations : GameFightAIInformations
    {
        public new const short Id = 29;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort CreatureGenericId { get; set; }
        public sbyte CreatureGrade { get; set; }
        public short CreatureLevel { get; set; }

        public GameFightMonsterInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, ushort creatureGenericId, sbyte creatureGrade, short creatureLevel)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.TeamId = teamId;
            this.Wave = wave;
            this.Alive = alive;
            this.Stats = stats;
            this.PreviousPositions = previousPositions;
            this.CreatureGenericId = creatureGenericId;
            this.CreatureGrade = creatureGrade;
            this.CreatureLevel = creatureLevel;
        }

        public GameFightMonsterInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(CreatureGenericId);
            writer.WriteSByte(CreatureGrade);
            writer.WriteShort(CreatureLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CreatureGenericId = reader.ReadVarUShort();
            CreatureGrade = reader.ReadSByte();
            CreatureLevel = reader.ReadShort();
        }

    }
}
