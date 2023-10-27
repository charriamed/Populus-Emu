namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayPlayerFightFriendlyRequestedMessage : Message
    {
        public const uint Id = 5937;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public ulong SourceId { get; set; }
        public ulong TargetId { get; set; }

        public GameRolePlayPlayerFightFriendlyRequestedMessage(ushort fightId, ulong sourceId, ulong targetId)
        {
            this.FightId = fightId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
        }

        public GameRolePlayPlayerFightFriendlyRequestedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteVarULong(SourceId);
            writer.WriteVarULong(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            SourceId = reader.ReadVarULong();
            TargetId = reader.ReadVarULong();
        }

    }
}
