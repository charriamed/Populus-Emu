namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaLeagueRewardsMessage : Message
    {
        public const uint Id = 6785;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SeasonId { get; set; }
        public ushort LeagueId { get; set; }
        public int LadderPosition { get; set; }
        public bool EndSeasonReward { get; set; }

        public GameRolePlayArenaLeagueRewardsMessage(ushort seasonId, ushort leagueId, int ladderPosition, bool endSeasonReward)
        {
            this.SeasonId = seasonId;
            this.LeagueId = leagueId;
            this.LadderPosition = ladderPosition;
            this.EndSeasonReward = endSeasonReward;
        }

        public GameRolePlayArenaLeagueRewardsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SeasonId);
            writer.WriteVarUShort(LeagueId);
            writer.WriteInt(LadderPosition);
            writer.WriteBoolean(EndSeasonReward);
        }

        public override void Deserialize(IDataReader reader)
        {
            SeasonId = reader.ReadVarUShort();
            LeagueId = reader.ReadVarUShort();
            LadderPosition = reader.ReadInt();
            EndSeasonReward = reader.ReadBoolean();
        }

    }
}
