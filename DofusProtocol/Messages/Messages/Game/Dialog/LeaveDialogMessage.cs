namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LeaveDialogMessage : Message
    {
        public const uint Id = 5502;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte DialogType { get; set; }

        public LeaveDialogMessage(sbyte dialogType)
        {
            this.DialogType = dialogType;
        }

        public LeaveDialogMessage() { }

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
