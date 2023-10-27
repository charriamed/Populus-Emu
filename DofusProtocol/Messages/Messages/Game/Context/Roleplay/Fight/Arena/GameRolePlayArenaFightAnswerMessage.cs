namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaFightAnswerMessage : Message
    {
        public const uint Id = 6279;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public bool Accept { get; set; }

        public GameRolePlayArenaFightAnswerMessage(ushort fightId, bool accept)
        {
            this.FightId = fightId;
            this.Accept = accept;
        }

        public GameRolePlayArenaFightAnswerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteBoolean(Accept);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            Accept = reader.ReadBoolean();
        }

    }
}
