namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MapRunningFightDetailsExtendedMessage : MapRunningFightDetailsMessage
    {
        public new const uint Id = 6500;
        public override uint MessageId
        {
            get { return Id; }
        }
        public NamedPartyTeam[] NamedPartyTeams { get; set; }

        public MapRunningFightDetailsExtendedMessage(ushort fightId, GameFightFighterLightInformations[] attackers, GameFightFighterLightInformations[] defenders, NamedPartyTeam[] namedPartyTeams)
        {
            this.FightId = fightId;
            this.Attackers = attackers;
            this.Defenders = defenders;
            this.NamedPartyTeams = namedPartyTeams;
        }

        public MapRunningFightDetailsExtendedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)NamedPartyTeams.Count());
            for (var namedPartyTeamsIndex = 0; namedPartyTeamsIndex < NamedPartyTeams.Count(); namedPartyTeamsIndex++)
            {
                var objectToSend = NamedPartyTeams[namedPartyTeamsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var namedPartyTeamsCount = reader.ReadUShort();
            NamedPartyTeams = new NamedPartyTeam[namedPartyTeamsCount];
            for (var namedPartyTeamsIndex = 0; namedPartyTeamsIndex < namedPartyTeamsCount; namedPartyTeamsIndex++)
            {
                var objectToAdd = new NamedPartyTeam();
                objectToAdd.Deserialize(reader);
                NamedPartyTeams[namedPartyTeamsIndex] = objectToAdd;
            }
        }

    }
}
