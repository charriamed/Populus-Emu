namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockGuildedInformations : PaddockBuyableInformations
    {
        public new const short Id = 508;
        public override short TypeId
        {
            get { return Id; }
        }
        public bool Deserted { get; set; }
        public GuildInformations GuildInfo { get; set; }

        public PaddockGuildedInformations(ulong price, bool locked, bool deserted, GuildInformations guildInfo)
        {
            this.Price = price;
            this.Locked = locked;
            this.Deserted = deserted;
            this.GuildInfo = guildInfo;
        }

        public PaddockGuildedInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Deserted);
            GuildInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Deserted = reader.ReadBoolean();
            GuildInfo = new GuildInformations();
            GuildInfo.Deserialize(reader);
        }

    }
}
