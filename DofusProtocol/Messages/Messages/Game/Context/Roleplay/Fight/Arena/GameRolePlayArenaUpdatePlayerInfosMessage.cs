namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaUpdatePlayerInfosMessage : Message
    {
        public const uint Id = 6301;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ArenaRankInfos Solo { get; set; }

        public GameRolePlayArenaUpdatePlayerInfosMessage(ArenaRankInfos solo)
        {
            this.Solo = solo;
        }

        public GameRolePlayArenaUpdatePlayerInfosMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Solo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Solo = new ArenaRankInfos();
            Solo.Deserialize(reader);
        }

    }
}
