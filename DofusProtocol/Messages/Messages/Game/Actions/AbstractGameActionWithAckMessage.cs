namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractGameActionWithAckMessage : AbstractGameActionMessage
    {
        public new const uint Id = 1001;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short WaitAckId { get; set; }

        public AbstractGameActionWithAckMessage(ushort actionId, double sourceId, short waitAckId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.WaitAckId = waitAckId;
        }

        public AbstractGameActionWithAckMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(WaitAckId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            WaitAckId = reader.ReadShort();
        }

    }
}
