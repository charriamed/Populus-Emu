namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ServerStatusUpdateMessage : Message
    {
        public const uint Id = 50;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameServerInformations Server { get; set; }

        public ServerStatusUpdateMessage(GameServerInformations server)
        {
            this.Server = server;
        }

        public ServerStatusUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Server.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Server = new GameServerInformations();
            Server.Deserialize(reader);
        }

    }
}
