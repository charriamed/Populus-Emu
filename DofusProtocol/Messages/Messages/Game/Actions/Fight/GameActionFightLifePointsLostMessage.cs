namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightLifePointsLostMessage : AbstractGameActionMessage
    {
        public new const uint Id = 6312;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public uint Loss { get; set; }
        public uint PermanentDamages { get; set; }
        public int ElementId { get; set; }

        public GameActionFightLifePointsLostMessage(ushort actionId, double sourceId, double targetId, uint loss, uint permanentDamages, int elementId)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.Loss = loss;
            this.PermanentDamages = permanentDamages;
            this.ElementId = elementId;
        }

        public GameActionFightLifePointsLostMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteVarUInt(Loss);
            writer.WriteVarUInt(PermanentDamages);
            writer.WriteVarInt(ElementId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            Loss = reader.ReadVarUInt();
            PermanentDamages = reader.ReadVarUInt();
            ElementId = reader.ReadVarInt();
        }

    }
}
