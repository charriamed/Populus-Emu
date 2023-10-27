namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class RoomAvailableUpdateMessage : Message
    {
        public const uint Id = 6630;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte NbRoom { get; set; }

        public RoomAvailableUpdateMessage(byte nbRoom)
        {
            this.NbRoom = nbRoom;
        }

        public RoomAvailableUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(NbRoom);
        }

        public override void Deserialize(IDataReader reader)
        {
            NbRoom = reader.ReadByte();
        }

    }
}
