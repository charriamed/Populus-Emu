namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightTriggerEffectMessage : GameActionFightDispellEffectMessage
    {
        public new const uint Id = 6147;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GameActionFightTriggerEffectMessage(ushort actionId, double sourceId, double targetId, bool verboseCast, int boostUID)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.VerboseCast = verboseCast;
            this.BoostUID = boostUID;
        }

        public GameActionFightTriggerEffectMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
