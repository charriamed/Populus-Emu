namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AcquaintanceServerListMessage : Message
    {
        public const uint Id = 6142;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] Servers { get; set; }

        public AcquaintanceServerListMessage(ushort[] servers)
        {
            this.Servers = servers;
        }

        public AcquaintanceServerListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Servers.Count());
            for (var serversIndex = 0; serversIndex < Servers.Count(); serversIndex++)
            {
                writer.WriteVarUShort(Servers[serversIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var serversCount = reader.ReadUShort();
            Servers = new ushort[serversCount];
            for (var serversIndex = 0; serversIndex < serversCount; serversIndex++)
            {
                Servers[serversIndex] = reader.ReadVarUShort();
            }
        }

    }
}
