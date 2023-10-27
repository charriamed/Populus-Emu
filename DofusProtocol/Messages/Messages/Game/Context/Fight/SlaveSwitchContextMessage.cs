namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class SlaveSwitchContextMessage : Message
    {
        public const uint Id = 6214;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MasterId { get; set; }
        public double SlaveId { get; set; }
        public SpellItem[] SlaveSpells { get; set; }
        public CharacterCharacteristicsInformations SlaveStats { get; set; }
        public Shortcut[] Shortcuts { get; set; }

        public SlaveSwitchContextMessage(double masterId, double slaveId, SpellItem[] slaveSpells, CharacterCharacteristicsInformations slaveStats, Shortcut[] shortcuts)
        {
            this.MasterId = masterId;
            this.SlaveId = slaveId;
            this.SlaveSpells = slaveSpells;
            this.SlaveStats = slaveStats;
            this.Shortcuts = shortcuts;
        }

        public SlaveSwitchContextMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MasterId);
            writer.WriteDouble(SlaveId);
            writer.WriteShort((short)SlaveSpells.Count());
            for (var slaveSpellsIndex = 0; slaveSpellsIndex < SlaveSpells.Count(); slaveSpellsIndex++)
            {
                var objectToSend = SlaveSpells[slaveSpellsIndex];
                objectToSend.Serialize(writer);
            }
            SlaveStats.Serialize(writer);
            writer.WriteShort((short)Shortcuts.Count());
            for (var shortcutsIndex = 0; shortcutsIndex < Shortcuts.Count(); shortcutsIndex++)
            {
                var objectToSend = Shortcuts[shortcutsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            MasterId = reader.ReadDouble();
            SlaveId = reader.ReadDouble();
            var slaveSpellsCount = reader.ReadUShort();
            SlaveSpells = new SpellItem[slaveSpellsCount];
            for (var slaveSpellsIndex = 0; slaveSpellsIndex < slaveSpellsCount; slaveSpellsIndex++)
            {
                var objectToAdd = new SpellItem();
                objectToAdd.Deserialize(reader);
                SlaveSpells[slaveSpellsIndex] = objectToAdd;
            }
            SlaveStats = new CharacterCharacteristicsInformations();
            SlaveStats.Deserialize(reader);
            var shortcutsCount = reader.ReadUShort();
            Shortcuts = new Shortcut[shortcutsCount];
            for (var shortcutsIndex = 0; shortcutsIndex < shortcutsCount; shortcutsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<Shortcut>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Shortcuts[shortcutsIndex] = objectToAdd;
            }
        }

    }
}
