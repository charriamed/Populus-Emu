namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicAckMessage : Message
    {
        public const uint Id = 6362;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Seq { get; set; }
        public ushort LastPacketId { get; set; }

        public BasicAckMessage(uint seq, ushort lastPacketId)
        {
            this.Seq = seq;
            this.LastPacketId = lastPacketId;
        }

        public BasicAckMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(Seq);
            writer.WriteVarUShort(LastPacketId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Seq = reader.ReadVarUInt();
            LastPacketId = reader.ReadVarUShort();
        }

    }
}
