namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightDeathMessage : AbstractGameActionMessage
    {
        public new const uint Id = 1099;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }

        public GameActionFightDeathMessage(ushort actionId, double sourceId, double targetId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
        }

        public GameActionFightDeathMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
        }

    }
}
