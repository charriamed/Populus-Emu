namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightResumeSlaveInfo
    {
        public const short Id  = 364;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double SlaveId { get; set; }
        public GameFightSpellCooldown[] SpellCooldowns { get; set; }
        public sbyte SummonCount { get; set; }
        public sbyte BombCount { get; set; }

        public GameFightResumeSlaveInfo(double slaveId, GameFightSpellCooldown[] spellCooldowns, sbyte summonCount, sbyte bombCount)
        {
            this.SlaveId = slaveId;
            this.SpellCooldowns = spellCooldowns;
            this.SummonCount = summonCount;
            this.BombCount = bombCount;
        }

        public GameFightResumeSlaveInfo() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(SlaveId);
            writer.WriteShort((short)SpellCooldowns.Count());
            for (var spellCooldownsIndex = 0; spellCooldownsIndex < SpellCooldowns.Count(); spellCooldownsIndex++)
            {
                var objectToSend = SpellCooldowns[spellCooldownsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteSByte(SummonCount);
            writer.WriteSByte(BombCount);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SlaveId = reader.ReadDouble();
            var spellCooldownsCount = reader.ReadUShort();
            SpellCooldowns = new GameFightSpellCooldown[spellCooldownsCount];
            for (var spellCooldownsIndex = 0; spellCooldownsIndex < spellCooldownsCount; spellCooldownsIndex++)
            {
                var objectToAdd = new GameFightSpellCooldown();
                objectToAdd.Deserialize(reader);
                SpellCooldowns[spellCooldownsIndex] = objectToAdd;
            }
            SummonCount = reader.ReadSByte();
            BombCount = reader.ReadSByte();
        }

    }
}
