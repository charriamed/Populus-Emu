namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SelectedServerRefusedMessage : Message
    {
        public const uint Id = 41;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ServerId { get; set; }
        public sbyte Error { get; set; }
        public sbyte ServerStatus { get; set; }

        public SelectedServerRefusedMessage(ushort serverId, sbyte error, sbyte serverStatus)
        {
            this.ServerId = serverId;
            this.Error = error;
            this.ServerStatus = serverStatus;
        }

        public SelectedServerRefusedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ServerId);
            writer.WriteSByte(Error);
            writer.WriteSByte(ServerStatus);
        }

        public override void Deserialize(IDataReader reader)
        {
            ServerId = reader.ReadVarUShort();
            Error = reader.ReadSByte();
            ServerStatus = reader.ReadSByte();
        }

    }
}
