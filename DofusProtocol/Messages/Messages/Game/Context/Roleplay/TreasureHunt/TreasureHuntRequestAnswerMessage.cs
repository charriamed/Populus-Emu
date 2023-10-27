namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntRequestAnswerMessage : Message
    {
        public const uint Id = 6489;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte QuestType { get; set; }
        public sbyte Result { get; set; }

        public TreasureHuntRequestAnswerMessage(sbyte questType, sbyte result)
        {
            this.QuestType = questType;
            this.Result = result;
        }

        public TreasureHuntRequestAnswerMessage() { }

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
