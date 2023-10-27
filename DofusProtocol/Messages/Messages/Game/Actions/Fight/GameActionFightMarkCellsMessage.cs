namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightMarkCellsMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5540;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameActionMark Mark { get; set; }

        public GameActionFightMarkCellsMessage(ushort actionId, double sourceId, GameActionMark mark)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.Mark = mark;
        }

        public GameActionFightMarkCellsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Mark.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Mark = new GameActionMark();
            Mark.Deserialize(reader);
        }

    }
}
