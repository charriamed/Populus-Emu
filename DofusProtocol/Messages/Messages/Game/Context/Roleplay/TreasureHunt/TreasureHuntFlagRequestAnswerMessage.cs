namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntFlagRequestAnswerMessage : Message
    {
        public const uint Id = 6507;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte QuestType { get; set; }
        public sbyte Result { get; set; }
        public sbyte Index { get; set; }

        public TreasureHuntFlagRequestAnswerMessage(sbyte questType, sbyte result, sbyte index)
        {
            this.QuestType = questType;
            this.Result = result;
            this.Index = index;
        }

        public TreasureHuntFlagRequestAnswerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(QuestType);
            writer.WriteSByte(Result);
            writer.WriteSByte(Index);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestType = reader.ReadSByte();
            Result = reader.ReadSByte();
            Index = reader.ReadSByte();
        }

    }
}
