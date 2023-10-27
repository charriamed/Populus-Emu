namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaSwitchToFightServerMessage : Message
    {
        public const uint Id = 6575;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Address { get; set; }
        public ushort[] Ports { get; set; }
        public sbyte[] Ticket { get; set; }

        public GameRolePlayArenaSwitchToFightServerMessage(string address, ushort[] ports, sbyte[] ticket)
        {
            this.Address = address;
            this.Ports = ports;
            this.Ticket = ticket;
        }

        public GameRolePlayArenaSwitchToFightServerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Address);
            writer.WriteShort((short)Ports.Count());
            for (var portsIndex = 0; portsIndex < Ports.Count(); portsIndex++)
            {
                writer.WriteUShort(Ports[portsIndex]);
            }
            writer.WriteVarInt(Ticket.Count());
            for (var ticketIndex = 0; ticketIndex < Ticket.Count(); ticketIndex++)
            {
                writer.WriteSByte(Ticket[ticketIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            Address = reader.ReadUTF();
            var portsCount = reader.ReadUShort();
            Ports = new ushort[portsCount];
            for (var portsIndex = 0; portsIndex < portsCount; portsIndex++)
            {
                Ports[portsIndex] = reader.ReadUShort();
            }
            var ticketCount = reader.ReadVarInt();
            Ticket = new sbyte[ticketCount];
            for (var ticketIndex = 0; ticketIndex < ticketCount; ticketIndex++)
            {
                Ticket[ticketIndex] = reader.ReadSByte();
            }
        }

    }
}
