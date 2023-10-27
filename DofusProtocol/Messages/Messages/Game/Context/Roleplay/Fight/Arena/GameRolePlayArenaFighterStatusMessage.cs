namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaFighterStatusMessage : Message
    {
        public const uint Id = 6281;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public double PlayerId { get; set; }
        public bool Accepted { get; set; }

        public GameRolePlayArenaFighterStatusMessage(ushort fightId, double playerId, bool accepted)
        {
            this.FightId = fightId;
            this.PlayerId = playerId;
            this.Accepted = accepted;
        }

        public GameRolePlayArenaFighterStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteDouble(PlayerId);
            writer.WriteBoolean(Accepted);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            PlayerId = reader.ReadDouble();
            Accepted = reader.ReadBoolean();
        }

    }
}
