namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SymbioticObjectAssociateRequestMessage : Message
    {
        public const uint Id = 6522;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint SymbioteUID { get; set; }
        public byte SymbiotePos { get; set; }
        public uint HostUID { get; set; }
        public byte HostPos { get; set; }

        public SymbioticObjectAssociateRequestMessage(uint symbioteUID, byte symbiotePos, uint hostUID, byte hostPos)
        {
            this.SymbioteUID = symbioteUID;
            this.SymbiotePos = symbiotePos;
            this.HostUID = hostUID;
            this.HostPos = hostPos;
        }

        public SymbioticObjectAssociateRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(SymbioteUID);
            writer.WriteByte(SymbiotePos);
            writer.WriteVarUInt(HostUID);
            writer.WriteByte(HostPos);
        }

        public override void Deserialize(IDataReader reader)
        {
            SymbioteUID = reader.ReadVarUInt();
            SymbiotePos = reader.ReadByte();
            HostUID = reader.ReadVarUInt();
            HostPos = reader.ReadByte();
        }

    }
}
