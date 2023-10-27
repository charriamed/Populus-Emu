namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class SpellListMessage : Message
    {
        public const uint Id = 1200;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool SpellPrevisualization { get; set; }
        public SpellItem[] Spells { get; set; }

        public SpellListMessage(bool spellPrevisualization, SpellItem[] spells)
        {
            this.SpellPrevisualization = spellPrevisualization;
            this.Spells = spells;
        }

        public SpellListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(SpellPrevisualization);
            writer.WriteShort((short)Spells.Count());
            for (var spellsIndex = 0; spellsIndex < Spells.Count(); spellsIndex++)
            {
                var objectToSend = Spells[spellsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellPrevisualization = reader.ReadBoolean();
            var spellsCount = reader.ReadUShort();
            Spells = new SpellItem[spellsCount];
            for (var spellsIndex = 0; spellsIndex < spellsCount; spellsIndex++)
            {
                var objectToAdd = new SpellItem();
                objectToAdd.Deserialize(reader);
                Spells[spellsIndex] = objectToAdd;
            }
        }

    }
}
