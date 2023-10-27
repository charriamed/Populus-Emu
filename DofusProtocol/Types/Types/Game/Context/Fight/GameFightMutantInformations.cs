namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightMutantInformations : GameFightFighterNamedInformations
    {
        public new const short Id = 50;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte PowerLevel { get; set; }

        public GameFightMutantInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, string name, PlayerStatus status, short leagueId, int ladderPosition, bool hiddenInPrefight, sbyte powerLevel)
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
            this.PowerLevel = powerLevel;
        }

        public GameFightMutantInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(PowerLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PowerLevel = reader.ReadSByte();
        }

    }
}
