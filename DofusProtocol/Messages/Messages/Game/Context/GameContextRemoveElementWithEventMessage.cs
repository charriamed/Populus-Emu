namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextRemoveElementWithEventMessage : GameContextRemoveElementMessage
    {
        public new const uint Id = 6412;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ElementEventId { get; set; }

        public GameContextRemoveElementWithEventMessage(double objectId, sbyte elementEventId)
        {
            this.ObjectId = objectId;
            this.ElementEventId = elementEventId;
        }

        public GameContextRemoveElementWithEventMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(ElementEventId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ElementEventId = reader.ReadSByte();
        }

    }
}
