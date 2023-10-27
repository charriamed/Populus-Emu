namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeOkMultiCraftMessage : Message
    {
        public const uint Id = 5768;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong InitiatorId { get; set; }
        public ulong OtherId { get; set; }
        public sbyte Role { get; set; }

        public ExchangeOkMultiCraftMessage(ulong initiatorId, ulong otherId, sbyte role)
        {
            this.InitiatorId = initiatorId;
            this.OtherId = otherId;
            this.Role = role;
        }

        public ExchangeOkMultiCraftMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(InitiatorId);
            writer.WriteVarULong(OtherId);
            writer.WriteSByte(Role);
        }

        public override void Deserialize(IDataReader reader)
        {
            InitiatorId = reader.ReadVarULong();
            OtherId = reader.ReadVarULong();
            Role = reader.ReadSByte();
        }

    }
}
