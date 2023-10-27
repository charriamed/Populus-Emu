namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightDodgePointLossMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5828;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public ushort Amount { get; set; }

        public GameActionFightDodgePointLossMessage(ushort actionId, double sourceId, double targetId, ushort amount)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.Amount = amount;
        }

        public GameActionFightDodgePointLossMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteVarUShort(Amount);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            Amount = reader.ReadVarUShort();
        }

    }
}
