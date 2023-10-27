namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountHarnessColorsUpdateRequestMessage : Message
    {
        public const uint Id = 6697;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool UseHarnessColors { get; set; }

        public MountHarnessColorsUpdateRequestMessage(bool useHarnessColors)
        {
            this.UseHarnessColors = useHarnessColors;
        }

        public MountHarnessColorsUpdateRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(UseHarnessColors);
        }

        public override void Deserialize(IDataReader reader)
        {
            UseHarnessColors = reader.ReadBoolean();
        }

    }
}
