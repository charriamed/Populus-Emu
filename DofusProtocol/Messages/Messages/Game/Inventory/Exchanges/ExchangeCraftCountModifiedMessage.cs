namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeCraftCountModifiedMessage : Message
    {
        public const uint Id = 6595;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int Count { get; set; }

        public ExchangeCraftCountModifiedMessage(int count)
        {
            this.Count = count;
        }

        public ExchangeCraftCountModifiedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(Count);
        }

        public override void Deserialize(IDataReader reader)
        {
            Count = reader.ReadVarInt();
        }

    }
}
