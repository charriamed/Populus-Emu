namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class QuestStartRequestMessage : Message
    {
        public const uint Id = 5643;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort QuestId { get; set; }

        public QuestStartRequestMessage(ushort questId)
        {
            this.QuestId = questId;
        }

        public QuestStartRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(QuestId);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestId = reader.ReadVarUShort();
        }

    }
}
