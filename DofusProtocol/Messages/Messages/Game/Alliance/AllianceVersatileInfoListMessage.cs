namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceVersatileInfoListMessage : Message
    {
        public const uint Id = 6436;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AllianceVersatileInformations[] Alliances { get; set; }

        public AllianceVersatileInfoListMessage(AllianceVersatileInformations[] alliances)
        {
            this.Alliances = alliances;
        }

        public AllianceVersatileInfoListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Alliances.Count());
            for (var alliancesIndex = 0; alliancesIndex < Alliances.Count(); alliancesIndex++)
            {
                var objectToSend = Alliances[alliancesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var alliancesCount = reader.ReadUShort();
            Alliances = new AllianceVersatileInformations[alliancesCount];
            for (var alliancesIndex = 0; alliancesIndex < alliancesCount; alliancesIndex++)
            {
                var objectToAdd = new AllianceVersatileInformations();
                objectToAdd.Deserialize(reader);
                Alliances[alliancesIndex] = objectToAdd;
            }
        }

    }
}
