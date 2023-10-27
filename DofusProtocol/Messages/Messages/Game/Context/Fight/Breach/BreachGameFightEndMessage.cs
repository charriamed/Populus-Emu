namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BreachGameFightEndMessage : GameFightEndMessage
    {
        public new const uint Id = 6809;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int Budget { get; set; }

        public BreachGameFightEndMessage(int duration, short rewardRate, short lootShareLimitMalus, FightResultListEntry[] results, NamedPartyTeamWithOutcome[] namedPartyTeamsOutcomes, int budget)
        {
            this.Duration = duration;
            this.RewardRate = rewardRate;
            this.LootShareLimitMalus = lootShareLimitMalus;
            this.Results = results;
            this.NamedPartyTeamsOutcomes = namedPartyTeamsOutcomes;
            this.Budget = budget;
        }

        public BreachGameFightEndMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(Budget);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Budget = reader.ReadInt();
        }

    }
}
