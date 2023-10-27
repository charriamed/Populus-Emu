namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayAttackMonsterRequestMessage : Message
    {
        public const uint Id = 6191;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MonsterGroupId { get; set; }

        public GameRolePlayAttackMonsterRequestMessage(double monsterGroupId)
        {
            this.MonsterGroupId = monsterGroupId;
        }

        public GameRolePlayAttackMonsterRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MonsterGroupId);
        }

        public override void Deserialize(IDataReader reader)
        {
            MonsterGroupId = reader.ReadDouble();
        }

    }
}
