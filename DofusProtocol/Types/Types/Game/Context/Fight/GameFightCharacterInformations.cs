namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightCharacterInformations : GameFightFighterNamedInformations
    {
        public new const short Id = 46;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Level { get; set; }
        public ActorAlignmentInformations AlignmentInfos { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }

        public GameFightCharacterInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, string name, PlayerStatus status, short leagueId, int ladderPosition, bool hiddenInPrefight, ushort level, ActorAlignmentInformations alignmentInfos, sbyte breed, bool sex)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.TeamId = teamId;
            this.Wave = wave;
            this.Alive = alive;
            this.Stats = stats;
            this.PreviousPositions = previousPositions;
            this.Name = name;
            this.Status = status;
            this.LeagueId = leagueId;
            this.LadderPosition = ladderPosition;
            this.HiddenInPrefight = hiddenInPrefight;
            this.Level = level;
            this.AlignmentInfos = alignmentInfos;
            this.Breed = breed;
            this.Sex = sex;
        }

        public GameFightCharacterInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Level);
            AlignmentInfos.Serialize(writer);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadVarUShort();
            AlignmentInfos = new ActorAlignmentInformations();
            AlignmentInfos.Deserialize(reader);
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
        }

    }
}
