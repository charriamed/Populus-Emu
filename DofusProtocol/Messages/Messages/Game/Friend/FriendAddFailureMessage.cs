namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendAddFailureMessage : Message
    {
        public const uint Id = 5600;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Reason { get; set; }

        public FriendAddFailureMessage(sbyte reason)
        {
            this.Reason = reason;
        }

        public FriendAddFailureMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Reason);
        }

        public override void Deserialize(IDataReader reader)
        {
            Reason = reader.ReadSByte();
        }

    }
}
