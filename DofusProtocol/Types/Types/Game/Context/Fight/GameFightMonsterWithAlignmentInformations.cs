namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightMonsterWithAlignmentInformations : GameFightMonsterInformations
    {
        public new const short Id = 203;
        public override short TypeId
        {
            get { return Id; }
        }
        public ActorAlignmentInformations AlignmentInfos { get; set; }

        public GameFightMonsterWithAlignmentInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, ushort creatureGenericId, sbyte creatureGrade, short creatureLevel, ActorAlignmentInformations alignmentInfos)
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
            this.AlignmentInfos = alignmentInfos;
        }

        public GameFightMonsterWithAlignmentInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            AlignmentInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AlignmentInfos = new ActorAlignmentInformations();
            AlignmentInfos.Deserialize(reader);
        }

    }
}
