namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightNoSpellCastMessage : Message
    {
        public const uint Id = 6132;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint SpellLevelId { get; set; }

        public GameActionFightNoSpellCastMessage(uint spellLevelId)
        {
            this.SpellLevelId = spellLevelId;
        }

        public GameActionFightNoSpellCastMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(SpellLevelId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellLevelId = reader.ReadVarUInt();
        }

    }
}
