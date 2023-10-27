namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightLifePointsGainMessage : AbstractGameActionMessage
    {
        public new const uint Id = 6311;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public uint Delta { get; set; }

        public GameActionFightLifePointsGainMessage(ushort actionId, double sourceId, double targetId, uint delta)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.Delta = delta;
        }

        public GameActionFightLifePointsGainMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteVarUInt(Delta);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            Delta = reader.ReadVarUInt();
        }

    }
}
