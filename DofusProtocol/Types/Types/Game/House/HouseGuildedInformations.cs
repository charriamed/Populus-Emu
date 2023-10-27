namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseGuildedInformations : HouseInstanceInformations
    {
        public new const short Id = 512;
        public override short TypeId
        {
            get { return Id; }
        }
        public GuildInformations GuildInfo { get; set; }

        public HouseGuildedInformations(bool secondHand, bool isLocked, bool isSaleLocked, int instanceId, string ownerName, long price, GuildInformations guildInfo)
        {
            this.SecondHand = secondHand;
            this.IsLocked = isLocked;
            this.IsSaleLocked = isSaleLocked;
            this.InstanceId = instanceId;
            this.OwnerName = ownerName;
            this.Price = price;
            this.GuildInfo = guildInfo;
        }

        public HouseGuildedInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            GuildInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            GuildInfo = new GuildInformations();
            GuildInfo.Deserialize(reader);
        }

    }
}
