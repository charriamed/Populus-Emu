namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightDispellMessage : AbstractGameActionMessage
    {
        public new const uint Id = 5533;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public bool VerboseCast { get; set; }

        public GameActionFightDispellMessage(ushort actionId, double sourceId, double targetId, bool verboseCast)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.VerboseCast = verboseCast;
        }

        public GameActionFightDispellMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(TargetId);
            writer.WriteBoolean(VerboseCast);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TargetId = reader.ReadDouble();
            VerboseCast = reader.ReadBoolean();
        }

    }
}
