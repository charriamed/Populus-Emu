namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightFighterNamedInformations : GameFightFighterInformations
    {
        public new const short Id = 158;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public PlayerStatus Status { get; set; }
        public short LeagueId { get; set; }
        public int LadderPosition { get; set; }
        public bool HiddenInPrefight { get; set; }

        public GameFightFighterNamedInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, string name, PlayerStatus status, short leagueId, int ladderPosition, bool hiddenInPrefight)
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
        }

        public GameFightFighterNamedInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
            Status.Serialize(writer);
            writer.WriteVarShort(LeagueId);
            writer.WriteInt(LadderPosition);
            writer.WriteBoolean(HiddenInPrefight);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
            Status = new PlayerStatus();
            Status.Deserialize(reader);
            LeagueId = reader.ReadVarShort();
            LadderPosition = reader.ReadInt();
            HiddenInPrefight = reader.ReadBoolean();
        }

    }
}
