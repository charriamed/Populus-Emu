namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayFightRequestCanceledMessage : Message
    {
        public const uint Id = 5822;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public double SourceId { get; set; }
        public double TargetId { get; set; }

        public GameRolePlayFightRequestCanceledMessage(ushort fightId, double sourceId, double targetId)
        {
            this.FightId = fightId;
            this.SourceId = sourceId;
            this.TargetId = targetId;
        }

        public GameRolePlayFightRequestCanceledMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteDouble(SourceId);
            writer.WriteDouble(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            SourceId = reader.ReadDouble();
            TargetId = reader.ReadDouble();
        }

    }
}
