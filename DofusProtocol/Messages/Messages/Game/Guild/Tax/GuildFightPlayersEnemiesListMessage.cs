namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFightPlayersEnemiesListMessage : Message
    {
        public const uint Id = 5928;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double FightId { get; set; }
        public CharacterMinimalPlusLookInformations[] PlayerInfo { get; set; }

        public GuildFightPlayersEnemiesListMessage(double fightId, CharacterMinimalPlusLookInformations[] playerInfo)
        {
            this.FightId = fightId;
            this.PlayerInfo = playerInfo;
        }

        public GuildFightPlayersEnemiesListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(FightId);
            writer.WriteShort((short)PlayerInfo.Count());
            for (var playerInfoIndex = 0; playerInfoIndex < PlayerInfo.Count(); playerInfoIndex++)
            {
                var objectToSend = PlayerInfo[playerInfoIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadDouble();
            var playerInfoCount = reader.ReadUShort();
            PlayerInfo = new CharacterMinimalPlusLookInformations[playerInfoCount];
            for (var playerInfoIndex = 0; playerInfoIndex < playerInfoCount; playerInfoIndex++)
            {
                var objectToAdd = new CharacterMinimalPlusLookInformations();
                objectToAdd.Deserialize(reader);
                PlayerInfo[playerInfoIndex] = objectToAdd;
            }
        }

    }
}
