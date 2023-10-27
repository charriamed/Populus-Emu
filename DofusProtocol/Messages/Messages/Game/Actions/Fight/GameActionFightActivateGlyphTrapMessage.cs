namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightActivateGlyphTrapMessage : AbstractGameActionMessage
    {
        public new const uint Id = 6545;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short MarkId { get; set; }
        public bool Active { get; set; }

        public GameActionFightActivateGlyphTrapMessage(ushort actionId, double sourceId, short markId, bool active)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.MarkId = markId;
            this.Active = active;
        }

        public GameActionFightActivateGlyphTrapMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(MarkId);
            writer.WriteBoolean(Active);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MarkId = reader.ReadShort();
            Active = reader.ReadBoolean();
        }

    }
}
