namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachInvitationRequestMessage : Message
    {
        public const uint Id = 6794;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Guest { get; set; }

        public BreachInvitationRequestMessage(ulong guest)
        {
            this.Guest = guest;
        }

        public BreachInvitationRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(Guest);
        }

        public override void Deserialize(IDataReader reader)
        {
            Guest = reader.ReadVarULong();
        }

    }
}
