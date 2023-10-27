namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayPlayerFightRequestMessage : Message
    {
        public const uint Id = 5731;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong TargetId { get; set; }
        public short TargetCellId { get; set; }
        public bool Friendly { get; set; }

        public GameRolePlayPlayerFightRequestMessage(ulong targetId, short targetCellId, bool friendly)
        {
            this.TargetId = targetId;
            this.TargetCellId = targetCellId;
            this.Friendly = friendly;
        }

        public GameRolePlayPlayerFightRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(TargetId);
            writer.WriteShort(TargetCellId);
            writer.WriteBoolean(Friendly);
        }

        public override void Deserialize(IDataReader reader)
        {
            TargetId = reader.ReadVarULong();
            TargetCellId = reader.ReadShort();
            Friendly = reader.ReadBoolean();
        }

    }
}
