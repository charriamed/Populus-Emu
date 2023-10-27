namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightExchangePositionsMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5527;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public short CasterCellId { get; set; }
        public short TargetCellId { get; set; }

        public GameActionFightExchangePositionsMessage(ushort actionId, double sourceId, double targetId, short casterCellId, short targetCellId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.CasterCellId = casterCellId;
            this.TargetCellId = targetCellId;
        }

        public GameActionFightExchangePositionsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteShort(CasterCellId);
            writer.WriteShort(TargetCellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            CasterCellId = reader.ReadShort();
            TargetCellId = reader.ReadShort();
        }

    }
}
