namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInfosUpgradeMessage : Message
    {
        public const uint Id = 5636;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte MaxTaxCollectorsCount { get; set; }
        public sbyte TaxCollectorsCount { get; set; }
        public ushort TaxCollectorLifePoints { get; set; }
        public ushort TaxCollectorDamagesBonuses { get; set; }
        public ushort TaxCollectorPods { get; set; }
        public ushort TaxCollectorProspecting { get; set; }
        public ushort TaxCollectorWisdom { get; set; }
        public ushort BoostPoints { get; set; }
        public ushort[] SpellId { get; set; }
        public short[] SpellLevel { get; set; }

        public GuildInfosUpgradeMessage(sbyte maxTaxCollectorsCount, sbyte taxCollectorsCount, ushort taxCollectorLifePoints, ushort taxCollectorDamagesBonuses, ushort taxCollectorPods, ushort taxCollectorProspecting, ushort taxCollectorWisdom, ushort boostPoints, ushort[] spellId, short[] spellLevel)
        {
            this.MaxTaxCollectorsCount = maxTaxCollectorsCount;
            this.TaxCollectorsCount = taxCollectorsCount;
            this.TaxCollectorLifePoints = taxCollectorLifePoints;
            this.TaxCollectorDamagesBonuses = taxCollectorDamagesBonuses;
            this.TaxCollectorPods = taxCollectorPods;
            this.TaxCollectorProspecting = taxCollectorProspecting;
            this.TaxCollectorWisdom = taxCollectorWisdom;
            this.BoostPoints = boostPoints;
            this.SpellId = spellId;
            this.SpellLevel = spellLevel;
        }

        public GuildInfosUpgradeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(MaxTaxCollectorsCount);
            writer.WriteSByte(TaxCollectorsCount);
            writer.WriteVarUShort(TaxCollectorLifePoints);
            writer.WriteVarUShort(TaxCollectorDamagesBonuses);
            writer.WriteVarUShort(TaxCollectorPods);
            writer.WriteVarUShort(TaxCollectorProspecting);
            writer.WriteVarUShort(TaxCollectorWisdom);
            writer.WriteVarUShort(BoostPoints);
            writer.WriteShort((short)SpellId.Count());
            for (var spellIdIndex = 0; spellIdIndex < SpellId.Count(); spellIdIndex++)
            {
                writer.WriteVarUShort(SpellId[spellIdIndex]);
            }
            writer.WriteShort((short)SpellLevel.Count());
            for (var spellLevelIndex = 0; spellLevelIndex < SpellLevel.Count(); spellLevelIndex++)
            {
                writer.WriteShort(SpellLevel[spellLevelIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            MaxTaxCollectorsCount = reader.ReadSByte();
            TaxCollectorsCount = reader.ReadSByte();
            TaxCollectorLifePoints = reader.ReadVarUShort();
            TaxCollectorDamagesBonuses = reader.ReadVarUShort();
            TaxCollectorPods = reader.ReadVarUShort();
            TaxCollectorProspecting = reader.ReadVarUShort();
            TaxCollectorWisdom = reader.ReadVarUShort();
            BoostPoints = reader.ReadVarUShort();
            var spellIdCount = reader.ReadUShort();
            SpellId = new ushort[spellIdCount];
            for (var spellIdIndex = 0; spellIdIndex < spellIdCount; spellIdIndex++)
            {
                SpellId[spellIdIndex] = reader.ReadVarUShort();
            }
            var spellLevelCount = reader.ReadUShort();
            SpellLevel = new short[spellLevelCount];
            for (var spellLevelIndex = 0; spellLevelIndex < spellLevelCount; spellLevelIndex++)
            {
                SpellLevel[spellLevelIndex] = reader.ReadShort();
            }
        }

    }
}
