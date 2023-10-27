namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntFlagRemoveRequestMessage : Message
    {
        public const uint Id = 6510;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte QuestType { get; set; }
        public sbyte Index { get; set; }

        public TreasureHuntFlagRemoveRequestMessage(sbyte questType, sbyte index)
        {
            this.QuestType = questType;
            this.Index = index;
        }

        public TreasureHuntFlagRemoveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(QuestType);
            writer.WriteSByte(Index);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestType = reader.ReadSByte();
            Index = reader.ReadSByte();
        }

    }
}
