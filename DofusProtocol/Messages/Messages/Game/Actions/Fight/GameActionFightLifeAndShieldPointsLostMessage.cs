namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightLifeAndShieldPointsLostMessage : GameActionFightLifePointsLostMessage
    {
        public new const uint Id = 6310;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ShieldLoss { get; set; }

        public GameActionFightLifeAndShieldPointsLostMessage(ushort actionId, double sourceId, double targetId, uint loss, uint permanentDamages, int elementId, ushort shieldLoss)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.Loss = loss;
            this.PermanentDamages = permanentDamages;
            this.ElementId = elementId;
            this.ShieldLoss = shieldLoss;
        }

        public GameActionFightLifeAndShieldPointsLostMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(ShieldLoss);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ShieldLoss = reader.ReadVarUShort();
        }

    }
}
