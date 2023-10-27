namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AccessoryPreviewErrorMessage : Message
    {
        public const uint Id = 6521;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Error { get; set; }

        public AccessoryPreviewErrorMessage(sbyte error)
        {
            this.Error = error;
        }

        public AccessoryPreviewErrorMessage() { }

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
