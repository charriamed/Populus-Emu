namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightStealKamaMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5535;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public ulong Amount { get; set; }

        public GameActionFightStealKamaMessage(ushort actionId, double sourceId, double targetId, ulong amount)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.Amount = amount;
        }

        public GameActionFightStealKamaMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteVarULong(Amount);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            Amount = reader.ReadVarULong();
        }

    }
}
