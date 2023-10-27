namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightResultListEntry
    {
        public const short Id  = 16;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort Outcome { get; set; }
        public sbyte Wave { get; set; }
        public FightLoot Rewards { get; set; }

        public FightResultListEntry(ushort outcome, sbyte wave, FightLoot rewards)
        {
            this.Outcome = outcome;
            this.Wave = wave;
            this.Rewards = rewards;
        }

        public FightResultListEntry() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(Outcome);
            writer.WriteSByte(Wave);
            Rewards.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Outcome = reader.ReadVarUShort();
            Wave = reader.ReadSByte();
            Rewards = new FightLoot();
            Rewards.Deserialize(reader);
        }

    }
}
