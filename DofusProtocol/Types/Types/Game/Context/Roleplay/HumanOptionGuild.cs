namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HumanOptionGuild : HumanOption
    {
        public new const short Id = 409;
        public override short TypeId
        {
            get { return Id; }
        }
        public GuildInformations GuildInformations { get; set; }

        public HumanOptionGuild(GuildInformations guildInformations)
        {
            this.GuildInformations = guildInformations;
        }

        public HumanOptionGuild() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            GuildInformations.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            GuildInformations = new GuildInformations();
            GuildInformations.Deserialize(reader);
        }

    }
}
