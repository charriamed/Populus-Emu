namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FocusedExchangeReadyMessage : ExchangeReadyMessage
    {
        public new const uint Id = 6701;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint FocusActionId { get; set; }

        public FocusedExchangeReadyMessage(bool ready, ushort step, uint focusActionId)
        {
            this.Ready = ready;
            this.Step = step;
            this.FocusActionId = focusActionId;
        }

        public FocusedExchangeReadyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(FocusActionId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            FocusActionId = reader.ReadVarUInt();
        }

    }
}
