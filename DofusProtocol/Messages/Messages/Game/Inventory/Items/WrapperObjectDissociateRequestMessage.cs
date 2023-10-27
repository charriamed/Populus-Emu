namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class WrapperObjectDissociateRequestMessage : Message
    {
        public const uint Id = 6524;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HostUID { get; set; }
        public byte HostPos { get; set; }

        public WrapperObjectDissociateRequestMessage(uint hostUID, byte hostPos)
        {
            this.HostUID = hostUID;
            this.HostPos = hostPos;
        }

        public WrapperObjectDissociateRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HostUID);
            writer.WriteByte(HostPos);
        }

        public override void Deserialize(IDataReader reader)
        {
            HostUID = reader.ReadVarUInt();
            HostPos = reader.ReadByte();
        }

    }
}
