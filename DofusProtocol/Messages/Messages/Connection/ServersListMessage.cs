namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ServersListMessage : Message
    {
        public const uint Id = 30;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameServerInformations[] Servers { get; set; }
        public ushort AlreadyConnectedToServerId { get; set; }
        public bool CanCreateNewCharacter { get; set; }

        public ServersListMessage(GameServerInformations[] servers, ushort alreadyConnectedToServerId, bool canCreateNewCharacter)
        {
            this.Servers = servers;
            this.AlreadyConnectedToServerId = alreadyConnectedToServerId;
            this.CanCreateNewCharacter = canCreateNewCharacter;
        }

        public ServersListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Servers.Count());
            for (var serversIndex = 0; serversIndex < Servers.Count(); serversIndex++)
            {
                var objectToSend = Servers[serversIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarUShort(AlreadyConnectedToServerId);
            writer.WriteBoolean(CanCreateNewCharacter);
        }

        public override void Deserialize(IDataReader reader)
        {
            var serversCount = reader.ReadUShort();
            Servers = new GameServerInformations[serversCount];
            for (var serversIndex = 0; serversIndex < serversCount; serversIndex++)
            {
                var objectToAdd = new GameServerInformations();
                objectToAdd.Deserialize(reader);
                Servers[serversIndex] = objectToAdd;
            }
            AlreadyConnectedToServerId = reader.ReadVarUShort();
            CanCreateNewCharacter = reader.ReadBoolean();
        }

    }
}
