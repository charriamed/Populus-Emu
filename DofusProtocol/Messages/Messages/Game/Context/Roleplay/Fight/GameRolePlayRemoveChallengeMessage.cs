namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayRemoveChallengeMessage : Message
    {
        public const uint Id = 300;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }

        public GameRolePlayRemoveChallengeMessage(ushort fightId)
        {
            this.FightId = fightId;
        }

        public GameRolePlayRemoveChallengeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
        }

    }
}
