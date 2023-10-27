namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightCastOnTargetRequestMessage : Message
    {
        public const uint Id = 6330;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SpellId { get; set; }
        public double TargetId { get; set; }

        public GameActionFightCastOnTargetRequestMessage(ushort spellId, double targetId)
        {
            this.SpellId = spellId;
            this.TargetId = targetId;
        }

        public GameActionFightCastOnTargetRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SpellId);
            writer.WriteDouble(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellId = reader.ReadVarUShort();
            TargetId = reader.ReadDouble();
        }

    }
}
