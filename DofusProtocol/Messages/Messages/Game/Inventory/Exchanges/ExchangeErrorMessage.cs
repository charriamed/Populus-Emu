namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeErrorMessage : Message
    {
        public const uint Id = 5513;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ErrorType { get; set; }

        public ExchangeErrorMessage(sbyte errorType)
        {
            this.ErrorType = errorType;
        }

        public ExchangeErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ErrorType);
        }

        public override void Deserialize(IDataReader reader)
        {
            ErrorType = reader.ReadSByte();
        }

    }
}
