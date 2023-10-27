namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutBarSwapRequestMessage : Message
    {
        public const uint Id = 6230;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte BarType { get; set; }
        public sbyte FirstSlot { get; set; }
        public sbyte SecondSlot { get; set; }

        public ShortcutBarSwapRequestMessage(sbyte barType, sbyte firstSlot, sbyte secondSlot)
        {
            this.BarType = barType;
            this.FirstSlot = firstSlot;
            this.SecondSlot = secondSlot;
        }

        public ShortcutBarSwapRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(BarType);
            writer.WriteSByte(FirstSlot);
            writer.WriteSByte(SecondSlot);
        }

        public override void Deserialize(IDataReader reader)
        {
            BarType = reader.ReadSByte();
            FirstSlot = reader.ReadSByte();
            SecondSlot = reader.ReadSByte();
        }

    }
}
