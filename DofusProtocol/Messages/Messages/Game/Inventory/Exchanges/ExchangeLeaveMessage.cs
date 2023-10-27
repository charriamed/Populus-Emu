namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeLeaveMessage : LeaveDialogMessage
    {
        public new const uint Id = 5628;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Success { get; set; }

        public ExchangeLeaveMessage(sbyte dialogType, bool success)
        {
            this.DialogType = dialogType;
            this.Success = success;
        }

        public ExchangeLeaveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Success);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Success = reader.ReadBoolean();
        }

    }
}
