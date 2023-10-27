namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightStartingMessage : Message
    {
        public const uint Id = 700;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte FightType { get; set; }
        public ushort FightId { get; set; }
        public double AttackerId { get; set; }
        public double DefenderId { get; set; }

        public GameFightStartingMessage(sbyte fightType, ushort fightId, double attackerId, double defenderId)
        {
            this.FightType = fightType;
            this.FightId = fightId;
            this.AttackerId = attackerId;
            this.DefenderId = defenderId;
        }

        public GameFightStartingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(FightType);
            writer.WriteVarUShort(FightId);
            writer.WriteDouble(AttackerId);
            writer.WriteDouble(DefenderId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightType = reader.ReadSByte();
            FightId = reader.ReadVarUShort();
            AttackerId = reader.ReadDouble();
            DefenderId = reader.ReadDouble();
        }

    }
}
