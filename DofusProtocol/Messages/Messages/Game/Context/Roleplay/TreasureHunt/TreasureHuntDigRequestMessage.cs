namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntDigRequestMessage : Message
    {
        public const uint Id = 6485;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte QuestType { get; set; }

        public TreasureHuntDigRequestMessage(sbyte questType)
        {
            this.QuestType = questType;
        }

        public TreasureHuntDigRequestMessage() { }

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
