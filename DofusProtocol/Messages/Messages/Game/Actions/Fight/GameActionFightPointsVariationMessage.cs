namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightPointsVariationMessage : AbstractGameActionMessage
    {
        public new const uint Id = 1030;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public short Delta { get; set; }

        public GameActionFightPointsVariationMessage(ushort actionId, double sourceId, double targetId, short delta)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.Delta = delta;
        }

        public GameActionFightPointsVariationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteShort(Delta);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            Delta = reader.ReadShort();
        }

    }
}
