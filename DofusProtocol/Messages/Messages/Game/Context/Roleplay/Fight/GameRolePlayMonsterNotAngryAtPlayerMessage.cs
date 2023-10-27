namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayMonsterNotAngryAtPlayerMessage : Message
    {
        public const uint Id = 6742;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public double MonsterGroupId { get; set; }

        public GameRolePlayMonsterNotAngryAtPlayerMessage(ulong playerId, double monsterGroupId)
        {
            this.PlayerId = playerId;
            this.MonsterGroupId = monsterGroupId;
        }

        public GameRolePlayMonsterNotAngryAtPlayerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(PlayerId);
            writer.WriteDouble(MonsterGroupId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarULong();
            MonsterGroupId = reader.ReadDouble();
        }

    }
}
