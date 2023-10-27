namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PlayerStatusUpdateRequestMessage : Message
    {
        public const uint Id = 6387;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PlayerStatus Status { get; set; }

        public PlayerStatusUpdateRequestMessage(PlayerStatus status)
        {
            this.Status = status;
        }

        public PlayerStatusUpdateRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Status.TypeId);
            Status.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            Status.Deserialize(reader);
        }

    }
}
