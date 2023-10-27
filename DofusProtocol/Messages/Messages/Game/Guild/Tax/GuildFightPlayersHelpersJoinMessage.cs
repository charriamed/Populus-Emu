namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFightPlayersHelpersJoinMessage : Message
    {
        public const uint Id = 5720;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double FightId { get; set; }
        public CharacterMinimalPlusLookInformations PlayerInfo { get; set; }

        public GuildFightPlayersHelpersJoinMessage(double fightId, CharacterMinimalPlusLookInformations playerInfo)
        {
            this.FightId = fightId;
            this.PlayerInfo = playerInfo;
        }

        public GuildFightPlayersHelpersJoinMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(FightId);
            PlayerInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadDouble();
            PlayerInfo = new CharacterMinimalPlusLookInformations();
            PlayerInfo.Deserialize(reader);
        }

    }
}
