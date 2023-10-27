namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntFinishedMessage : Message
    {
        public const uint Id = 6483;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte QuestType { get; set; }

        public TreasureHuntFinishedMessage(sbyte questType)
        {
            this.QuestType = questType;
        }

        public TreasureHuntFinishedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(QuestType);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestType = reader.ReadSByte();
        }

    }
}
