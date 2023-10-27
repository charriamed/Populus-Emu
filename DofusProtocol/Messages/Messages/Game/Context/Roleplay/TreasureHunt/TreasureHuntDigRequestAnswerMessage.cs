namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntDigRequestAnswerMessage : Message
    {
        public const uint Id = 6484;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte QuestType { get; set; }
        public sbyte Result { get; set; }

        public TreasureHuntDigRequestAnswerMessage(sbyte questType, sbyte result)
        {
            this.QuestType = questType;
            this.Result = result;
        }

        public TreasureHuntDigRequestAnswerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(QuestType);
            writer.WriteSByte(Result);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestType = reader.ReadSByte();
            Result = reader.ReadSByte();
        }

    }
}
