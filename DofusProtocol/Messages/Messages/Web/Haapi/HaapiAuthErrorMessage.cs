namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HaapiAuthErrorMessage : Message
    {
        public const uint Id = 6768;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Type { get; set; }

        public HaapiAuthErrorMessage(sbyte type)
        {
            this.Type = type;
        }

        public HaapiAuthErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Type);
        }

        public override void Deserialize(IDataReader reader)
        {
            Type = reader.ReadSByte();
        }

    }
}
