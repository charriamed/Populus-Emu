namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareErrorMessage : Message
    {
        public const uint Id = 6667;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Error { get; set; }

        public DareErrorMessage(sbyte error)
        {
            this.Error = error;
        }

        public DareErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Error);
        }

        public override void Deserialize(IDataReader reader)
        {
            Error = reader.ReadSByte();
        }

    }
}
