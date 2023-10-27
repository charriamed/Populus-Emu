namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayAggressionMessage : Message
    {
        public const uint Id = 6073;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong AttackerId { get; set; }
        public ulong DefenderId { get; set; }

        public GameRolePlayAggressionMessage(ulong attackerId, ulong defenderId)
        {
            this.AttackerId = attackerId;
            this.DefenderId = defenderId;
        }

        public GameRolePlayAggressionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(AttackerId);
            writer.WriteVarULong(DefenderId);
        }

        public override void Deserialize(IDataReader reader)
        {
            AttackerId = reader.ReadVarULong();
            DefenderId = reader.ReadVarULong();
        }

    }
}
