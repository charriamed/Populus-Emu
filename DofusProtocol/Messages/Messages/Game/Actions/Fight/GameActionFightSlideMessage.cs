namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightSlideMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5525;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public short StartCellId { get; set; }
        public short EndCellId { get; set; }

        public GameActionFightSlideMessage(ushort actionId, double sourceId, double targetId, short startCellId, short endCellId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.StartCellId = startCellId;
            this.EndCellId = endCellId;
        }

        public GameActionFightSlideMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteShort(StartCellId);
            writer.WriteShort(EndCellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            StartCellId = reader.ReadShort();
            EndCellId = reader.ReadShort();
        }

    }
}
