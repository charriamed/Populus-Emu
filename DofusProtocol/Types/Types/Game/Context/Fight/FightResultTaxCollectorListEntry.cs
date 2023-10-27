namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightResultTaxCollectorListEntry : FightResultFighterListEntry
    {
        public new const short Id = 84;
        public override short TypeId
        {
            get { return Id; }
        }
        public byte Level { get; set; }
        public BasicGuildInformations GuildInfo { get; set; }
        public int ExperienceForGuild { get; set; }

        public FightResultTaxCollectorListEntry(ushort outcome, sbyte wave, FightLoot rewards, double objectId, bool alive, byte level, BasicGuildInformations guildInfo, int experienceForGuild)
        {
            this.Outcome = outcome;
            this.Wave = wave;
            this.Rewards = rewards;
            this.ObjectId = objectId;
            this.Alive = alive;
            this.Level = level;
            this.GuildInfo = guildInfo;
            this.ExperienceForGuild = experienceForGuild;
        }

        public FightResultTaxCollectorListEntry() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(Level);
            GuildInfo.Serialize(writer);
            writer.WriteInt(ExperienceForGuild);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadByte();
            GuildInfo = new BasicGuildInformations();
            GuildInfo.Deserialize(reader);
            ExperienceForGuild = reader.ReadInt();
        }

    }
}
