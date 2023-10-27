namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismUseRequestMessage : Message
    {
        public const uint Id = 6041;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ModuleToUse { get; set; }

        public PrismUseRequestMessage(sbyte moduleToUse)
        {
            this.ModuleToUse = moduleToUse;
        }

        public PrismUseRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ModuleToUse);
        }

        public override void Deserialize(IDataReader reader)
        {
            ModuleToUse = reader.ReadSByte();
        }

    }
}
