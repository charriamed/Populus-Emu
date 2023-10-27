namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MimicryObjectEraseRequestMessage : Message
    {
        public const uint Id = 6457;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HostUID { get; set; }
        public byte HostPos { get; set; }

        public MimicryObjectEraseRequestMessage(uint hostUID, byte hostPos)
        {
            this.HostUID = hostUID;
            this.HostPos = hostPos;
        }

        public MimicryObjectEraseRequestMessage() { }

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
