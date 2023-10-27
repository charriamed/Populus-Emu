namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage : GameRolePlayArenaUpdatePlayerInfosMessage
    {
        public new const uint Id = 6728;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ArenaRankInfos Team { get; set; }
        public ArenaRankInfos Duel { get; set; }

        public GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage(ArenaRankInfos solo, ArenaRankInfos team, ArenaRankInfos duel)
        {
            this.Solo = solo;
            this.Team = team;
            this.Duel = duel;
        }

        public GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Team.Serialize(writer);
            Duel.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Team = new ArenaRankInfos();
            Team.Deserialize(reader);
            Duel = new ArenaRankInfos();
            Duel.Deserialize(reader);
        }

    }
}
