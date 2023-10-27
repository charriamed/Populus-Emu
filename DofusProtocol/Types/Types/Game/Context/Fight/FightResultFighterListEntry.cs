namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightResultFighterListEntry : FightResultListEntry
    {
        public new const short Id = 189;
        public override short TypeId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }
        public bool Alive { get; set; }

        public FightResultFighterListEntry(ushort outcome, sbyte wave, FightLoot rewards, double objectId, bool alive)
        {
            this.Outcome = outcome;
            this.Wave = wave;
            this.Rewards = rewards;
            this.ObjectId = objectId;
            this.Alive = alive;
        }

        public FightResultFighterListEntry() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(ObjectId);
            writer.WriteBoolean(Alive);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectId = reader.ReadDouble();
            Alive = reader.ReadBoolean();
        }

    }
}
