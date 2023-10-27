namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFightJoinRequestMessage : Message
    {
        public const uint Id = 5717;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TaxCollectorId { get; set; }

        public GuildFightJoinRequestMessage(double taxCollectorId)
        {
            this.TaxCollectorId = taxCollectorId;
        }

        public GuildFightJoinRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(TaxCollectorId);
        }

        public override void Deserialize(IDataReader reader)
        {
            TaxCollectorId = reader.ReadDouble();
        }

    }
}
