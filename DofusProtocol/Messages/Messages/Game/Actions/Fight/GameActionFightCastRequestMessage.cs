namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightCastRequestMessage : Message
    {
        public const uint Id = 1005;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SpellId { get; set; }
        public short CellId { get; set; }

        public GameActionFightCastRequestMessage(ushort spellId, short cellId)
        {
            this.SpellId = spellId;
            this.CellId = cellId;
        }

        public GameActionFightCastRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SpellId);
            writer.WriteShort(CellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellId = reader.ReadVarUShort();
            CellId = reader.ReadShort();
        }

    }
}
