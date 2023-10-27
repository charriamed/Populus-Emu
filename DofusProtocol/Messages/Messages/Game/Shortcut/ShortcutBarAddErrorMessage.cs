namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutBarAddErrorMessage : Message
    {
        public const uint Id = 6227;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Error { get; set; }

        public ShortcutBarAddErrorMessage(sbyte error)
        {
            this.Error = error;
        }

        public ShortcutBarAddErrorMessage() { }

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
