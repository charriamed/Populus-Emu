namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GuildHousesInformationMessage : Message
    {
        public const uint Id = 5919;
        public override uint MessageId
        {
            get { return Id; }
        }
        public HouseInformationsForGuild[] HousesInformations { get; set; }

        public GuildHousesInformationMessage(HouseInformationsForGuild[] housesInformations)
        {
            this.HousesInformations = housesInformations;
        }

        public GuildHousesInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)HousesInformations.Count());
            for (var housesInformationsIndex = 0; housesInformationsIndex < HousesInformations.Count(); housesInformationsIndex++)
            {
                var objectToSend = HousesInformations[housesInformationsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var housesInformationsCount = reader.ReadUShort();
            HousesInformations = new HouseInformationsForGuild[housesInformationsCount];
            for (var housesInformationsIndex = 0; housesInformationsIndex < housesInformationsCount; housesInformationsIndex++)
            {
                var objectToAdd = new HouseInformationsForGuild();
                objectToAdd.Deserialize(reader);
                HousesInformations[housesInformationsIndex] = objectToAdd;
            }
        }

    }
}
