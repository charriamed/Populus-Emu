namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class QuestStepInfoRequestMessage : Message
    {
        public const uint Id = 5622;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort QuestId { get; set; }

        public QuestStepInfoRequestMessage(ushort questId)
        {
            this.QuestId = questId;
        }

        public QuestStepInfoRequestMessage() { }

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
