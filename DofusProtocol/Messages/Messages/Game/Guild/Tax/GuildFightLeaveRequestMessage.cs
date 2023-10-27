namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFightLeaveRequestMessage : Message
    {
        public const uint Id = 5715;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TaxCollectorId { get; set; }
        public ulong CharacterId { get; set; }

        public GuildFightLeaveRequestMessage(double taxCollectorId, ulong characterId)
        {
            this.TaxCollectorId = taxCollectorId;
            this.CharacterId = characterId;
        }

        public GuildFightLeaveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(TaxCollectorId);
            writer.WriteVarULong(CharacterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            TaxCollectorId = reader.ReadDouble();
            CharacterId = reader.ReadVarULong();
        }

    }
}
