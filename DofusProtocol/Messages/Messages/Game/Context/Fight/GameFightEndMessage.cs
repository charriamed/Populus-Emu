namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightEndMessage : Message
    {
        public const uint Id = 720;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int Duration { get; set; }
        public short RewardRate { get; set; }
        public short LootShareLimitMalus { get; set; }
        public FightResultListEntry[] Results { get; set; }
        public NamedPartyTeamWithOutcome[] NamedPartyTeamsOutcomes { get; set; }

        public GameFightEndMessage(int duration, short rewardRate, short lootShareLimitMalus, FightResultListEntry[] results, NamedPartyTeamWithOutcome[] namedPartyTeamsOutcomes)
        {
            this.Duration = duration;
            this.RewardRate = rewardRate;
            this.LootShareLimitMalus = lootShareLimitMalus;
            this.Results = results;
            this.NamedPartyTeamsOutcomes = namedPartyTeamsOutcomes;
        }

        public GameFightEndMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(Duration);
            writer.WriteVarShort(RewardRate);
            writer.WriteShort(LootShareLimitMalus);
            writer.WriteShort((short)Results.Count());
            for (var resultsIndex = 0; resultsIndex < Results.Count(); resultsIndex++)
            {
                var objectToSend = Results[resultsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)NamedPartyTeamsOutcomes.Count());
            for (var namedPartyTeamsOutcomesIndex = 0; namedPartyTeamsOutcomesIndex < NamedPartyTeamsOutcomes.Count(); namedPartyTeamsOutcomesIndex++)
            {
                var objectToSend = NamedPartyTeamsOutcomes[namedPartyTeamsOutcomesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            Duration = reader.ReadInt();
            RewardRate = reader.ReadVarShort();
            LootShareLimitMalus = reader.ReadShort();
            var resultsCount = reader.ReadUShort();
            Results = new FightResultListEntry[resultsCount];
            for (var resultsIndex = 0; resultsIndex < resultsCount; resultsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<FightResultListEntry>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Results[resultsIndex] = objectToAdd;
            }
            var namedPartyTeamsOutcomesCount = reader.ReadUShort();
            NamedPartyTeamsOutcomes = new NamedPartyTeamWithOutcome[namedPartyTeamsOutcomesCount];
            for (var namedPartyTeamsOutcomesIndex = 0; namedPartyTeamsOutcomesIndex < namedPartyTeamsOutcomesCount; namedPartyTeamsOutcomesIndex++)
            {
                var objectToAdd = new NamedPartyTeamWithOutcome();
                objectToAdd.Deserialize(reader);
                NamedPartyTeamsOutcomes[namedPartyTeamsOutcomesIndex] = objectToAdd;
            }
        }

    }
}
