namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightCarryCharacterMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5830;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public short CellId { get; set; }

        public GameActionFightCarryCharacterMessage(ushort actionId, double sourceId, double targetId, short cellId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.CellId = cellId;
        }

        public GameActionFightCarryCharacterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteShort(CellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            CellId = reader.ReadShort();
        }

    }
}
