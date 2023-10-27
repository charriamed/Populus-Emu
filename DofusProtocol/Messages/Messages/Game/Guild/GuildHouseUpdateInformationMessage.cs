namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildHouseUpdateInformationMessage : Message
    {
        public const uint Id = 6181;
        public override uint MessageId
        {
            get { return Id; }
        }
        public HouseInformationsForGuild HousesInformations { get; set; }

        public GuildHouseUpdateInformationMessage(HouseInformationsForGuild housesInformations)
        {
            this.HousesInformations = housesInformations;
        }

        public GuildHouseUpdateInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            HousesInformations.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            HousesInformations = new HouseInformationsForGuild();
            HousesInformations.Deserialize(reader);
        }

    }
}
