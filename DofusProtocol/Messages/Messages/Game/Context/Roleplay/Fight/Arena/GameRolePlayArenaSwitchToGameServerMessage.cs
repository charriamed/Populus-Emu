namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaSwitchToGameServerMessage : Message
    {
        public const uint Id = 6574;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool ValidToken { get; set; }
        public sbyte[] Ticket { get; set; }
        public short HomeServerId { get; set; }

        public GameRolePlayArenaSwitchToGameServerMessage(bool validToken, sbyte[] ticket, short homeServerId)
        {
            this.ValidToken = validToken;
            this.Ticket = ticket;
            this.HomeServerId = homeServerId;
        }

        public GameRolePlayArenaSwitchToGameServerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(ValidToken);
            writer.WriteVarInt(Ticket.Count());
            for (var ticketIndex = 0; ticketIndex < Ticket.Count(); ticketIndex++)
            {
                writer.WriteSByte(Ticket[ticketIndex]);
            }
            writer.WriteShort(HomeServerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ValidToken = reader.ReadBoolean();
            var ticketCount = reader.ReadVarInt();
            Ticket = new sbyte[ticketCount];
            for (var ticketIndex = 0; ticketIndex < ticketCount; ticketIndex++)
            {
                Ticket[ticketIndex] = reader.ReadSByte();
            }
            HomeServerId = reader.ReadShort();
        }

    }
}
