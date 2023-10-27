namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightJoinLeaveRequestMessage : Message
    {
        public const uint Id = 5843;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public bool Join { get; set; }

        public PrismFightJoinLeaveRequestMessage(ushort subAreaId, bool join)
        {
            this.SubAreaId = subAreaId;
            this.Join = join;
        }

        public PrismFightJoinLeaveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteBoolean(Join);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            Join = reader.ReadBoolean();
        }

    }
}
