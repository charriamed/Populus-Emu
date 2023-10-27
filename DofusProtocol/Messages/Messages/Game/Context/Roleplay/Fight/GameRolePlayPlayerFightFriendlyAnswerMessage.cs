namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayPlayerFightFriendlyAnswerMessage : Message
    {
        public const uint Id = 5732;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public bool Accept { get; set; }

        public GameRolePlayPlayerFightFriendlyAnswerMessage(ushort fightId, bool accept)
        {
            this.FightId = fightId;
            this.Accept = accept;
        }

        public GameRolePlayPlayerFightFriendlyAnswerMessage() { }

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
