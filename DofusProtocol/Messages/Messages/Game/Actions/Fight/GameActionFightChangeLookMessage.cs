namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightChangeLookMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5532;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public EntityLook EntityLook { get; set; }

        public GameActionFightChangeLookMessage(ushort actionId, double sourceId, double targetId, EntityLook entityLook)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.EntityLook = entityLook;
        }

        public GameActionFightChangeLookMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            EntityLook.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            EntityLook = new EntityLook();
            EntityLook.Deserialize(reader);
        }

    }
}
