namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayMonsterAngryAtPlayerMessage : Message
    {
        public const uint Id = 6741;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public double MonsterGroupId { get; set; }
        public double AngryStartTime { get; set; }
        public double AttackTime { get; set; }

        public GameRolePlayMonsterAngryAtPlayerMessage(ulong playerId, double monsterGroupId, double angryStartTime, double attackTime)
        {
            this.PlayerId = playerId;
            this.MonsterGroupId = monsterGroupId;
            this.AngryStartTime = angryStartTime;
            this.AttackTime = attackTime;
        }

        public GameRolePlayMonsterAngryAtPlayerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(PlayerId);
            writer.WriteDouble(MonsterGroupId);
            writer.WriteDouble(AngryStartTime);
            writer.WriteDouble(AttackTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarULong();
            MonsterGroupId = reader.ReadDouble();
            AngryStartTime = reader.ReadDouble();
            AttackTime = reader.ReadDouble();
        }

    }
}
