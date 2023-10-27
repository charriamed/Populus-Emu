namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PauseDialogMessage : Message
    {
        public const uint Id = 6012;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte DialogType { get; set; }

        public PauseDialogMessage(sbyte dialogType)
        {
            this.DialogType = dialogType;
        }

        public PauseDialogMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(DialogType);
        }

        public override void Deserialize(IDataReader reader)
        {
            DialogType = reader.ReadSByte();
        }

    }
}
