namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FighterStatsListMessage : Message
    {
        public const uint Id = 6322;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterCharacteristicsInformations Stats { get; set; }

        public FighterStatsListMessage(CharacterCharacteristicsInformations stats)
        {
            this.Stats = stats;
        }

        public FighterStatsListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Stats.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Stats = new CharacterCharacteristicsInformations();
            Stats.Deserialize(reader);
        }

    }
}
