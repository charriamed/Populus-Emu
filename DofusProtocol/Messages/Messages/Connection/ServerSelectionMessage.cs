namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ServerSelectionMessage : Message
    {
        public const uint Id = 40;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ServerId { get; set; }

        public ServerSelectionMessage(ushort serverId)
        {
            this.ServerId = serverId;
        }

        public ServerSelectionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ServerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ServerId = reader.ReadVarUShort();
        }

    }
}
