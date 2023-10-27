namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFightPlayersEnemyRemoveMessage : Message
    {
        public const uint Id = 5929;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double FightId { get; set; }
        public ulong PlayerId { get; set; }

        public GuildFightPlayersEnemyRemoveMessage(double fightId, ulong playerId)
        {
            this.FightId = fightId;
            this.PlayerId = playerId;
        }

        public GuildFightPlayersEnemyRemoveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(FightId);
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadDouble();
            PlayerId = reader.ReadVarULong();
        }

    }
}
