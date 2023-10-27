namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightUnmarkCellsMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5570;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short MarkId { get; set; }

        public GameActionFightUnmarkCellsMessage(ushort actionId, double sourceId, short markId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.MarkId = markId;
        }

        public GameActionFightUnmarkCellsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(MarkId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MarkId = reader.ReadShort();
        }

    }
}
