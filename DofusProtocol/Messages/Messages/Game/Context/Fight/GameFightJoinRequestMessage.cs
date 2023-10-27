namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightJoinRequestMessage : Message
    {
        public const uint Id = 701;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double FighterId { get; set; }
        public ushort FightId { get; set; }

        public GameFightJoinRequestMessage(double fighterId, ushort fightId)
        {
            this.FighterId = fighterId;
            this.FightId = fightId;
        }

        public GameFightJoinRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(FighterId);
            writer.WriteVarUShort(FightId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FighterId = reader.ReadDouble();
            FightId = reader.ReadVarUShort();
        }

    }
}
