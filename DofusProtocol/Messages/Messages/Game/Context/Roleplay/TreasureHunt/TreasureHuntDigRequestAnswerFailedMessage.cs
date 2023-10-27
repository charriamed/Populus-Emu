namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntDigRequestAnswerFailedMessage : TreasureHuntDigRequestAnswerMessage
    {
        public new const uint Id = 6509;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte WrongFlagCount { get; set; }

        public TreasureHuntDigRequestAnswerFailedMessage(sbyte questType, sbyte result, sbyte wrongFlagCount)
        {
            this.QuestType = questType;
            this.Result = result;
            this.WrongFlagCount = wrongFlagCount;
        }

        public TreasureHuntDigRequestAnswerFailedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(WrongFlagCount);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            WrongFlagCount = reader.ReadSByte();
        }

    }
}
