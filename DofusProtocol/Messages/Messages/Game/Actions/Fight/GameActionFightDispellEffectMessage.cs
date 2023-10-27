namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightDispellEffectMessage : GameActionFightDispellMessage
    {
        public new const uint Id = 6113;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int BoostUID { get; set; }

        public GameActionFightDispellEffectMessage(ushort actionId, double sourceId, double targetId, bool verboseCast, int boostUID)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.VerboseCast = verboseCast;
            this.BoostUID = boostUID;
        }

        public GameActionFightDispellEffectMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(BoostUID);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            BoostUID = reader.ReadInt();
        }

    }
}
