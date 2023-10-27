namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntAvailableRetryCountUpdateMessage : Message
    {
        public const uint Id = 6491;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte QuestType { get; set; }
        public int AvailableRetryCount { get; set; }

        public TreasureHuntAvailableRetryCountUpdateMessage(sbyte questType, int availableRetryCount)
        {
            this.QuestType = questType;
            this.AvailableRetryCount = availableRetryCount;
        }

        public TreasureHuntAvailableRetryCountUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(QuestType);
            writer.WriteInt(AvailableRetryCount);
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestType = reader.ReadSByte();
            AvailableRetryCount = reader.ReadInt();
        }

    }
}
