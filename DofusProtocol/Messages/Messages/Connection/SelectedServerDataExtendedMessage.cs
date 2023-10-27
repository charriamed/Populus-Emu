namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class SelectedServerDataExtendedMessage : SelectedServerDataMessage
    {
        public new const uint Id = 6469;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameServerInformations[] Servers { get; set; }

        public SelectedServerDataExtendedMessage(ushort serverId, string address, int[] ports, bool canCreateNewCharacter, sbyte[] ticket, GameServerInformations[] servers)
        {
            this.ServerId = serverId;
            this.Address = address;
            this.Ports = ports;
            this.CanCreateNewCharacter = canCreateNewCharacter;
            this.Ticket = ticket;
            this.Servers = servers;
        }

        public SelectedServerDataExtendedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Servers.Count());
            for (var serversIndex = 0; serversIndex < Servers.Count(); serversIndex++)
            {
                var objectToSend = Servers[serversIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var serversCount = reader.ReadUShort();
            Servers = new GameServerInformations[serversCount];
            for (var serversIndex = 0; serversIndex < serversCount; serversIndex++)
            {
                var objectToAdd = new GameServerInformations();
                objectToAdd.Deserialize(reader);
                Servers[serversIndex] = objectToAdd;
            }
        }

    }
}
