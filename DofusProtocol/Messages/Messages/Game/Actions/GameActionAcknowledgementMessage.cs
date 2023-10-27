namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionAcknowledgementMessage : Message
    {
        public const uint Id = 957;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Valid { get; set; }
        public sbyte ActionId { get; set; }

        public GameActionAcknowledgementMessage(bool valid, sbyte actionId)
        {
            this.Valid = valid;
            this.ActionId = actionId;
        }

        public GameActionAcknowledgementMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Valid);
            writer.WriteSByte(ActionId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Valid = reader.ReadBoolean();
            ActionId = reader.ReadSByte();
        }

    }
}
