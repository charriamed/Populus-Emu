namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightRemoveTeamMemberMessage : Message
    {
        public const uint Id = 711;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public sbyte TeamId { get; set; }
        public double CharId { get; set; }

        public GameFightRemoveTeamMemberMessage(ushort fightId, sbyte teamId, double charId)
        {
            this.FightId = fightId;
            this.TeamId = teamId;
            this.CharId = charId;
        }

        public GameFightRemoveTeamMemberMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteSByte(TeamId);
            writer.WriteDouble(CharId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            TeamId = reader.ReadSByte();
            CharId = reader.ReadDouble();
        }

    }
}
