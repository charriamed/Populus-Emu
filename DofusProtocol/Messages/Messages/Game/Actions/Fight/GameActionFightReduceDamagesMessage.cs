namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightReduceDamagesMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5526;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public uint Amount { get; set; }

        public GameActionFightReduceDamagesMessage(ushort actionId, double sourceId, double targetId, uint amount)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.Amount = amount;
        }

        public GameActionFightReduceDamagesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteVarUInt(Amount);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            Amount = reader.ReadVarUInt();
        }

    }
}
