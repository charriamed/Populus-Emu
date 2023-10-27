namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightResultMutantListEntry : FightResultFighterListEntry
    {
        public new const short Id = 216;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Level { get; set; }

        public FightResultMutantListEntry(ushort outcome, sbyte wave, FightLoot rewards, double objectId, bool alive, ushort level)
        {
            this.Outcome = outcome;
            this.Wave = wave;
            this.Rewards = rewards;
            this.ObjectId = objectId;
            this.Alive = alive;
            this.Level = level;
        }

        public FightResultMutantListEntry() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadVarUShort();
        }

    }
}
