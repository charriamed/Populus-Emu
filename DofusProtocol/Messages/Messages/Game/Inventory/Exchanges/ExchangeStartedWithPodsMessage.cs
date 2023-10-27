namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartedWithPodsMessage : ExchangeStartedMessage
    {
        public new const uint Id = 6129;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double FirstCharacterId { get; set; }
        public uint FirstCharacterCurrentWeight { get; set; }
        public uint FirstCharacterMaxWeight { get; set; }
        public double SecondCharacterId { get; set; }
        public uint SecondCharacterCurrentWeight { get; set; }
        public uint SecondCharacterMaxWeight { get; set; }

        public ExchangeStartedWithPodsMessage(sbyte exchangeType, double firstCharacterId, uint firstCharacterCurrentWeight, uint firstCharacterMaxWeight, double secondCharacterId, uint secondCharacterCurrentWeight, uint secondCharacterMaxWeight)
        {
            this.ExchangeType = exchangeType;
            this.FirstCharacterId = firstCharacterId;
            this.FirstCharacterCurrentWeight = firstCharacterCurrentWeight;
            this.FirstCharacterMaxWeight = firstCharacterMaxWeight;
            this.SecondCharacterId = secondCharacterId;
            this.SecondCharacterCurrentWeight = secondCharacterCurrentWeight;
            this.SecondCharacterMaxWeight = secondCharacterMaxWeight;
        }

        public ExchangeStartedWithPodsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(FirstCharacterId);
            writer.WriteVarUInt(FirstCharacterCurrentWeight);
            writer.WriteVarUInt(FirstCharacterMaxWeight);
            writer.WriteDouble(SecondCharacterId);
            writer.WriteVarUInt(SecondCharacterCurrentWeight);
            writer.WriteVarUInt(SecondCharacterMaxWeight);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            FirstCharacterId = reader.ReadDouble();
            FirstCharacterCurrentWeight = reader.ReadVarUInt();
            FirstCharacterMaxWeight = reader.ReadVarUInt();
            SecondCharacterId = reader.ReadDouble();
            SecondCharacterCurrentWeight = reader.ReadVarUInt();
            SecondCharacterMaxWeight = reader.ReadVarUInt();
        }

    }
}
