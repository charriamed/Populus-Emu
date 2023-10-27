namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightDispellableEffectMessage : AbstractGameActionMessage
    {
        public new const uint Id = 6070;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AbstractFightDispellableEffect Effect { get; set; }

        public GameActionFightDispellableEffectMessage(ushort actionId, double sourceId, AbstractFightDispellableEffect effect)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.Effect = effect;
        }

        public GameActionFightDispellableEffectMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(Effect.TypeId);
            Effect.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Effect = ProtocolTypeManager.GetInstance<AbstractFightDispellableEffect>(reader.ReadShort());
            Effect.Deserialize(reader);
        }

    }
}
