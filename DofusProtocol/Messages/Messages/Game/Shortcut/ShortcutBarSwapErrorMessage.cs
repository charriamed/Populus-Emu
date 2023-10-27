namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutBarSwapErrorMessage : Message
    {
        public const uint Id = 6226;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Error { get; set; }

        public ShortcutBarSwapErrorMessage(sbyte error)
        {
            this.Error = error;
        }

        public ShortcutBarSwapErrorMessage() { }

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
