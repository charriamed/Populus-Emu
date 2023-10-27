namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayPlayerFightFriendlyAnsweredMessage : Message
    {
        public const uint Id = 5733;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public ulong SourceId { get; set; }
        public ulong TargetId { get; set; }
        public bool Accept { get; set; }

        public GameRolePlayPlayerFightFriendlyAnsweredMessage(ushort fightId, ulong sourceId, ulong targetId, bool accept)
        {
            this.FightId = fightId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.Accept = accept;
        }

        public GameRolePlayPlayerFightFriendlyAnsweredMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteVarULong(SourceId);
            writer.WriteVarULong(TargetId);
            writer.WriteBoolean(Accept);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            SourceId = reader.ReadVarULong();
            TargetId = reader.ReadVarULong();
            Accept = reader.ReadBoolean();
        }

    }
}
