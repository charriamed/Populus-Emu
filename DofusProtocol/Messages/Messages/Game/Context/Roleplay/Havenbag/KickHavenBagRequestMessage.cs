namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class KickHavenBagRequestMessage : Message
    {
        public const uint Id = 6652;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong GuestId { get; set; }

        public KickHavenBagRequestMessage(ulong guestId)
        {
            this.GuestId = guestId;
        }

        public KickHavenBagRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(GuestId);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuestId = reader.ReadVarULong();
        }

    }
}
