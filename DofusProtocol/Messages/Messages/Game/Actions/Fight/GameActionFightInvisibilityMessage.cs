namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightInvisibilityMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5821;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public sbyte State { get; set; }

        public GameActionFightInvisibilityMessage(ushort actionId, double sourceId, double targetId, sbyte state)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.State = state;
        }

        public GameActionFightInvisibilityMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteSByte(State);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            State = reader.ReadSByte();
        }

    }
}
