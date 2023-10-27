namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChangeHavenBagRoomRequestMessage : Message
    {
        public const uint Id = 6638;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte RoomId { get; set; }

        public ChangeHavenBagRoomRequestMessage(sbyte roomId)
        {
            this.RoomId = roomId;
        }

        public ChangeHavenBagRoomRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(RoomId);
        }

        public override void Deserialize(IDataReader reader)
        {
            RoomId = reader.ReadSByte();
        }

    }
}
