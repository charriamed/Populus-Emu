namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractGameActionMessage : Message
    {
        public const uint Id = 1000;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ActionId { get; set; }
        public double SourceId { get; set; }

        public AbstractGameActionMessage(ushort actionId, double sourceId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
        }

        public AbstractGameActionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ActionId);
            writer.WriteDouble(SourceId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ActionId = reader.ReadVarUShort();
            SourceId = reader.ReadDouble();
        }

    }
}
