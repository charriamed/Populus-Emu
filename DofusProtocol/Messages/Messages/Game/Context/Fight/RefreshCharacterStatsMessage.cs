namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class RefreshCharacterStatsMessage : Message
    {
        public const uint Id = 6699;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double FighterId { get; set; }
        public GameFightMinimalStats Stats { get; set; }

        public RefreshCharacterStatsMessage(double fighterId, GameFightMinimalStats stats)
        {
            this.FighterId = fighterId;
            this.Stats = stats;
        }

        public RefreshCharacterStatsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(FighterId);
            Stats.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            FighterId = reader.ReadDouble();
            Stats = new GameFightMinimalStats();
            Stats.Deserialize(reader);
        }

    }
}
