namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightUpdateTeamMessage : Message
    {
        public const uint Id = 5572;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public FightTeamInformations Team { get; set; }

        public GameFightUpdateTeamMessage(ushort fightId, FightTeamInformations team)
        {
            this.FightId = fightId;
            this.Team = team;
        }

        public GameFightUpdateTeamMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            Team.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            Team = new FightTeamInformations();
            Team.Deserialize(reader);
        }

    }
}
