namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismsListRegisterMessage : Message
    {
        public const uint Id = 6441;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Listen { get; set; }

        public PrismsListRegisterMessage(sbyte listen)
        {
            this.Listen = listen;
        }

        public PrismsListRegisterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Listen);
        }

        public override void Deserialize(IDataReader reader)
        {
            Listen = reader.ReadSByte();
        }

    }
}
