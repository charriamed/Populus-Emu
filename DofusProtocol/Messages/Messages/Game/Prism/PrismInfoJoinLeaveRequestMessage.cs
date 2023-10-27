namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismInfoJoinLeaveRequestMessage : Message
    {
        public const uint Id = 5844;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Join { get; set; }

        public PrismInfoJoinLeaveRequestMessage(bool join)
        {
            this.Join = join;
        }

        public PrismInfoJoinLeaveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Join);
        }

        public override void Deserialize(IDataReader reader)
        {
            Join = reader.ReadBoolean();
        }

    }
}
