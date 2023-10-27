namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFightTakePlaceRequestMessage : GuildFightJoinRequestMessage
    {
        public new const uint Id = 6235;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int ReplacedCharacterId { get; set; }

        public GuildFightTakePlaceRequestMessage(double taxCollectorId, int replacedCharacterId)
        {
            this.TaxCollectorId = taxCollectorId;
            this.ReplacedCharacterId = replacedCharacterId;
        }

        public GuildFightTakePlaceRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(ReplacedCharacterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ReplacedCharacterId = reader.ReadInt();
        }

    }
}
